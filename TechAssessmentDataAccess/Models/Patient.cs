using System;
using System.Collections.Generic;

#nullable disable

namespace TechAssessmentDataAccess.Models
{
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Insurance { get; set; }
    }
}
