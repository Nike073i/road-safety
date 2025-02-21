namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public interface ISerializer
	{
		T? Deserialize<T>(string json);
		string Serialize<T>(T elem);
	}
}
