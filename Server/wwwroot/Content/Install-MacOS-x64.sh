#!/bin/bash

HostName=
Organization=
GUID="$(uuidgen)"
UpdatePackagePath=""

Args=( "$@" )
ArgLength=${#Args[@]}

for (( i=0; i<${ArgLength}; i+=2 ));
do
    if [ "${Args[$i]}" = "--uninstall" ]; then
        launchctl unload -w /Library/LaunchDaemons/rimot-agent.plist
        rm -r -f /usr/local/bin/Rimot/
        rm -f /Library/LaunchDaemons/rimot-agent.plist
        exit
    elif [ "${Args[$i]}" = "--path" ]; then
        UpdatePackagePath="${Args[$i+1]}"
    fi
done


# Install Homebrew

if [[ -n "$SUDO_USER" && "$SUDO_USER" != "root" ]]; then
    su - $SUDO_USER -c '/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"'
fi

Owner=$(ls -l /usr/local/bin/brew | awk '{print $3}')

su - $Owner -c "brew update"

# Install .NET Runtime
su - $Owner -c "brew install --cask dotnet"

# Install dependency for System.Drawing.Common
su - $Owner -c "brew install mono-libgdiplus"

# Install other dependencies
su - $Owner -c "brew install curl"
su - $Owner -c "brew install jq"


if [ -f "/usr/local/bin/Rimot/ConnectionInfo.json" ]; then
    SavedGUID=`su - $Owner -c "cat '/usr/local/bin/Rimot/ConnectionInfo.json' | jq -r '.DeviceID'"`
    if [[ "$SavedGUID" != "null" && -n "$SavedGUID" ]]; then
        GUID="$SavedGUID"
    fi
fi

rm -r -f /Applications/Rimot
rm -f /Library/LaunchDaemons/rimot-agent.plist

mkdir -p /usr/local/bin/Rimot/
chmod -R 755 /usr/local/bin/Rimot/
cd /usr/local/bin/Rimot/

if [ -z "$UpdatePackagePath" ]; then
    echo  "Downloading client..." >> /tmp/Rimot_Install.log
    curl $HostName/Content/Rimot-MacOS-x64.zip --output /usr/local/bin/Rimot/Rimot-MacOS-x64.zip
else
    echo  "Copying install files..." >> /tmp/Rimot_Install.log
    cp "$UpdatePackagePath" /usr/local/bin/Rimot/Rimot-MacOS-x64.zip
    rm -f "$UpdatePackagePath"
fi

unzip -o ./Rimot-MacOS-x64.zip
rm -f ./Rimot-MacOS-x64.zip


connectionInfo="{
    \"DeviceID\":\"$GUID\", 
    \"Host\":\"$HostName\",
    \"OrganizationID\": \"$Organization\",
    \"ServerVerificationToken\":\"\"
}"

echo "$connectionInfo" > ./ConnectionInfo.json

curl --head $HostName/Content/Rimot-MacOS-x64.zip | grep -i "etag" | cut -d' ' -f 2 > ./etag.txt


plistFile="<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">
<plist version=\"1.0\">
<dict>
    <key>Label</key>
    <string>com.qits.rimot-agent</string>
    <key>ProgramArguments</key>
    <array>
        <string>/usr/local/bin/dotnet</string>
        <string>/usr/local/bin/Rimot/Rimot_Agent.dll</string>
    </array>
    <key>KeepAlive</key>
    <true/>
</dict>
</plist>"
echo "$plistFile" > "/Library/LaunchDaemons/rimot-agent.plist"

launchctl load -w /Library/LaunchDaemons/rimot-agent.plist
launchctl kickstart -k system/com.qits.rimot-agent
