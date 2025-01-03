CREATE TABLE users.roles(
    code integer NOT NULL,
    name text NOT NULL,
    CONSTRAINT pk_users_roles_code PRIMARY KEY (code)
);
