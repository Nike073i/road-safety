BEGIN;

SELECT 
    id
    operation_json
FROM long_operations
WHERE 
    processed_on IS NULL AND
    (scheduled_time IS NULL OR scheduled_time <= '2025-02-18 17:46:42') --@CurrentTime
ORDER BY ocurred_on
LIMIT 1
FOR UPDATE SKIP LOCKED;

UPDATE long_operations
    SET 
        processed_on = '2025-02-18 17:48:42', --@ProcessedOn
        error = NULL --@Error
    WHERE id = '8762fb84-3df7-4a09-8c3a-73da63d41561'; --@Id

COMMIT;

