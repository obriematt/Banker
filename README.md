# Bank Ledger Sample

Bank Ledger Sample is three separate projects for a Bank Ledger Sample.

  - Banker : Web Api use .Net Core 2.2
  - BankConsoleApp : Self hosted console application of the BankService
  - BankerTest : Unit testing for the BankService

# Banker

  - A basic Web Api that includes Swagger/Swashbuckle for documentation and testing.
  - Performs basic operations of the BankService.
  - Two API routes for usage through a Manager or Regular account. Difference between the two not implemented.
  - Regular account allows for Withdrawal, Deposit, Transaction History, and Account Information.
  - Manager account allows for Creation and Deletion of Accounts, Bank Transaction history, and viewing all Accounts.
  - Running the Banker project will direct to a Swagger documentation page to allow for testing of the API.

# BankConsoleApp

  - A console application of the BankService. 
  - Reads in user input from a basic selection to perform the operations of the BankService.
  - Basic user inputs include: Viewing account information and balance, withdrawal or deposit into account, transaction history for the account, creating a new account, and logging out.
  - The log out function is only a call to exit the User Selection loop.
  - The Console Application will loop until exiting.
  - Account usernames must be unique.
  - All credentials are stored in plain text. Implementation of an SSO and encryption was outside the scope of the sample.

# BankerTest

  - Basic unit testing to validate the BankService is functioning properly.
