#!/bin/bash
HostName=
Organization=
GUID=$(cat /proc/sys/kernel/random/uuid)
UpdatePackagePath=""


Args=( "$@" )
ArgLength=${#Args[@]}

for (( i=0; i<${ArgLength}; i+=2 ));
do
    if [ "${Args[$i]}" = "--uninstall" ]; then
        systemctl stop rimot-agent
        rm -r -f /usr/local/bin/Rimot
        rm -f /etc/systemd/system/rimot-agent.service
        systemctl daemon-reload
        exit
    elif [ "${Args[$i]}" = "--path" ]; then
        UpdatePackagePath="${Args[$i+1}"
    fi
done

pacman -Sy
pacman -S dotnet-runtime-6.0 --noconfirm
pacman -S libx11 --noconfirm
pacman -S unzip --noconfirm
pacman -S libc6 --noconfirm
pacman -S libgdiplus --noconfirm
pacman -S libxtst --noconfirm
pacman -S xclip --noconfirm
pacman -S jq --noconfirm
pacman -S curl --noconfirm

if [ -f "/usr/local/bin/Rimot/ConnectionInfo.json" ]; then
    SavedGUID=`cat "/usr/local/bin/Rimot/ConnectionInfo.json" | jq -r '.DeviceID'`
    if [[ "$SavedGUID" != "null" && -n "$SavedGUID" ]]; then
        GUID="$SavedGUID"
    fi
fi

rm -r -f /usr/local/bin/Rimot
rm -f /etc/systemd/system/rimot-agent.service

mkdir -p /usr/local/bin/Rimot/
cd /usr/local/bin/Rimot/

if [ -z "$UpdatePackagePath" ]; then
    echo  "Downloading client..." >> /tmp/Rimot_Install.log
    wget $HostName/Content/Rimot-Linux.zip
else
    echo  "Copying install files..." >> /tmp/Rimot_Install.log
    cp "$UpdatePackagePath" /usr/local/bin/Rimot/Rimot-Linux.zip
    rm -f "$UpdatePackagePath"
fi

unzip ./Rimot-Linux.zip
rm -f ./Rimot-Linux.zip
chmod +x ./Rimot_Agent
chmod +x ./Desktop/Rimot_Desktop


connectionInfo="{
    \"DeviceID\":\"$GUID\", 
    \"Host\":\"$HostName\",
    \"OrganizationID\": \"$Organization\",
    \"ServerVerificationToken\":\"\"
}"

echo "$connectionInfo" > ./ConnectionInfo.json

curl --head $HostName/Content/Rimot-Linux.zip | grep -i "etag" | cut -d' ' -f 2 > ./etag.txt

echo Creating service... >> /tmp/Rimot_Install.log

serviceConfig="[Unit]
Description=The Rimot agent used for remote access.

[Service]
WorkingDirectory=/usr/local/bin/Rimot/
ExecStart=/usr/local/bin/Rimot/Rimot_Agent
Restart=always
StartLimitIntervalSec=0
RestartSec=10

[Install]
WantedBy=graphical.target"

echo "$serviceConfig" > /etc/systemd/system/rimot-agent.service

systemctl enable rimot-agent
systemctl restart rimot-agent

echo Install complete. >> /tmp/Rimot_Install.log
