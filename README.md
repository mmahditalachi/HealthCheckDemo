# HealthCheckDemo

Getting started health check in Asp .net core 

## What is Health check ?

ASP.NET Core offers Health Checks Middleware and libraries for reporting the health of app infrastructure components.
For many apps, a basic health probe configuration that reports the app's availability to process requests (liveness) is sufficient to discover the status of the app.

## Health check UI

There is also a package that adds a monitoring UI that shows you the status of all the checks you added, as well as their history.

* [AspNetCore.HealthChecks.UI](https://www.nuget.org/packages/AspNetCore.HealthChecks.UI "package page link") 
* [AspNetCore.HealthChecks.UI.Client](https://www.nuget.org/packages/AspNetCore.HealthChecks.UI.Client/ "package page link") 
* [AspNetCore.HealthChecks.UI.InMemory.Storage](https://www.nuget.org/packages/AspNetCore.HealthChecks.UI.InMemory.Storage/ "package page link") 


To run the sample app for a given scenario, use the dotnet run command from the project's folder in a command shell

health check ui endpoint : /healthchecks-ui#/healthchecks

for more information check packages repo 
