#!/bin/sh
base=$(dirname $0)
chmod +x $base/cleanup.sh $base/start-application.sh
systemctl start docker