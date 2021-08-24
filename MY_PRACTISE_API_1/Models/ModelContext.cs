using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MY_PRACTISE_API_1.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));Persist Security Info=True;User Id=projectadmin1;Password=oracle;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PROJECTADMIN1")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(o => o.CompanyId);

                entity.ToTable("COMPANY");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COMPANY_ID")
                    .HasDefaultValueSql("PROJECTADMIN1.COMPANY_ID_SEQ.NEXTVAL ");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(i => i.DepartmentId);

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DEPARTMENT_ID")
                    .HasDefaultValueSql("PROJECTADMIN1.DEPARTMENT_ID_SEQ.NEXTVAL ");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEPARTMENT_NAME");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_COMPANY");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(o => o.DesignationId);

                entity.ToTable("DESIGNATION");

                entity.Property(e => e.DesignationId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DESIGNATION_ID")
                    .HasDefaultValueSql("PROJECTADMIN1.DESIGNATION_ID_SEQ.NEXTVAL ");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DesignationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESIGNATION_NAME");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Designations)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_DEPARTMENT");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(i => i.EmployeeId);

                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EMPLOYEE_ID")
                    .HasDefaultValueSql("PROJECTADMIN1.EMPLOYEE_ID_SEQ.NEXTVAL ");

                entity.Property(e => e.ActiveStatus)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ACTIVE_STATUS")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.DesignationId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DESIGNATION_ID");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYEE_NAME");

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("DATE")
                    .HasColumnName("JOINING_DATE")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.Salary)
                    .HasColumnType("FLOAT")
                    .HasColumnName("SALARY")
                    .HasDefaultValueSql("0 ");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK_DESIGNATION");
            });

            modelBuilder.HasSequence("COMPANY_ID_SEQ");

            modelBuilder.HasSequence("DEPARTMENT_ID_SEQ");

            modelBuilder.HasSequence("DESIGNATION_ID_SEQ");

            modelBuilder.HasSequence("EMPLOYEE_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
