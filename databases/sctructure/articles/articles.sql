CREATE TABLE articles.articles(
    id uuid NOT NULL,
    author_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    is_archived boolean NOT NULL DEFAULT FALSE,
    is_enable_commenting boolean NOT NULL,
    visibility articles.visibility NOT NULL,
    last_edited_at timestamp with time zone,
    public_version int NOT NULL DEFAULT 0,
    CONSTRAINT pk_articles_articles_id PRIMARY KEY (id)
);
