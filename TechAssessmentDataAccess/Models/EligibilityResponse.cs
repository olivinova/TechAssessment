using System;
using System.Collections.Generic;

#nullable disable

namespace TechAssessmentDataAccess.Models
{
    public partial class EligibilityResponse
    {
        public int ResponseId { get; set; }
        public int TreatmentPlanId { get; set; }
        public string Response { get; set; }
    }
}
