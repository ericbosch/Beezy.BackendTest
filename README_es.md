# Beezy.BackendTest
## _Eric Bosch Mompió - Abril 2020_
![.NET Core Build](https://github.com/ericbosch/Beezy.BackendTest/workflows/.NET%20Core%20Build/badge.svg) _[__Ejemplos de llamadas__](https://github.com/ericbosch/Beezy.BackendTest/wiki/Test-samples)_

*Vesión en [Inglés](README.md).*

### Ubicación de la api online

Podéis encontrar la api en la dirección siguiente: https://bit.ly/beezybackendapi

Url completa: https://beezybackendtestapi20200425164353.azurewebsites.net/

Acceso al swagger implementado: https://bit.ly/beezybackendtest

### Implementación

Implementación del escenario descrito en la prueba para __Beezy__.

Se ha desarrollado una API REST utilizando .NetCore 3.1.

- __Parte 1__: Se implementa la estructura de los controladores para todos los endpoints descritos. Actualmente devuelven un __error 404__ con el texto __"Under construction, come back soon!"__.

- __Parte 2__: Se implementa el endpoint _`api/managers/billboards/intelligent`_ con funcionalidad completa tanto para búsqueda en BD, como para búsqueda en la [API externa](themoviedb.org).
  - Se ha tomado como consideración que en lugar de utilizar un flag, se indique la ciudad a consultar. 
    - Si este está vacío buscará en la API
    - Si este tiene valor, buscará basándose en esa ciudad. 
      - Si como ciudad se utiliza __"All"__, devolverá las películas con resultado global.
      
* Se ha implementado la solución usando los principios __SOLID__, __YAGNI__, __KISS__ y __DRY__.
* Los commit sirven de histórico para ver el desarrollo de la solución.
* Para la api externa y el acceso a la base de datos se ha usado el patrón __Repository__ para no depender de la infraestructura de datos directamente.
* Se ha implementado un healthcheck accesible en `/health`
* Se implementa un pipeline de validación mediante [__FluentValidation__](https://fluentvalidation.net/).
* Se implementa el patrón __Mediator__ mediante [__MediatR__](https://github.com/jbogard/MediatR), de este modo se desacopla el controlador de sus dependencias, haciendo mas fácil su inyección y su extensión. El flujo de funcionamiento de MediatR es:
  - La __petición__ se manda al __manipulador__.
  - Éste procesa la petición y genera una __respuesta__.
  - Puede haber un intermediario que hace de __validador__ de la petición.
* Para el logging, se utiliza [__Serilog__](https://serilog.net/), el cual permite configurar varias salidas (en este caso se ha usado consola y fichero, pero se podría haber extendido a soluciones mas cloud como __Graylog__). Además se ha configurado un pipeline para que escriba todas las acciones de MediatR.
* Se usa el patrón __MayBe__, utilizando [__Optional__](https://github.com/nlkl/Optional), de este modo se puede controlar los casos límite con facilidad.

* Para las pruebas, se han creado test de integración para verificar la integridad del flujo de trabajo, así como test unitarios para los dominios y test de aceptación para los controladores.

* Se usa [__Hellang.Middleware.ProblemDetails__](https://github.com/khellang/Middleware) para la gestión de errores.
* Se usa [__ServiceStack__](https://github.com/ServiceStack/ServiceStack) para la conexión a la Api externa.
* Se usa [__Swagger__](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) para la gestión documental de la Api, así como para proveer de un entorno dónde probarla.
* Se usa [__Scrutor__](https://github.com/khellang/Scrutor) para escanear los posibles validadores que se creen.

* Se usa [__Autofixture__](https://github.com/AutoFixture/AutoFixture) para declarar casos de prueba.
* Se usa [__Bogus__](https://github.com/bchavez/Bogus) para generar datos de prueba aleatorios.
* Se usa [__FluentAssertions__](https://fluentassertions.com/) para facilitar la creación de los casos de prueba.
* Se usa [__NSubstitute__](https://nsubstitute.github.io/) para crear mocks.

      
### Diseño

La solución se implementa siguiendo la arquitectura __Clean__ por capas, teniendo:
- Capa de infraestructura:
  - Proxy para las llamadas a la Api externa.
  - Repositorio para las consultas en la base de datos.
  - Particularidades de la Api (Cross-cutting).
- Capa de dominio:
  - Entidades
  - Modelos de negocio
- Capa de aplicación:
  - Controladores
  - Servicios
  - Configuración de la Api
  
Esta arquitectura permite que el dominio sea agnóstico del resto de capas, teniendo la infraestructura en la capa exterior.

La Api se encuentra versionada mediante __namespace__, así queda estructurado de forma que hacer un nuevo desarrollo o funcionalidad sea mas simple.

Tanto el contexto como las entidades se han obtenido mediante la utilidad de scaffolding de [dotnet-ef](https://docs.microsoft.com/es-es/ef/core/managing-schemas/scaffolding), tomando ciertas modificaciones para el correcto uso necesario.

### Conclusiones

* Habría sido interesante crear __ValueObjects__ para las entidades, de este modo se puede generar diversas validaciones adicionales en su creación.
* Se podría haber usado [__Polly__](https://github.com/App-vNext/Polly) para controlar los accesos a la base de datos y cambiar entity framework por [__Dapper__](https://stackexchange.github.io/Dapper/) con tal de optimizar las consultas.
* Utilizar dos contextos de base de datos: uno para lectura y otro para escritura. De este modo se pueden controlar mejor los accesos a los datos y su (posible) manipulación.
* Los datos sensibles están expuestos en la configuración (appsettings). En esta caso particular no hay grandes problemas de seguridad, pero en un entorno mas productivo debería haberse usado algún gestor como [__Vault__](https://www.vaultproject.io/) o el mismo gestor ofrecido por [__Visual Studio__](https://docs.microsoft.com/es-es/aspnet/core/security/app-secrets), aunque este último no se lleva muy bien con entornos deslocalizados como Docker.

* Api publicada en Azure
* [Action](https://github.com/ericbosch/Beezy.BackendTest/actions?query=workflow%3A%22.NET+Core+Build%22) de github creada para automatizar la compilación.
