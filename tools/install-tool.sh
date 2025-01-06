#!/bin/bash

if [ -z "$1" ]; then
  echo "Ошибка: Укажите имя нужной утилиты"
  exit 1
fi

scriptDir=$(dirname $(realpath $0)) 

if [ ! -d "$scriptDir/$1" ]; then
  echo "Ошибка: Утилита с именем '$1' не существует"
  exit 1
fi

python3 -m venv $scriptDir/$1/venv
. $scriptDir/$1/venv/bin/activate
pip install -r $scriptDir/$1/requirements.txt

echo "Утилита успешно установлена"