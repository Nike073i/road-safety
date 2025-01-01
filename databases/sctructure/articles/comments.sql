CREATE TABLE articles.comments(
    id uuid NOT NULL,
    author_id uuid NOT NULL,
    article_id uuid NOT NULL,
    content text NOT NULL,
    reply_comment_id uuid,
    published_at timestamp with time zone NOT NULL,
    last_edited_at timestamp with time zone,
    CONSTRAINT pk_articles_comments_id PRIMARY KEY (id),
    CONSTRAINT fk_articles_comments_articles FOREIGN KEY (id) REFERENCES articles.articles(id),
    CONSTRAINT fk_articles_comments_comments FOREIGN KEY (reply_comment_id) REFERENCES articles.comments(id)
);
