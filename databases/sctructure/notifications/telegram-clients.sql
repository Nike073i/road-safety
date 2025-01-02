CREATE TABLE notifications.telegram_clients (
    user_id bigint NOT NULL,
    is_active boolean NOT NULL,
    CONSTRAINT pk_notifications_telegram_clients_user_id PRIMARY KEY (user_id)
);
