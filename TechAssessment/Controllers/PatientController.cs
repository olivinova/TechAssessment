using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechAssessment.Services;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {

        private PatientService service;

        public PatientController()
        {
            service = new PatientService();
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetAllPatients(context);
            }
        }

        [HttpGet]
        [Route("test")]
        public string test()
        {
            return "server up";
        }

        [HttpGet]
        [Route("byid/{id}")]
        public Patient GetById(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetPatient(id, context);
            }
        }

        [HttpGet]
        [Route("benefitinquiry/{id}/{treatmentId}/")]
        public benefitInformation GetBenefitInquiryInformation(int id, int treatmentId)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.GetBenefitInquiry(id, treatmentId, context);
            }
        }

        [HttpPost]
        public bool UpdatePatient([FromBody]Patient patient)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.UpdatePatients(patient.PatientId, patient.PatientName, patient.Insurance, context);
            }
        }


        [HttpPut]
        public bool InsertPatient([FromBody]Patient patient)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.InsertPatients(patient.PatientName, patient.Insurance, context);

            }
        }


        [HttpDelete]
        public bool DeletePatient(int id)
        {
            using (var context = new tech_assessmentContext())
            {
                return service.DeletePatient(id, context);
            }
        }

    }
    public class benefitInformation
    {
        public TreatmentPlan treatmentPlan;
        public string patientInsurance;
        public string PatientName;
    }
}
