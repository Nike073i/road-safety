# Database Migrator
Утилита для применения SQL-скриптов к базе данных PostgreSQL.

## Установка

### Установка через скрипт
Для установки утилиты в Linux используйте скрипт `install-tool.sh`. Сначала дайте права на выполнение скрипта и затем запустите его:

```sh
chmod +x ../install-tool.sh
../install-tool.sh database-migrator
```

### Установка вручную
Утилиту также можно установить вручную. Для этого установите зависимости, указанные в файле `requirements.txt`. Рекомендуется использовать виртуальное окружение venv для изоляции зависимостей

## Параметры и конфигурация

### Источники
Конфигурация утилиты осуществляется через два источника:
- Файл `.env`
- Аргументы командной строки: именованные параметры, передаваемые при запуске утилиты.

> Примечание: Аргументы командной строки имеют приоритет над значениями из файла `.env`

> [[env.sample|Пример файла `.env`]]

### Параметры
Доступные параметры конфигурации:
- `db_url` - Строка подключения к базе данных PostgreSQL (обязательный параметр).
- `script_path` - Путь к директории со скриптами миграции (обязательный параметр).
- `log_name` - Имя файла для логов (необязательный параметр, по умолчанию `logs.log`).

Путь к файлу `.env` можно указать через аргумент командной строки `--env_path`.

### Пути
При указании путя следует учитывать:
- Параметры с путями в командной строке разрешаются относительно текущей директории, из которой вызывается утилита.
- Параметры пути в файле .env разрешаются относительно директории, где расположен сам .env файл.

## Скрипты
Скрипты должны быть сохранены в файлах с расширением .sql. Они будут применяться в алфавитном порядке. Если скрипт уже был применен, его повторное изменение не приведет к повторному применению.

## Примеры вызова утилиты

### С использованием файла .env
```sh
# рабочий каталог - /home/user/road-safety/tools/database-migrator
python main.py --env_path ../../secrets/migrator.env --log_name "migrator.log"
```

### С передачей параметров через командную строку
```sh
python main.py --db_url "postgresql://user:password@localhost:5432/road-safety" --script_path ../../databases/scripts/seeds
```
