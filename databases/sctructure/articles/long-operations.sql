CREATE TABLE articles.long_operations(
    id uuid NOT NULL,
    operation_json TEXT NOT NULL,
    ocurred_on timestamp with time zone NOT NULL,
    scheduled_time timestamp with time zone,
    error TEXT,
    processed_on timestamp with time zone,
    CONSTRAINT pk_articles_long_operations_id PRIMARY KEY (id)
);
