CREATE TABLE articles.articles(
    id uuid NOT NULL,
    author_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    visibility articles.visibility NOT NULL,
    comments_included boolean NOT NULL,
    last_edited_at timestamp with time zone,
    CONSTRAINT pk_articles_articles_id PRIMARY KEY (id)
);
