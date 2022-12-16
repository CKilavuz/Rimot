#!/bin/bash

echo "Entered main script."

ServerDir=/var/www/rimot
RimotData=/rimot-data

AppSettingsVolume=/rimot-data/appsettings.json
AppSettingsWww=/var/www/rimot/appsettings.json

if [ ! -f "$AppSettingsVolume" ]; then
	echo "Copying appsettings.json to volume."
	cp "$AppSettingsWww" "$AppSettingsVolume"
fi

if [ -f "$AppSettingsWww" ]; then
	rm "$AppSettingsWww"
fi

ln -s "$AppSettingsVolume" "$AppSettingsWww"

echo "Starting Rimot server."
exec /usr/bin/dotnet /var/www/rimot/Rimot_Server.dll
