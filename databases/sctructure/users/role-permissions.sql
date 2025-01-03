CREATE TABLE users.role_permissions(
    role_code integer NOT NULL,
    permission_code integer NOT NULL,
    CONSTRAINT pk_users_role_permissions_role_code_permission_code PRIMARY KEY (role_code, permission_code),
    CONSTRAINT fk_users_role_permissions_roles FOREIGN KEY (role_code) REFERENCES users.roles (code),
    CONSTRAINT fk_users_role_permissions_permissions FOREIGN KEY (permission_code) REFERENCES users.permissions (code)
);
