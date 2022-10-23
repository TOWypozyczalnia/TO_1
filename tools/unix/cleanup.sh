#!/bin/sh
base=$(dirname $0)
docker-compose -f $base/../../docker-compose.yml down -v