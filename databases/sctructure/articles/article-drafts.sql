CREATE TABLE articles.article_drafts(
    article_id uuid NOT NULL,
    blocks jsonb NOT NULL,
    CONSTRAINT pk_articles_article_drafts_article_id PRIMARY KEY (article_id),
    CONSTRAINT fk_articles_article_drafts_articles FOREIGN KEY (article_id) REFERENCES articles.articles(id)
);
