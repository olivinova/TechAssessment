using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechAssessment.Services;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EligibilityResponseController : ControllerBase
    {
        private EligibilityResponseService service;

        public EligibilityResponseController()
        {
            service = new EligibilityResponseService();
        }

        [HttpGet]
        public IEnumerable<EligibilityResponse> Get()
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetAllEligibilityResponses(context);
            }
        }

        [HttpGet]
        [Route("byid/{id}")]
        public EligibilityResponse GetById(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetEligibilityResponse(id, context);
            }
        }

        [HttpPost]
        public bool UpdateEligibilityResponse(EligibilityResponse eligibilityResponse)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.UpdateEligibilityResponse(eligibilityResponse.ResponseId, eligibilityResponse.TreatmentPlanId, 
                    eligibilityResponse.Response, context);
            }
        }

        [HttpGet]
        [Route("getinfo/{id}/{authorizationId}/")]
        public ElegibilityResponseInformation GetInformationToSubmitPriorAuthorization(int id, int authorizationId)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetInformationToSubmitPriorAuthorization(id, authorizationId, context);
            }
        }

        [HttpPut]
        public bool InsertEligibilityResponse(EligibilityResponse eligibilityResponse)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.InsertEligibilityResponse(eligibilityResponse.TreatmentPlanId, eligibilityResponse.Response, context);

            }
        }


        [HttpDelete]
        public bool DeleteEligibilityResponse(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.DeleteEligibilityResponse(id, context);
            }
        }

    }

    public class ElegibilityResponseInformation
    {
        public EligibilityResponse EligabilityResponse;
        public string authorization;
    }
}
