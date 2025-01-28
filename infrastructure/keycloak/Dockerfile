FROM keycloak/keycloak

ARG RABBIT_PLUGIN_VERSION=3.0.5

WORKDIR /opt/keycloak/providers

ADD --chmod=664 https://github.com/aznamier/keycloak-event-listener-rabbitmq/releases/download/${RABBIT_PLUGIN_VERSION}/keycloak-to-rabbit-${RABBIT_PLUGIN_VERSION}.jar .  

CMD [ "start" ]
