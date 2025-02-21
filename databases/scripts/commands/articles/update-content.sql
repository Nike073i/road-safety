BEGIN;

UPDATE articles.article_drafts AS d
SET blocks = '[ 
    { "type": "TEXT", "title": "Заголовок 1", "paragraphs": [ "Абзац 1", "Абзац 2", "Абзац 3" ] }, 
    { "type": "IMAGE", "title": "Рисунок 1", "src": "https://www.birigroup.co.uk/public/uploads/blogs/understanding-traffic-awareness-and-road-safety-a-complete-guide.webp" } 
]' -- @Blocks 
FROM articles.articles AS a
WHERE 
    d.article_id = '8762fb84-3df7-4a09-8c3a-73da63d41561' AND  -- @ArticleId
    a.public_version < 10; -- @NewVersion

UPDATE articles.article_infos AS i
SET 
    title = 'Тестовая статья', -- @Title
    subtitle = 'Подтекстовый текст', -- @Subtitle
    image = 'https://www.birigroup.co.uk/public/uploads/blogs/What-is-traffic-safety-Everything-that-you-need-to-know.webp', -- @Image
    tags = '{"Тест", "Статья"}' -- @Tags
FROM articles.articles AS a
WHERE 
    i.article_id = '8762fb84-3df7-4a09-8c3a-73da63d41561' AND  -- @ArticleId
    a.public_version < 10; -- @NewVersion

UPDATE articles.articles
SET 
    last_edited_at = '2025-01-29 23:43:42', --@EditedAt
    public_version = 10; -- @NewVersion
WHERE
    article_id = '8762fb84-3df7-4a09-8c3a-73da63d41561' AND  -- @ArticleId
    public_version < 10; -- @NewVersion

COMMIT;
