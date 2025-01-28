CREATE SCHEMA users;

CREATE TABLE users.users(
    id uuid NOT NULL,
    CONSTRAINT pk_users_users_id PRIMARY KEY (id)
);

CREATE TABLE users.roles(
    code integer NOT NULL,
    name text NOT NULL,
    CONSTRAINT pk_users_roles_code PRIMARY KEY (code)
);

CREATE TABLE users.permissions(
    code integer NOT NULL,
    name text NOT NULL,
    CONSTRAINT pk_users_permissions_code PRIMARY KEY (code)
);

CREATE TABLE users.role_permissions(
    role_code integer NOT NULL,
    permission_code integer NOT NULL,
    CONSTRAINT pk_users_role_permissions_role_code_permission_code PRIMARY KEY (role_code, permission_code),
    CONSTRAINT fk_users_role_permissions_roles FOREIGN KEY (role_code) REFERENCES users.roles (code),
    CONSTRAINT fk_users_role_permissions_permissions FOREIGN KEY (permission_code) REFERENCES users.permissions (code)
);

CREATE TABLE users.user_roles(
    user_id uuid NOT NULL,
    role_code integer NOT NULL,
    CONSTRAINT pk_users_user_roles_user_id_role_code PRIMARY KEY (user_id, role_code),
    CONSTRAINT fk_users_user_roles_users FOREIGN KEY (user_id) REFERENCES users.users (id),
    CONSTRAINT fk_users_user_roles_roles FOREIGN KEY (role_code) REFERENCES users.roles (code)
);

CREATE TABLE users.inbox_state
(
    id SERIAL,
    message_id uuid NOT NULL,
    consumer_id uuid NOT NULL,
    lock_id uuid NOT NULL,
    row_version bytea,
    received timestamp with time zone NOT NULL,
    receive_count integer NOT NULL,
    expiration_time timestamp with time zone,
    consumed timestamp with time zone,
    delivered timestamp with time zone,
    last_sequence_number bigint,
    CONSTRAINT pk_users_inbox_state_id PRIMARY KEY (id),
    CONSTRAINT uc_users_inbox_state_message_id_consumer_id UNIQUE (message_id, consumer_id)
);

CREATE TABLE users.outbox_message
(
    sequence_number SERIAL,
    enqueue_time timestamp with time zone,
    sent_time timestamp with time zone NOT NULL,
    headers text,
    properties text,
    inbox_message_id uuid,
    inbox_consumer_id uuid,
    outbox_id uuid,
    message_id uuid NOT NULL,
    content_type character varying(256) NOT NULL,
    message_type text NOT NULL,
    body text NOT NULL,
    conversation_id uuid,
    correlation_id uuid,
    initiator_id uuid,
    request_id uuid,
    source_address character varying(256),
    destination_address character varying(256),
    response_address character varying(256),
    fault_address character varying(256),
    expiration_time timestamp with time zone,
    CONSTRAINT pk_users_outbox_message_sequence_number PRIMARY KEY (sequence_number)
);

CREATE TABLE users.outbox_state
(
    outbox_id uuid NOT NULL,
    lock_id uuid NOT NULL,
    row_version bytea,
    created timestamp with time zone NOT NULL,
    delivered timestamp with time zone,
    last_sequence_number bigint,
    CONSTRAINT pk_users_outbox_state_outbox_id PRIMARY KEY (outbox_id)
);

-- Seed

INSERT INTO users.roles(code, name) 
VALUES 
    (1, 'User'), 
    (2, 'Moderator');
