using RestSharp;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SpringBank.Models;

namespace SpringBank.Testing.Credit.Client
{
    internal sealed class CreditServiceClientTests
    {
        [TestClass]
        public class CreditService
        {
            [TestMethod]
            public void GivenInvalidCreditSearchRequest_WhenInvokedAsRestService_ThenTheServiceReturnsAnBadRequestResponse()
            {
                // Arrange
                var creditSearchRequest = CreateCreditSearchRequest(100M, 0, 0M);
                var expectedResultContent = "Invalid term provided . Term provided : 0";

                var restClient = new RestClient("https://localhost:44379/api/credit/creditsearch");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(creditSearchRequest);

                // Act
                var actualResponse = restClient.Execute(request);

                // Assert
                Assert.AreEqual(HttpStatusCode.BadRequest, actualResponse.StatusCode);
                Assert.IsTrue(actualResponse.Content.Contains(expectedResultContent));
            }

            [TestMethod]
            public void GivenValidCreditSearchRequest_WhenInvokedAsRestService_ThenTheServiceReturnsAnOkRequestResponse()
            {
                // Arrange
                var appliedAmount = 2000M;
                var term = 4;
                var preExistingCredit = 0M;

                var creditSearchRequest = CreateCreditSearchRequest(appliedAmount, term, preExistingCredit);
                CreditDecision expectedCreditDEcision = CreateExpectedCreditDecision(isEligibleForCredit: true, interestRate: 3);

                var restClient = new RestClient("https://localhost:44379/api/credit/creditsearch");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(creditSearchRequest);

                // Act
                var actualResponse = restClient.Execute(request);

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, actualResponse.StatusCode);
                Assert.AreNotEqual(0, actualResponse.ContentLength);
                var actualcreditDecision = JsonConvert.DeserializeObject<CreditDecision>(actualResponse.Content);
                IsExpectedCreditDecision(expectedCreditDEcision, actualcreditDecision);
            }

            [TestMethod]
            public void GivenValidCreditSearchAsJson_WhenInvokedAsRestService_ThenExpectedResponseReturned()
            {
                //Arrange
                var restClient = new RestClient("https://localhost:44379/api/credit/");
                RestRequest request = new("creditsearch", Method.POST);
                request.AddJsonBody(@"{
                                            ""AppliedAmount"": 2000,
                                            ""term"": 1,
                                            ""PreExistingCredit"": 37500
                                    }");

                var expectedResponse = @"{""isEligibleForCredit"":true,""interestRate"":4}";

                //Act
                var response = restClient.Execute(request);

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(expectedResponse, response.Content);
            }

            [TestMethod]
            public void GivenCreditSearchLessThanMinimumRequired_WhenInvokedAsRestService_ThenExpectedResponseReturned()
            {
                //Arrange
                var restClient = new RestClient("https://localhost:44379/api/credit/");
                RestRequest request = new("creditsearch", Method.POST);
                request.AddJsonBody(@"{
                                            ""AppliedAmount"": 1000,
                                            ""term"": 1,
                                            ""PreExistingCredit"": 37500
                                    }");

                var expectedResponse = @"{""isEligibleForCredit"":false,""interestRate"":null}";

                //Act
                var response = restClient.Execute(request);

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(expectedResponse, response.Content);
            }

            [TestMethod]
            public void GivenInValidCreditSearch_WhenInvokedAsRestService_ThenExpectedResponseReturned()
            {
                //Arrange
                var restClient = new RestClient("https://localhost:44379/api/credit/");
                RestRequest request = new("creditsearch", Method.POST);
                request.AddJsonBody(@"{
                                            ""AppliedAmount"": -2000,
                                            ""term"": 1,
                                            ""PreExistingCredit"": 37500
                                    }");

                var expectedResponse = "\"Invalid credit amount requested . Amount provided : -2000\"";

                //Act
                var response = restClient.Execute(request);

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(expectedResponse, response.Content);
            }
        }

        private static CreditSearchRequest CreateCreditSearchRequest(decimal appliedAmount = 0M, int term = 1, decimal preExistingCredit = 0M)
        {
            return new CreditSearchRequest
            {
                AppliedAmount = appliedAmount,
                Term = term,
                PreExistingCredit = preExistingCredit
            };
        }

        private static CreditDecision CreateExpectedCreditDecision(bool isEligibleForCredit, int interestRate)
        {
            return new CreditDecision
            {
                IsEligibleForCredit = isEligibleForCredit,
                InterestRate = interestRate
            };
        }

        private static void IsExpectedCreditDecision(CreditDecision expectedCreditDEcision, CreditDecision actualcreditDecision)
        {
            Assert.AreEqual(expectedCreditDEcision.IsEligibleForCredit, actualcreditDecision.IsEligibleForCredit);
            Assert.AreEqual(expectedCreditDEcision.InterestRate, actualcreditDecision.InterestRate);
        }
    }
}
