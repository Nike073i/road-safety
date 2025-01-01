CREATE TABLE articles.article_infos(
    article_id uuid NOT NULL,
    title text NOT NULL,
    subtitle text,
    image text,
    tags text[],
    CONSTRAINT pk_articles_article_infos_article_id PRIMARY KEY (article_id),
    CONSTRAINT fk_articles_article_infos_articles FOREIGN KEY (article_id) REFERENCES articles.articles(id)
);
