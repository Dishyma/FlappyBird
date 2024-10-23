# Imagen base con Unity (usando una versión disponible)
FROM gableroux/unity3d:2020.1.1f1-linux-il2cpp

# Directorio de trabajo
WORKDIR /app

# Copiar el proyecto Unity
COPY . .

# Instalar dependencias necesarias
RUN apt-get update && apt-get install -y \
    xvfb \
    && rm -rf /var/lib/apt/lists/*

# Script para activar Unity y construir el proyecto
RUN echo '#!/bin/bash\n\
# Activar Unity Personal\n\
xvfb-run --auto-servernum --server-args="-screen 0 640x480x24" \
/opt/Unity/Editor/Unity \
    -batchmode \
    -nographics \
    -username "$UNITY_USERNAME" \
    -password "$UNITY_PASSWORD" \
    -quit \
    || true\n\
# Construir el proyecto\n\
xvfb-run --auto-servernum --server-args="-screen 0 640x480x24" \
/opt/Unity/Editor/Unity \
    -quit \
    -batchmode \
    -nographics \
    -projectPath /app \
    -executeMethod BuildScript.BuildLinux \
    -logFile /dev/stdout' > /app/build.sh \
    && chmod +x /app/build.sh

# Punto de entrada para la construcción
ENTRYPOINT ["/app/build.sh"]