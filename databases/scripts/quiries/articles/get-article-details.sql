SELECT 
    a.id, 
    a.author_id, 
    a.created_at, 
    a.is_archived, 
    a.is_enable_commenting, 
    a.visibility, 
    a.last_edited_at,
    i.title,
    i.subtitle,
    i.image,
    i.tags,
    d.blocks
FROM articles.articles AS a
    JOIN articles.article_infos AS i ON a.id = i.article_id
    JOIN articles.article_drafts AS d ON a.id = d.article_id
WHERE 
    a.id = '13e5b190-8094-40a5-a7c0-fd9807d1ab8f' AND --@Id
    a.is_archived = FALSE AND
	a.visibility = 'Visible';