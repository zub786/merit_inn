using BitsolMeritInSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.DAL
{
    public class ContextClasss : DbContext
    {
        public ContextClasss()
            : base("ContextClasss")
        {

        }
        public DbSet<Student> Student { get; set; }

        public DbSet<Class> Class { get; set; }

        public DbSet<DailyExpenditure> DailyExpenditure { get; set; }
        public DbSet<MonthlyFee> MonthlyFee { get; set; }

        public DbSet<MonthlySalarySlip> MonthlySalarySlip { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<StaffMember> StaffMember { get; set; }

        public DbSet<Attendence> Attendence { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<StaffMemberAttendence> StaffMemberAttendence { get; set; }

        public DbSet<ClassSection> ClassSections { get; set; }

     

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

       

        internal void SubmitChanges()
        {
            throw new NotImplementedException();
        }

        public System.Data.Entity.DbSet<BitsolMeritInSchoolSystem.Models.AnnualCharges> AnnualCharges { get; set; }
    }
}