FROM ubuntu:jammy

EXPOSE 5000

ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ASPNETCORE_URLS="http://*:5000"

RUN \
  apt-get -y update && \
  apt-get -y install \
  apt-utils \
  wget \
  apt-transport-https \
  unzip \
  acl \
  libssl1.0

RUN \
  apt-get -y install aspnetcore-runtime-6.0

RUN \
  mkdir -p /var/www/rimot && \
  mkdir /config && \
  wget -q https://github.com/rimot-support/Rimot/releases/latest/download/Rimot_Server_Linux-x64.zip && \
  unzip -o Rimot_Server_Linux-x64.zip -d /var/www/rimot && \
  rm Rimot_Server_Linux-x64.zip

RUN \
  mkdir -p /rimot-data && \
  sed -i 's/DataSource=Rimot.db/DataSource=\/rimot-data\/Rimot.db/' /var/www/rimot/appsettings.json

VOLUME "/rimot-data"

WORKDIR /var/www/rimot

COPY DockerMain.sh /

RUN chmod 755 /DockerMain.sh

ENTRYPOINT ["/DockerMain.sh"]
