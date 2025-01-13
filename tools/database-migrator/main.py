from logger import LoggerFactory
import sys
from yoyo import read_migrations, get_backend
import fire
import os
from config import Configuration

def main(env_path: str = None, **kwargs):
    config = Configuration()
    config.add_env(env_path)
    config.add_args(kwargs)

    factory = LoggerFactory(config.get_log_name())
    logger = factory.create_logger()

    scripts = config.get_script_path()
    if not scripts:
        logger.error('Укажите путь к директории со скриптами')
        sys.exit(1)

    if not os.path.isdir(scripts):
        logger.error(f"Указан неверный путь к директории со скриптами - {scripts}")
        sys.exit(1)

    db_url = config.get_db_url()
    if not db_url:
        logger.error('Укажите строку подключения к БД')
        sys.exit(1)

    try:
        logger.info(f"Местоположение скриптов - {scripts}")
        backend = get_backend(db_url)

        migrations = read_migrations(scripts)

        with backend.lock():
            to_apply = backend.to_apply(migrations)
            if not to_apply:
                logger.info("Нет миграций для применения.")
            else:
                logger.info(f"Найдено {len(to_apply)} миграций для применения.")
                backend.apply_migrations(to_apply)
                logger.info("Миграции успешно применены.")
    
    except Exception as e:
        logger.error(f"Ошибка при выполнении скрипта: {e}", exc_info=True)
        sys.exit(1)
    

if __name__ == '__main__':
    fire.Fire(main)
