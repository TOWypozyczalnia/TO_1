#!/bin/sh
base=$(dirname $0)
chmod +x $base/cleanup.sh $base/start-application.sh
sudo apt update
sudo apt install docker-engine docker-compose -y