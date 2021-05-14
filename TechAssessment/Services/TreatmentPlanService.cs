using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Services
{
    public class TreatmentPlanService
    {
        public IEnumerable<TreatmentPlan> GetAllTreatmentPlans(tech_assessmentContext context)
        {
            //get all prior authorizations
            return context.TreatmentPlans.ToList();
        }

        public TreatmentPlan GetTreatmentPlan(int id, tech_assessmentContext context)
        {
            //get a single Treatment Plan by id
            TreatmentPlan treatmentPlan = context.TreatmentPlans.Find(id);
            return treatmentPlan;
        }

        public bool UpdateTreatmentPlan(int id, int patientId, string diagnosis, string service, string drug, string jcode, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to edit
            TreatmentPlan treatmentPlan = context.TreatmentPlans.Find(id);

            if (treatmentPlan != null)
            {
                treatmentPlan.PatientId = patientId;
                treatmentPlan.Diagnosis = diagnosis;
                treatmentPlan.Service = service;
                treatmentPlan.Drug = drug;
                treatmentPlan.Jcode = jcode;
                context.TreatmentPlans.Update(treatmentPlan);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }

            return false;
        }

        public bool InsertTreatmentPlan(int patientId, string diagnosis, string service, string drug, string jcode, tech_assessmentContext context)
        {
            TreatmentPlan treatmentPlan = new TreatmentPlan();


            treatmentPlan.PatientId = patientId;
            treatmentPlan.Diagnosis = diagnosis;
            treatmentPlan.Service = service;
            treatmentPlan.Drug = drug;
            treatmentPlan.Jcode = jcode;
            //attempt to insert new response if it fails returns false
            try
            {
                context.TreatmentPlans.Add(treatmentPlan);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTreatmentPlan(int id, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to delete

            TreatmentPlan treatmentPlan = new TreatmentPlan();
            treatmentPlan = context.TreatmentPlans.Find(id);
            DeleteTreatmentPlanAssociations(context, treatmentPlan);
            try
            {
                //attempt to commit the changes
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            catch
            {
                //if commit fails let the requester know
                return false;
            }
        }

        //used in patient deletion as well so no associations end up hanging around after attaching records are removed
        public tech_assessmentContext DeleteTreatmentPlanAssociations(tech_assessmentContext context, TreatmentPlan treatmentPlan)
        {
            //find prior authorizations tied to each treatment plan then delete them
            List<PriorAuthorization> priorAuthorizations = new List<PriorAuthorization>();
            priorAuthorizations = context.PriorAuthorizations.Where(x => x.TreatmentPlanId == treatmentPlan.TreatmentPlanId).ToList();
            if (priorAuthorizations.Count > 0)
            {
                foreach (PriorAuthorization priorAuthorization in priorAuthorizations)
                {
                    context.PriorAuthorizations.Remove(priorAuthorization);
                }
            }

            //find Eligibility responses tied to each treatment plan then delete them
            List<EligibilityResponse> eligibilityResponses = new List<EligibilityResponse>();
            eligibilityResponses = context.EligibilityResponses.Where(x => x.TreatmentPlanId == treatmentPlan.TreatmentPlanId).ToList();
            if (eligibilityResponses.Count > 0)
            {
                foreach (EligibilityResponse eligibilityResponse in eligibilityResponses)
                {
                    context.EligibilityResponses.Remove(eligibilityResponse);
                }
            }
            //remove treatment plan
            context.TreatmentPlans.Remove(treatmentPlan);
            return context;
        }
    }
}
