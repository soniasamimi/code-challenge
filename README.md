# Code Challenge

## Front-End

The Front-End project which is located in `web/` directory was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.0.7. Before you start make, sure the latest versions of Node.js and npm are installed on your machine.

### Packages
Run `npm i` to install all required packages for the project. 

### Application Configuration
The `assets/config.json` file constains all configuration required by the Front-End. The production deployment process should update this file with proper production values. This is the list of the available settings:

- **apiUrl**: The base URL for the back end (Default value is `http://localhost:5000`)

### Accessibility
The Front-End project is [WCAG](https://www.w3.org/WAI/standards-guidelines/wcag/) complianat and is designed to work with most screen readers.

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io). This will automatically generate code coverage for the tests. You can find the coverage report in the `/coverage` directory.

### Lint

Run `ng lint` to make sure your code uses the best Type Script coding practices.

## Back-End

The Back-End solution is located in `Api/` directory. It uses .NET Core 3.1 and runs on `http://localhost:5000` by default. You can use the Swagger UI page which opens automatically when the application is run to test the Controllers and their Actions straight away without running the Front-End project. It uses the `Data.json` file as its read-only datasource.

A quick summary of the used modules and implemented features:
- .NET Core 3.1
- EF Core (InMemory provider)
- Swagger (UI is available on `http://localhost:5000/swagger`)
- NLog (Log files are stored in `logs/` directory. See `nlog.config` file for details)
- Xunit for testing
- Unhandled Exception handling (`ExceptionHandler.cs`)


Areas of improvement:
- More tests can be added to increase the code coverage
- More detailed CORS implementation
