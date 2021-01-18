# Code Challenge

## Front-End

The Front-End project which is located in `web/` directory was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.0.7. Before you start make sure the latest versions of Node.js and npm are installed on your machine.

This project utilises Lazy Loading for routes and implements PreloadAllModules strategy to load all modules for a faster application.

### Application Structure
Front-End project has 5 modules
- **AppModule:** *Main module*
- **SharedModule:** *All shared modules are imported and exported into/from this module. i.e: ReactiveFormsModule or any future 3rd party module (Angular Material components fo example).*
- **CoreModule:** *All application services which might be used in AppModule or in multiple modules.*
- **HomeModule:** *Routes, components and services specific to `/home`*
- **SalesModule:** *Routes, components and services specific to `/sales`*

### Application Configuration
The `assets/config.json` file constains all configuration required by the Front-End. The production deployment process should update this file with proper production values. This is the list of the available settings:

- **apiUrl**: The base URL for the back end (Default value is `http://localhost:5000`)

### Accessibility
The Front-End project is [WCAG](https://www.w3.org/WAI/standards-guidelines/wcag/) compliant and is designed to work with most screen readers.


### Packages
The required packages have been added by Angular CLI. The following 3rd party packages have been added to the application manually:
- **bootstrap:** *The popular front-end framework for building responsive, mobile-first sites and applications.*
- **ngx-toastr**: *For displaying toast messages.*

Run `npm i` to install all required packages for the project.

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io). This will automatically generate code coverage for the tests. You can find the coverage report in the `/coverage` directory.


### Lint

Run `ng lint` to make sure your code uses the best Type Script coding practices.

### Notes
- More tests can be added to increase the code coverage.
- Reactive Forms has been used to create the forms and the validations.

## Back-End

The Back-End solution is located in `Api/` directory. It uses .NET Core 3.1 and runs on `http://localhost:5000` by default. You can use the Swagger UI page which opens automatically when the application is run to test the Controllers and their Actions straight away without running the Front-End project. It uses the `Data.json` file as its read-only datasource.

### Overview
- .NET Core 3.1
- Repository Pattern using [EF Core](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core#using-a-custom-repository-versus-using-ef-dbcontext-directly)
- Asynchronous Methods
- Swagger (UI is available on `http://localhost:5000/swagger`)
- NLog (Log files are stored in `logs/` directory. See `nlog.config` file for details)
- Xunit for testing
- Unhandled Exception handling (`ExceptionHandler.cs`)
- Validation

### Application Structure

The solution has 4 projects.
- **Sales.Api:** *Controllers and their Actions, Application setup and Exception handling.*
- **Sales.Domain:** *DBContext, model classes and Enums.*
- **Sales.Services:** *Businnes logic and repositories*
- **Slaes.Services.Tests:** *Unit Tests for the Sales.Services services*

### Notes
- The JSON file gets loaded into the memory in the `SalesDbContext` constructor if it's available which gets mapped to the entities defined in the context. The test project does not have the JSON file, so the context will be empty there.
- GroupRepository and PersonRepository have tests. More tests can be added for the PersonRepository and Controllers to increase the code coverage.
- More detailed CORS implementation (This sample allows all Origins, Methods and Headers)