import logging
from constants import Constants
import os
import sys

class LoggerFactory:
    def __init__(self, filename):
        if not filename:
            filename = os.path.realpath(os.path.join(os.path.dirname(__file__), Constants.DEFAULT_LOG))
        self.filename = filename
        self.level = logging.INFO

    def create_logger(self):
        logging.basicConfig(
            level=self.level,
            format='%(asctime)s - %(levelname)s - %(message)s',
            handlers=[
                logging.StreamHandler(sys.stdout),
                logging.FileHandler(self.filename, mode='a')
            ]
        )

        return logging.getLogger()