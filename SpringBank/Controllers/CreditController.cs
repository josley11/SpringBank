using System;
using Microsoft.AspNetCore.Mvc;
using SpringBank.Models;
using Microsoft.AspNetCore.Http;

namespace SpringBank.Controllers
{
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditDecisionWorker _creditDecisionWorker;

        public CreditController( ICreditDecisionWorker creditDecisionWorker)
        {
           _creditDecisionWorker = creditDecisionWorker;
        }        

        /// <summary>
        /// Consumes a credit search request and decides on credit eligibilty and interest rate
        /// </summary>
        [HttpPost, Route("api/credit/creditsearch")]
        public IActionResult CreditSearch([FromBody] CreditSearchRequest searchRequest)
        {
            try
            {
                return _creditDecisionWorker.IsValidRequest(searchRequest, out var message)
                    ? Ok(_creditDecisionWorker.GetCreditDecision(searchRequest))
                    : StatusCode(StatusCodes.Status400BadRequest, message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }           
    }
}
