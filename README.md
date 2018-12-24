# LoansManagerRestApi
## Overwiev:
Get and save information about change money loans.

## API endpoints:
### Basic
* Listing all borrower `~/loans/getBorrowersLimited` GET,
* Listing all lender `~/loans/getLendersLimited` GET,
* Listing all loans in context of specified user `~/loans/getUsersLoansLimited/{userId}` GET,
* Adding new user  `~/users/register` POST,
* Adding new loan `~/loans` POST,
* Repaying the loan `~/loans/repay` POST.

### Additional
* Get loan by its key `~/loans/get/{id}` GET,
* Get user by its key `~/users/get/{userName}` GET,
* List all users `~/users/getLimited` GET,
* Get token for specified user `~/users/auth` POST.

## Other stuff:
* HTTPS enabled and HTTP redirect to HTTPS,
* JWT token authorization implemented,
* authentication implemented to keep all borrower and lender safe,
* endpoints where collections are returned are secured with records limit to not allow for overload server,
* All comunicates are keep within designed Resources classes to easily localize api in the future.

## Technical overview:
* ASP .NET Core Web API 2.2,
* MS-SQL, EF Code First approach.

## Repo:
The type of commit is contained within the title and can be one of these types:

* Feat: a new feature,
* Fix: a bug fix,
* Docs: changes to documentation,
* Style: formatting; no code change,
* Refactor: refactoring production code,
* Test: adding tests, refactoring test; no production code change,
* Config: changing of configurations.
