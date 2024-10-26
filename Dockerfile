# Usa la imagen de Unity CI adecuada
FROM unityci/editor:2020.1.1f1-linux-il2cpp-0.14.0

# Define argumentos para las credenciales de usuario
ARG UNITY_USERNAME
ARG UNITY_PASSWORD

# Establece la ubicación del archivo de licencia y variables para optimizar la activación
ENV UNITY_LICENSE="/workspace/license/Unity_lic.ulf"
ENV UNITY_NOPROXY="localhost,127.0.0.1"
ENV UNITY_DISABLE_HOME_SCREEN="true"

# Copia el archivo de licencia previamente activado en el contenedor
COPY Unity_lic.ulf /workspace/license/Unity_lic.ulf

# Ejecuta la activación de licencia usando el archivo manual
RUN unity-editor \
    -batchmode \
    -nographics \
    -logFile /dev/stdout \
    -manualLicenseFile "$UNITY_LICENSE" \
    -quit || true

# Comando para construir el proyecto en Linux
RUN unity-editor \
    -projectPath . \
    -quit \
    -batchmode \
    -nographics \
    -executeMethod BuildScript.BuildLinux \
    -buildTarget StandaloneLinux64 \
    -logFile /dev/stdout || exit 1

# Comando para construir el proyecto en Windows
RUN unity-editor \
    -projectPath . \
    -quit \
    -batchmode \
    -nographics \
    -executeMethod BuildScript.BuildWindows \
    -buildTarget StandaloneWindows64 \
    -logFile /dev/stdout || exit 1

# Limpia la licencia después del build
RUN unity-editor -quit -batchmode -returnlicense || true

# Define el volumen de salida para los builds
VOLUME /workspace/build