namespace BitsolMeritInSchoolSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatingexistingtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnualCharges",
                c => new
                    {
                        AnnualChargesId = c.Int(nullable: false, identity: true),
                        AnnualChargesDescription = c.String(nullable: false),
                        PaidAmount = c.Int(nullable: false),
                        RemainingChargesAmount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnnualChargesId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        FatherName = c.String(nullable: false),
                        ContactNo = c.String(nullable: false, maxLength: 14),
                        BayFormNo = c.String(maxLength: 13),
                        Date = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        StudentImage = c.Binary(),
                        ClassSectionId = c.Int(nullable: false),
                        Closed = c.Boolean(nullable: false),
                        Update = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .Index(t => t.ClassSectionId);
            
            CreateTable(
                "dbo.ClassSection",
                c => new
                    {
                        ClassSectionId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassSectionId)
                .ForeignKey("dbo.Class", t => t.ClassId)
                .ForeignKey("dbo.Section", t => t.SectionId)
                .Index(t => t.ClassId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        SectionName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.Attendence",
                c => new
                    {
                        AttendenceId = c.Int(nullable: false, identity: true),
                        ClassSectionId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AttendenceId)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.ClassSectionId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.DailyExpenditure",
                c => new
                    {
                        DailyExpenditureId = c.Int(nullable: false, identity: true),
                        ExpenditureDescription = c.String(nullable: false),
                        ExpenditureAmount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DailyExpenditureId);
            
            CreateTable(
                "dbo.MonthlyFee",
                c => new
                    {
                        MonthlyFeeId = c.Int(nullable: false, identity: true),
                        ClassSectionId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        FeeAmount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyFeeId)
                .ForeignKey("dbo.ClassSection", t => t.ClassSectionId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.ClassSectionId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.MonthlySalarySlip",
                c => new
                    {
                        MonthlySalarySlipId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CNIC = c.String(nullable: false, maxLength: 13),
                        Salary = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlySalarySlipId);
            
            CreateTable(
                "dbo.StaffMember",
                c => new
                    {
                        StaffMemberId = c.Int(nullable: false, identity: true),
                        StaffMemberName = c.String(nullable: false),
                        StaffMemberFatherName = c.String(nullable: false),
                        StaffMemberContactNo = c.String(nullable: false),
                        StaffMemberCNIC = c.String(nullable: false, maxLength: 13),
                        HiringDate = c.DateTime(nullable: false),
                        StaffMemberEmail = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Designation = c.String(nullable: false),
                        Qualification = c.String(),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaffMemberId);
            
            CreateTable(
                "dbo.StaffMemberAttendence",
                c => new
                    {
                        StaffMemberAttendenceId = c.Int(nullable: false, identity: true),
                        StaffMemberId = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        Date = c.DateTime(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        OutTime = c.DateTime(nullable: false),
                        ClassSection_ClassSectionId = c.Int(),
                    })
                .PrimaryKey(t => t.StaffMemberAttendenceId)
                .ForeignKey("dbo.ClassSection", t => t.ClassSection_ClassSectionId)
                .ForeignKey("dbo.StaffMember", t => t.StaffMemberId)
                .Index(t => t.StaffMemberId)
                .Index(t => t.ClassSection_ClassSectionId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffMemberAttendence", "StaffMemberId", "dbo.StaffMember");
            DropForeignKey("dbo.StaffMemberAttendence", "ClassSection_ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.MonthlyFee", "StudentId", "dbo.Student");
            DropForeignKey("dbo.MonthlyFee", "ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.Attendence", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Attendence", "ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.AnnualCharges", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Student", "ClassSectionId", "dbo.ClassSection");
            DropForeignKey("dbo.ClassSection", "SectionId", "dbo.Section");
            DropForeignKey("dbo.ClassSection", "ClassId", "dbo.Class");
            DropIndex("dbo.StaffMemberAttendence", new[] { "ClassSection_ClassSectionId" });
            DropIndex("dbo.StaffMemberAttendence", new[] { "StaffMemberId" });
            DropIndex("dbo.MonthlyFee", new[] { "StudentId" });
            DropIndex("dbo.MonthlyFee", new[] { "ClassSectionId" });
            DropIndex("dbo.Attendence", new[] { "StudentId" });
            DropIndex("dbo.Attendence", new[] { "ClassSectionId" });
            DropIndex("dbo.ClassSection", new[] { "SectionId" });
            DropIndex("dbo.ClassSection", new[] { "ClassId" });
            DropIndex("dbo.Student", new[] { "ClassSectionId" });
            DropIndex("dbo.AnnualCharges", new[] { "StudentId" });
            DropTable("dbo.User");
            DropTable("dbo.StaffMemberAttendence");
            DropTable("dbo.StaffMember");
            DropTable("dbo.MonthlySalarySlip");
            DropTable("dbo.MonthlyFee");
            DropTable("dbo.DailyExpenditure");
            DropTable("dbo.Attendence");
            DropTable("dbo.Section");
            DropTable("dbo.Class");
            DropTable("dbo.ClassSection");
            DropTable("dbo.Student");
            DropTable("dbo.AnnualCharges");
        }
    }
}
