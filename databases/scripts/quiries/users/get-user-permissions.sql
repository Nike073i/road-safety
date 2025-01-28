SELECT p.code, p.name
    FROM (
        SELECT role_code 
        FROM users.user_roles 
        WHERE user_id = '31214dac-c37e-436e-8544-471b8fe9985f' -- @UserId
        ) as r
    LEFT JOIN users.role_permissions AS rp
        ON r.role_code = rp.role_code
    LEFT JOIN users.permissions AS p
        ON rp.permission_code = p.code;