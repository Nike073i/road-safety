CREATE TABLE users.users(
    id uuid NOT NULL,
    is_enable boolean NOT NULL,
    CONSTRAINT pk_users_users_id PRIMARY KEY (id)
);
