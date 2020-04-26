# Beezy.BackendTest
## _Eric Bosch Mompió - April 2020_
![.NET Core Build](https://github.com/ericbosch/Beezy.BackendTest/workflows/.NET%20Core%20Build/badge.svg) 
_[__Api call samples__](https://github.com/ericbosch/Beezy.BackendTest/wiki/Test-samples)_

### Online api location


You can find the api located on the following address: https://bit.ly/beezybackendapi

Full url: https://beezybackendtestapi20200425164353.azurewebsites.net/

Swagger access: https://bit.ly/beezybackendtest

### Implementation

A REST Api has been developed using .Net Core 3.1


- __Part 1__: The controller structure is implemented for all described endpoints. Currently returns a __404 error__ with the following text: __"Under construction, come back soon!"__.

- __Part 2__: `api/managers/billboards/intelligent` endpoint implemented with full funcionality for both Database and  [external Api](themoviedb.org) searches.
  - It has been taken as a consideration that instead of using a flag, the city to be consulted is indicated on the request.
    - If it's empty, it will search on the external Api.
    - If it has value, it will search based on that city.
      - If __"All"__ is used as city, it will return the films with global results.
      
* Solution implemented using __SOLID__, __YAGNI__, __KISS__ and __DRY__ principles.
* Commits serve as history to see the development of the solution.
* For external Api and database acces the __Repository__ pattern has been used to not depend on the data infrastucture directly.
* Healthcheck available on `/health`
* Pipeline validation via [__FluentValidation__](https://fluentvalidation.net/).
* __Mediator__ pattern implemented via [__MediatR__](https://github.com/jbogard/MediatR), in this way the controller is decoupled from its dependencies, making its injection and extension easier. 
MediatR working flow are based on:
  - The __request__ are sent to the __handler__.
  - It processes the request and generates a __response__.
  - There may be a broker who acts as __validator__ of the request.
* For logging, it uses [__Serilog__](https://serilog.net/), which allows to configure several outputs (in this case _console_ and _file_ have been used, but could have been extended to cloud solutions such as __Graylog__). In addition, a pipeline has been configured to write all the MediatR actions.
* __MayBe__ pattern used using [__Optional__](https://github.com/nlkl/Optional), in this way limit cases can be controlled easily.

* For tests, an integration test has been created to check workflow integrity, as well as unit tests for domains and acceptance tests for controllers.

* [__Hellang.Middleware.ProblemDetails__](https://github.com/khellang/Middleware) is used for error handling.
* [__ServiceStack__](https://github.com/ServiceStack/ServiceStack) is used for external Api connections.
* [__Swagger__](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) is used for the documentary management of the Api, as well as to provide an environment where to test it.
* [__Scrutor__](https://github.com/khellang/Scrutor) is used to scan all possible validators created.

* [__Autofixture__](https://github.com/AutoFixture/AutoFixture) is used to declare test cases.
* [__Bogus__](https://github.com/bchavez/Bogus) is used to generate random test data.
* [__FluentAssertions__](https://fluentassertions.com/) is used to help on the creation of test cases.
* [__NSubstitute__](https://nsubstitute.github.io/) is used to mocking the needed processes and services.

      
### Design

Solution implemented following __Clean__ arquitecture by layers, having:
- Infrastucture layer:
  - Proxy for external Api calls.
  - Repository for database queries.
  - Api features (Cross-cutting).
- Domain layer:
  - Entities.
  - Bussiness models.
- Aplication layer:
  - Controllers.
  - Services.
  - Api configuration.
  
This architecture allows the domain to be agnostic of the rest of the layers, having the infrastructure in the external layer.

Api versioning using specific __namespace__, that makes a new development or extends a functionality more simple.

Both context and entities obtained using [dotnet-ef](https://docs.microsoft.com/es-es/ef/core/managing-schemas/scaffolding) scaffolding utility, taking specific modifications to adapt the correct use.

### Conclusions

* It would have been interesting to create __ValueObjects__ for entities. Using it you can generate various additional validations on creation.
* [__Polly__](https://github.com/App-vNext/Polly) could have been used to control access on database and change entity framwework to [__Dapper__](https://stackexchange.github.io/Dapper/) in order to optimize the queries.
* Use two database contexts: one for reading and one for writing. In this way, data access and its (possible) manipulation can be better controlled.
* Sensitive data are exposed on configuration (appsettings). In that particular case there are no major security problems, but in a production environment we could have used a key store manager such as [__Vault__](https://www.vaultproject.io/) or one offered by [__Visual Studio__](https://docs.microsoft.com/es-es/aspnet/core/security/app-secrets), although this one does not get along very well with Docker or similars.

* Api deployed on Azure
* Github [action](https://github.com/ericbosch/Beezy.BackendTest/actions?query=workflow%3A%22.NET+Core+Build%22) created to automate compilation.
