# Acudir Challenge
Challenge para Acudir Emergencias

## Stack
| tecnología | versión |
| ------ | ------ |
| .NET Core | 6 |
| Microsoft SQL | 2017+ / Express |
| [VueJS](https://vuejs.org/guide/quick-start.html) | 2 |
| [NuxtJS](https://nuxtjs.org) | 2 |

## Inicialización del proyecto

### Back-end
Configurar la conexión a la base de datos dentro del archivo appsettings.json:
```json
{
  "ConnectionStrings": {
    "AcudirChallengeDatabase" : "Server=[SERVER];Database=AcudirChallenge;User=sa;Password=[PASSWORD];Trusted_Connection=False;"
  }
}
```
Para iniciar la aplicación ejecutar la siguiente linea de comando desde una consola:
```
22
dotnet run --project src/Acudir.API/Acudir.API.csproj
23
```
Una vez iniciado el back-end se podrá acceder a la documentación Swagger desde la url [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html)

### Front-end
Para iniciar la aplicación ejecutar las siguientes lineas de comando desde una consola:
```
cd front-end
npm install
npm run dev
```

Una vez iniciado el front-end se podrá acceder desde la url [http://localhost:3000/](http://localhost:3000/)
