from dotenv import dotenv_values
from constants import Constants
import os

class Configuration:
    def __init__(self):
        self.__settings__ = dict()

    def add_env(self, config_path=None):
        if not config_path:
            base_path = os.path.dirname(__file__)
            config_path = os.path.realpath(os.path.join(base_path, Constants.DEFAULT_ENV))
        else:
            base_path = os.path.realpath(os.path.dirname(config_path))

        values = dotenv_values(config_path)
        self.__update_values__(values, base_path)
        return self

    def __update_values__(self, values, base_path):
        transformed_update = {
            key: os.path.realpath(os.path.join(base_path, value)) if key.endswith(Constants.PATHS_SUFFIX) else value
            for key, value in values.items()
        }
        self.__settings__.update(transformed_update)

    def add_args(self, args):
        base_path = os.getcwd()
        self.__update_values__(args, base_path)
        return self

    def get_log_name(self):
        return self.__get_value__(Constants.LOG_NAME_KEY)

    def get_db_url(self):
        return self.__get_value__(Constants.DB_URL_KEY)

    def get_script_path(self):
        return self.__get_value__(Constants.SCRIPT_PATHS_KEY)

    def __get_value__(self, key):
        return self.__settings__[key] if key in self.__settings__ else None

