#!/bin/bash
# hex_to_ascii.sh

if [ -z "$1" ]
then
    echo "No argument supplied. Please provide hex string."
    exit 1
fi

hex=$1
echo -n $hex | xxd -r -p
echo

#Example:
#sh hex_to_ascii.sh 53657276657220726563656976656420796f7572206d65737361676521
#Server received your message!