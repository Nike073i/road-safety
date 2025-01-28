using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RoadSafety.BuildingBlocks.Application.Auth;

namespace RoadSafety.BuildingBlocks.Api.Auth
{
	public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
	{
		private readonly Lazy<UserInfo?> _lazyUserInfo = new(
			() => ExtractInfo(httpContextAccessor)
		);
		public UserInfo? UserInfo => _lazyUserInfo.Value;

		private static UserInfo? ExtractInfo(IHttpContextAccessor httpContextAccessor)
		{
			var httpContext = httpContextAccessor.HttpContext;
			if (httpContext is null || httpContext.User.Identity is not { IsAuthenticated: true })
				return default;

			var user = httpContext.User;
			var userId = GetValue(user, ClaimTypes.NameIdentifier, Guid.Parse);
			string email = GetValue(user, ClaimTypes.Email);
			bool emailVerified = GetValue(user, JwtRegisteredClaimNames.EmailVerified, bool.Parse);
			string username = GetValue(user, JwtRegisteredClaimNames.PreferredUsername);
			return new(userId, username, email, emailVerified);
		}

		private static T GetValue<T>(
			ClaimsPrincipal principal,
			string claimName,
			Func<string, T> transfor
		)
		{
			string? value = principal.FindFirstValue(claimName);
			return string.IsNullOrEmpty(value)
				? throw new ApplicationException("A required statement is missing")
				: transfor(value);
		}

		private static string GetValue(ClaimsPrincipal principal, string claimName) =>
			GetValue(principal, claimName, value => value);
	}
}
