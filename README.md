# Loans Manager REST API
## Overwiev:
Get and save information about change money loans.

## API endpoints:
### Basic
* Listing all borrower `~/api/loans/getBorrowersLimited` GET,
* listing all lender `~/api/loans/getLendersLimited` GET,
* listing all loans in context of specified user `~/api/loans/getUsersLoansLimited/{userId}` GET,
* adding new user  `~/api/users/register` POST,
* adding new loan `~/api/loans` POST,
* repaying the loan `~/api/loans/repay` POST.

### Additional
* Get loan by its key `~/api/loans/get/{id}` GET,
* get user by its key `~/api/users/get/{userName}` GET,
* list all users `~/api/users/getLimited` GET,
* get token for specified user `~/api/users/auth` POST.

## Other stuff:
* HTTPS enabled and HTTP redirect to HTTPS,
* JWT token authorization implemented,
* authentication implemented to keep all borrower and lender safe,
* endpoints where collections are returned are secured with records limit to not allow for overload server,
* all comunicates are keep within designed Resources classes to easily localize api in the future.

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
