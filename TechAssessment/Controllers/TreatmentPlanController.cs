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
    public class TreatmentPlanController : Controller
    {

        private TreatmentPlanService treatmentService;

        public TreatmentPlanController()
        {
            treatmentService = new TreatmentPlanService();
        }

        [HttpGet]
        public IEnumerable<TreatmentPlan> Get()
        {
            using (var context = new tech_assessmentContext())
            {
                return treatmentService.GetAllTreatmentPlans(context);
            }
        }

        [HttpGet]
        [Route("byid/{id}")]
        public TreatmentPlan GetById(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return treatmentService.GetTreatmentPlan(id, context);
            }
        }

        [HttpPost]
        public bool UpdateTreatmentPlan(TreatmentPlan treamentPlan)
        {
            using (var context = new tech_assessmentContext())
            {
                return treatmentService.UpdateTreatmentPlan(treamentPlan.TreatmentPlanId, treamentPlan.PatientId, treamentPlan.Diagnosis,
                    treamentPlan.Service, treamentPlan.Drug, treamentPlan.Jcode, context);
            }
        }

        [HttpPut]
        public bool InsertTreatmentPlan(TreatmentPlan treamentPlan)
        {
            using (var context = new tech_assessmentContext())
            {
                return treatmentService.InsertTreatmentPlan(treamentPlan.PatientId, treamentPlan.Diagnosis, treamentPlan.Service,
                    treamentPlan.Drug, treamentPlan.Jcode, context);

            }
        }


        [HttpDelete]
        public bool DeleteTreatmentPlan(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return treatmentService.DeleteTreatmentPlan(id, context);
            }
        }

    }
}
