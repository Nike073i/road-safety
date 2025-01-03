CREATE TABLE users.user_roles(
    user_id uuid NOT NULL,
    role_code integer NOT NULL,
    CONSTRAINT pk_users_user_roles_user_id_role_code PRIMARY KEY (user_id, role_code),
    CONSTRAINT fk_users_user_roles_users FOREIGN KEY (user_id) REFERENCES users.users (id),
    CONSTRAINT fk_users_user_roles_roles FOREIGN KEY (role_code) REFERENCES users.roles (code)
);
