using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Services
{
    public class PriorAuthorizationService
    {

        public IEnumerable<PriorAuthorization> GetAllPriorAuthorizations(tech_assessmentContext context)
        {
            //get all prior authorizations
            return context.PriorAuthorizations.ToList();
        }

        public PriorAuthorization GetPriorAuthorization(int id, tech_assessmentContext context)
        {
            //get a single Prior Authorization by id
            PriorAuthorization priorAuthorization = context.PriorAuthorizations.Find(id);
            return priorAuthorization;
        }

        public bool UpdatePriorAuthorization(int id, int treatmentId, string authorization, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to edit
            PriorAuthorization priorAuthorization = context.PriorAuthorizations.Find(id);

            if (priorAuthorization.PriorAuthorizationId == id)
            {
                priorAuthorization.TreatmentPlanId = treatmentId;
                priorAuthorization.Authorization = authorization;
                context.PriorAuthorizations.Update(priorAuthorization);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            return false;
        }

        public bool InsertPriorAuthorization(int treatmentId, string authorization, tech_assessmentContext context)
        {
            PriorAuthorization priorAuthorization = new PriorAuthorization();

            priorAuthorization.TreatmentPlanId = treatmentId;
            priorAuthorization.Authorization = authorization;
            //attempt to insert new response if it fails returns false
            try
            {
                context.PriorAuthorizations.Add(priorAuthorization);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePriorAuthorization(int id, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to delete

            PriorAuthorization priorAuthorization = new PriorAuthorization();
            priorAuthorization = context.PriorAuthorizations.Find(id);

            context.PriorAuthorizations.Remove(priorAuthorization);
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
    }
}
