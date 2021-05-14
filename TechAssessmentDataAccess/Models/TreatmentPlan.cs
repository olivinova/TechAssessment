using System;
using System.Collections.Generic;

#nullable disable

namespace TechAssessmentDataAccess.Models
{
    public partial class TreatmentPlan
    {
        public int TreatmentPlanId { get; set; }
        public string Diagnosis { get; set; }
        public string Service { get; set; }
        public string Drug { get; set; }
        public string Jcode { get; set; }
        public int PatientId { get; set; }
    }
}
