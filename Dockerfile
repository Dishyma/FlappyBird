# Imagen base con Unity
FROM gableroux/unity3d:2022.3.16f1-linux-il2cpp

# Argumentos para las credenciales de Unity (necesarias para la activación)
ARG UNITY_USERNAME
ARG UNITY_PASSWORD
ARG UNITY_SERIAL

# Directorio de trabajo
WORKDIR /app

# Copiar el proyecto Unity
COPY . .

# Instalar dependencias necesarias
RUN apt-get update && apt-get install -y \
    xvfb \
    && rm -rf /var/lib/apt/lists/*

# Script para construir el proyecto
RUN echo '#!/bin/bash\n\
xvfb-run --auto-servernum --server-args="-screen 0 640x480x24" \
/opt/Unity/Editor/Unity \
    -quit \
    -batchmode \
    -nographics \
    -username "$UNITY_USERNAME" \
    -password "$UNITY_PASSWORD" \
    -serial "$UNITY_SERIAL" \
    -projectPath /app \
    -executeMethod BuildScript.BuildLinux \
    -logFile /dev/stdout' > /app/build.sh \
    && chmod +x /app/build.sh

# Punto de entrada para la construcción
ENTRYPOINT ["/app/build.sh"]