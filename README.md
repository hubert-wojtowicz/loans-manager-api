# Loans Manager REST API
## Overwiev:
Get and save information about users change money loans.

## Main functionalities
Registering new user, getting token for user, creating loan, repaying loan, getting loan details, getting user details, listing users, listing borrowers, listing lenders, listing loans that belongs to user.

## API endpoints documentation:
* UI swagger doc of the API can be reached at `~/swagger`,
* JSON swagger doc of the API can be reach at address: `~/swagger/LoansApiV1/swagger.json`.

## Other stuff:
* HTTPS enabled and HTTP redirect to HTTPS,
* JWT token authorization implemented,
* authentication implemented to keep all borrower and lender safe,
* endpoints where collections are returned are secured with records limit to not allow for overload server,
* all comunicates are keep within designed Resources classes to easily localize api in the future.

## Repo:
The type of commit is contained within the title and can be one of these types:

* Feat: a new feature,
* Fix: a bug fix,
* Docs: changes to documentation,
* Style: formatting; no code change,
* Refactor: refactoring production code,
* Test: adding tests, refactoring test; no production code change,
* Config: changing of configurations.

## Technical overview:
* ASP .NET Core Web API 2.2,
* MS-SQL, EF Core, Code First approach.
