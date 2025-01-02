CREATE TABLE users.role_permissions(
    role_id integer NOT NULL,
    permission_id integer NOT NULL,
    CONSTRAINT pk_users_role_permissions_role_id_permission_id PRIMARY KEY (role_id, permission_id),
    CONSTRAINT fk_users_role_permissions_roles FOREIGN KEY (role_id) REFERENCES users.roles (id),
    CONSTRAINT fk_users_role_permissions_permissions FOREIGN KEY (permission_id) REFERENCES users.permissions (id)
);
