#!/bin/sh

OUTPUT_DIR=`pwd`
OUTPUT_DIR_NAME=`basename $OUTPUT_DIR`

if [ -z "$1" ]; then
  NAME=$OUTPUT_DIR_NAME
else
  NAME=$1
fi

SCRIPT_DIR=`dirname $0`

TEMPLATE_NAME=query.tt

TEMPLATE_PATH=$SCRIPT_DIR/$TEMPLATE_NAME

dotnet t4 -p:Name="$NAME" -o $OUTPUT_DIR/$NAME "$TEMPLATE_PATH"
