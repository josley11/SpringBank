# SpringBank
 Service to check credit eligibility

The solution is called SpringBank. It includes an ASP.NET code web API (REST service) project. It also includes the test project called SpringBank.testing

The application launches with a Swagger (in Google Chrome) which exposes the post REST API for credit search where it can be tried out. 

Postman can be used to test the service. The credit service can be accessed using the url:
https://localhost:<portnumber>/api/credit/creditsearch (for eg: https://localhost:44379/api/credit/creditsearch)

The request body should contain a sample JSON as follows:
{
    "AppliedAmount": 2000,
    "term": 3,
    "PreExistingCredit": 40000
}

For the above-mentioned valid request, the service returns a response with a JSON content as below:
{
    "isEligibleForCredit": true,
    "interestRate": 5
}

The service should return a decision and an interest rate percentage, based on the following rules:

-------------------------------------------------------------------------
| Applied amount 	|	<2000 	|	>2000 	|	>69000	|
-------------------------------------------------------------------------
| Decision 		|	No 	|	Yes 	|	No	|
-------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------
| Total future debt 	|	<20000	|	20000-39000	|	40000-59000	|	>60000	|
---------------------------------------------------------------------------------------------------------
| Interest rate % 	|	3 	|	4 		|	5 		|	6	|
---------------------------------------------------------------------------------------------------------