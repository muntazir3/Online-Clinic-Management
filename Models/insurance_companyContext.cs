using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EPROJECT.Models
{
    public partial class insurance_companyContext : DbContext
    {
        
        public insurance_companyContext()
        {
        }

        public insurance_companyContext(DbContextOptions<insurance_companyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLogin> AdminLogins { get; set; } = null!;
        public virtual DbSet<CompanyDetail> CompanyDetails { get; set; } = null!;
        public virtual DbSet<EmpRegister> EmpRegisters { get; set; } = null!;
        public virtual DbSet<PoliciesOnEmployee> PoliciesOnEmployees { get; set; } = null!;
        public virtual DbSet<Policy> Policies { get; set; } = null!;
        public virtual DbSet<PolicyApprovalDetail> PolicyApprovalDetails { get; set; } = null!;
        public virtual DbSet<PolicyRequestDetail> PolicyRequestDetails { get; set; } = null!;
        public virtual DbSet<PolicyTotalDescription> PolicyTotalDescriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9JHNJIS\\SQLEXPRESS;Database=insurance_company;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__admin_lo__F3DBC573417D2BF5");

                entity.ToTable("admin_login");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<CompanyDetail>(entity =>
            {
                entity.HasKey(e => e.Companyid)
                    .HasName("PK__company___AD5755B88B92673A");

                entity.ToTable("company_details");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.CompanyUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyURL");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<EmpRegister>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("PK__emp_regi__AF2EBFA148142D4D");

                entity.ToTable("emp_register");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Contactno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contactno");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasColumnName("join_date");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Salary)
                    .HasColumnType("money")
                    .HasColumnName("salary");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<PoliciesOnEmployee>(entity =>
            {
                entity.HasKey(e => new { e.Empid, e.Policyid })
                    .HasName("PK__policies__18A0B571A5283986");

                entity.ToTable("policies_on_employees");

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyname");

                entity.Property(e => e.Emi)
                    .HasColumnType("money")
                    .HasColumnName("emi");

                entity.Property(e => e.Penddate)
                    .HasColumnType("datetime")
                    .HasColumnName("penddate");

                entity.Property(e => e.Policyamount)
                    .HasColumnType("money")
                    .HasColumnName("policyamount");

                entity.Property(e => e.Policyduration)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("policyduration");

                entity.Property(e => e.Policyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("policyname");

                entity.Property(e => e.Pstartdate)
                    .HasColumnType("datetime")
                    .HasColumnName("pstartdate");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.PoliciesOnEmployees)
                    .HasForeignKey(d => d.Empid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__policies___Empid__52593CB8");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PoliciesOnEmployees)
                    .HasForeignKey(d => d.Policyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__policies___polic__534D60F1");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("policies");

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Emi)
                    .HasColumnType("money")
                    .HasColumnName("emi");

                entity.Property(e => e.Medicalid).HasColumnName("medicalid");

                entity.Property(e => e.Policydesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("policydesc");

                entity.Property(e => e.Policyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("policyname");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.Companyid)
                    .HasConstraintName("FK__policies__compan__4D94879B");
            });

            modelBuilder.Entity<PolicyApprovalDetail>(entity =>
            {
                entity.HasKey(e => e.Policyid)
                    .HasName("PK__policy_a__78E0AD0A76CE3E5F");

                entity.ToTable("policy_approval_details");

                entity.Property(e => e.Policyid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("policyid");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Status)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.HasOne(d => d.Policy)
                    .WithOne(p => p.PolicyApprovalDetail)
                    .HasForeignKey<PolicyApprovalDetail>(d => d.Policyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__policy_ap__polic__5629CD9C");
            });

            modelBuilder.Entity<PolicyRequestDetail>(entity =>
            {
                entity.HasKey(e => e.Requestid)
                    .HasName("PK__policy_r__E3C6D249672961F6");

                entity.ToTable("policy_request_details");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyname");

                entity.Property(e => e.Emi)
                    .HasColumnType("money")
                    .HasColumnName("emi");

                entity.Property(e => e.Policyamount)
                    .HasColumnType("money")
                    .HasColumnName("policyamount");

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Policyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("policyname");

                entity.Property(e => e.Requestdate)
                    .HasColumnType("datetime")
                    .HasColumnName("requestdate");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.PolicyRequestDetails)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("FK__policy_re__Empid__59063A47");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyRequestDetails)
                    .HasForeignKey(d => d.Policyid)
                    .HasConstraintName("FK__policy_re__polic__59FA5E80");
            });

            modelBuilder.Entity<PolicyTotalDescription>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("policy_total_description");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("companyname");

                entity.Property(e => e.Emi)
                    .HasColumnType("money")
                    .HasColumnName("emi");

                entity.Property(e => e.Policyamount)
                    .HasColumnType("money")
                    .HasColumnName("policyamount");

                entity.Property(e => e.Policydes)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("policydes");

                entity.Property(e => e.Policydurationmonths).HasColumnName("policydurationmonths");

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Policyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("policyname");

                entity.HasOne(d => d.Policy)
                    .WithMany()
                    .HasForeignKey(d => d.Policyid)
                    .HasConstraintName("FK__policy_to__polic__5BE2A6F2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
