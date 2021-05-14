using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechAssessmentDataAccess.Models
{
    public partial class tech_assessmentContext : DbContext
    {
        public tech_assessmentContext()
        {
        }

        public tech_assessmentContext(DbContextOptions<tech_assessmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EligibilityResponse> EligibilityResponses { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PriorAuthorization> PriorAuthorizations { get; set; }
        public virtual DbSet<TreatmentPlan> TreatmentPlans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=techassessment.database.windows.net;Initial Catalog=tech_assessment;User ID=adm;Password=J-08-65-w;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EligibilityResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId);

                entity.ToTable("eligibility_responses");

                entity.Property(e => e.ResponseId)
                    .ValueGeneratedNever()
                    .HasColumnName("response_id");

                entity.Property(e => e.Response)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("response");

                entity.Property(e => e.TreatmentPlanId).HasColumnName("treatment_plan_id");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patients");

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("patient_id");

                entity.Property(e => e.Insurance)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("insurance");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("patient_name");
            });

            modelBuilder.Entity<PriorAuthorization>(entity =>
            {
                entity.ToTable("prior_authorizations");

                entity.Property(e => e.PriorAuthorizationId)
                    .ValueGeneratedNever()
                    .HasColumnName("prior_authorization_id");

                entity.Property(e => e.Authorization)
                    .IsUnicode(false)
                    .HasColumnName("authorization");

                entity.Property(e => e.Submitted).HasColumnName("submitted");

                entity.Property(e => e.TreatmentPlanId).HasColumnName("treatment_plan_id");
            });

            modelBuilder.Entity<TreatmentPlan>(entity =>
            {
                entity.ToTable("treatment_plans");

                entity.Property(e => e.TreatmentPlanId)
                    .ValueGeneratedNever()
                    .HasColumnName("treatment_plan_id");

                entity.Property(e => e.Diagnosis)
                    .IsUnicode(false)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.Drug)
                    .IsUnicode(false)
                    .HasColumnName("drug");

                entity.Property(e => e.Jcode)
                    .IsUnicode(false)
                    .HasColumnName("jcode");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Service)
                    .IsUnicode(false)
                    .HasColumnName("service");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
