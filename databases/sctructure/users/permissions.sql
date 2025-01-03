CREATE TABLE users.permissions(
    code integer NOT NULL,
    name text NOT NULL,
    CONSTRAINT pk_users_permissions_code PRIMARY KEY (code)
);
