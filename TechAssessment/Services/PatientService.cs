using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessment.Controllers;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Services
{
    public class PatientService
    {
        public IEnumerable<Patient> GetAllPatients(tech_assessmentContext context)
        {
            //get all patients
            return context.Patients.ToList();
        }

        public Patient GetPatient(int id, tech_assessmentContext context)
        {
            //get a single patient by id
            Patient patient = context.Patients.Find(id);
            return patient;
        }

        public bool UpdatePatients(int id, string name, string insurance, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to edit
            Patient patient = context.Patients.Find(id);

            if (patient.PatientId == id)
            {
                patient.PatientName = name;
                patient.Insurance = insurance;
                context.Patients.Update(patient);
                var changes = context.SaveChanges();
                return changes == 1;
            }
            return false;
        }

        public bool InsertPatients(string name, string insurance, tech_assessmentContext context)
        {
            Patient patient = new Patient();

            patient.PatientName = name;
            patient.Insurance = insurance;
            //attempt to insert new patient if it fails returns false
            try
            {
                context.Patients.Add(patient);
                var changes = context.SaveChanges();
                return changes == 1;
            }
            catch
            {
                return false;
            }
        }

        internal benefitInformation GetBenefitInquiry(int id, int treatmentId, tech_assessmentContext context)
        {
            //We need a patient as well as a treatment plan to submit benefits verification
            benefitInformation information = new benefitInformation();
            Patient patient = context.Patients.Find(id);
            TreatmentPlan plan = context.TreatmentPlans.Find(treatmentId);
            information.PatientName = patient.PatientName;
            information.patientInsurance = patient.Insurance;
            information.treatmentPlan = plan;
            return information;
        }

        public bool DeletePatient(int id, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to delete
            Patient patient = context.Patients.Find(id);
            //find treatment plans matching that patient
            List<TreatmentPlan> treatmentPlans = new List<TreatmentPlan>();
            treatmentPlans = context.TreatmentPlans.Where(x => x.PatientId == patient.PatientId).ToList();
            //if there aren't any treatment plans we can skip forward
            TreatmentPlanService treatmentService = new TreatmentPlanService();
            if (treatmentPlans.Count > 0)
            {
                foreach (TreatmentPlan treatmentPlan in treatmentPlans)
                {
                    context = treatmentService.DeleteTreatmentPlanAssociations(context, treatmentPlan);
                }
            }
            //remove the patient
            context.Patients.Remove(patient);
            try
            {
                //attempt to commit the changes
                var changes = context.SaveChanges();
                //if the changes are equal to or greater than 1 the change was sucessful
                return changes >= 1;
            }
            catch
            {
                //if commit fails let the requester know
                return false;
            }
        }


    }
}
