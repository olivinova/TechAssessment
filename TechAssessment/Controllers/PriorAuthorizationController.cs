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
    public class PriorAuthorizationController : ControllerBase
    {

        private PriorAuthorizationService service;

        public PriorAuthorizationController()
        {
            service = new PriorAuthorizationService();
        }

        [HttpGet]
        public IEnumerable<PriorAuthorization> Get()
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetAllPriorAuthorizations(context);
            }
        }

        [HttpGet]
        [Route("byid/{id}")]
        public PriorAuthorization GetById(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetPriorAuthorization(id, context);
            }
        }


        [HttpPost]
        public bool UpdatePriorAuthorization(PriorAuthorization priorAuthorization)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.UpdatePriorAuthorization(priorAuthorization.PriorAuthorizationId, priorAuthorization.TreatmentPlanId, priorAuthorization.Authorization, context);
            }
        }


        [HttpPut]
        public bool InsertPriorAuthorization(PriorAuthorization priorAuthorization)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.InsertPriorAuthorization(priorAuthorization.TreatmentPlanId, priorAuthorization.Authorization, context);

            }
        }


        [HttpDelete]
        public bool DeletePriorAuthorization(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.DeletePriorAuthorization(id, context);
            }
        }

    }
}
