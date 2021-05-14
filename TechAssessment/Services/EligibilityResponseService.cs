using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessment.Controllers;
using TechAssessmentDataAccess.Models;

namespace TechAssessment.Services
{
    public class EligibilityResponseService
    {

        public IEnumerable<EligibilityResponse> GetAllEligibilityResponses(tech_assessmentContext context)
        {
            //get all eligibility responses
            return context.EligibilityResponses.ToList();
        }

        public EligibilityResponse GetEligibilityResponse(int id, tech_assessmentContext context)
        {
            //get a single Eligibility Response by id
            EligibilityResponse eligibilityResponse = context.EligibilityResponses.Find(id);
            return eligibilityResponse;
        }

        public bool UpdateEligibilityResponse(int id, int treatmentId, string response, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to edit
            EligibilityResponse eligibilityResponse = context.EligibilityResponses.Find(id);

            if (eligibilityResponse.ResponseId == id)
            {
                eligibilityResponse.TreatmentPlanId = treatmentId;
                eligibilityResponse.Response = response;
                context.EligibilityResponses.Update(eligibilityResponse);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            return false;
        }

        public bool InsertEligibilityResponse(int treatmentId, string response, tech_assessmentContext context)
        {
            EligibilityResponse eligibilityResponse = new EligibilityResponse();

            eligibilityResponse.TreatmentPlanId = treatmentId;
            eligibilityResponse.Response = response;
            //attempt to insert new response if it fails returns false
            try
            {
                context.EligibilityResponses.Add(eligibilityResponse);
                var changes = context.SaveChanges();
                //if the changes are equal to 1 the change was sucessful
                return changes == 1;
            }
            catch
            {
                return false;
            }
        }

        public ElegibilityResponseInformation GetInformationToSubmitPriorAuthorization(int id, int authorizationId, tech_assessmentContext context)
        {

            //we need both an eligability response and an authorization document to submit prior authorization
            ElegibilityResponseInformation information = new ElegibilityResponseInformation();
            EligibilityResponse response = context.EligibilityResponses.Find(id);
            PriorAuthorization authorization = context.PriorAuthorizations.Find(authorizationId);
            information.EligabilityResponse = response;
            information.authorization = authorization.Authorization;
            return information;
        }

        public bool DeleteEligibilityResponse(int id, tech_assessmentContext context)
        {
            //make sure it exsists before attempting to delete

            EligibilityResponse eligibilityResponse = new EligibilityResponse();
            eligibilityResponse = context.EligibilityResponses.Find(id);

            context.EligibilityResponses.Remove(eligibilityResponse);
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
