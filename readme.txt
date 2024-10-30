Si desea ejecutar el docker en local, puede que necesite
tener una licensia de unity, para esto el comando que se 
use es:

sudo docker build -t unity-build \                         
  --build-arg UNITY_USERNAME="Correo aqui" \
  --build-arg UNITY_PASSWORD=<Contrasena aqui> .

puede verificar los pipelines, en github, de todas maneras
adjunto el pipeline que se ejecuta cuando se hace un push en
el main, y genera los archivos ejecutables para linux y wind
al igual que los assets en un archivo comprimido.