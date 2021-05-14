using System;
using System.Collections.Generic;

#nullable disable

namespace TechAssessmentDataAccess.Models
{
    public partial class PriorAuthorization
    {
        public int PriorAuthorizationId { get; set; }
        public string Authorization { get; set; }
        public bool? Submitted { get; set; }
        public int TreatmentPlanId { get; set; }
    }
}
