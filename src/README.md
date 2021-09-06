# Summary of changes
- Extracting interfaces and using them instead of dependencies, for ease of future DI or mocking.
- Refactoring code.
- Introducing tests (.NET 5.0).
- Splitting solution into smaller projects (.NET 4.5.2 Framework).


# Some ideas for further improvements
- Update solution & project files to .NET 5.0 (tests are ready).
- Resolve dependencies with DI, phase out default constructor.
- Refactor UnicodeFileToHtmlTextConverter namespace - namespace is same as class name. Breaking change for one of the dependencies.