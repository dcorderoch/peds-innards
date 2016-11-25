namespace MyLearnDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievement",
                c => new
                    {
                        AchievementId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AchievementId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Group = c.Int(nullable: false),
                        ProfessorId = c.Guid(nullable: false),
                        UniversityId = c.Guid(nullable: false),
                        MinScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Professor", t => t.ProfessorId)
                .ForeignKey("dbo.University", t => t.UniversityId, cascadeDelete: false)
                .Index(t => t.ProfessorId)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false, maxLength: 15),
                        Photo = c.Binary(),
                        InDate = c.DateTime(nullable: false),
                        TRepo = c.Int(nullable: false),
                        IsActive = c.Int(nullable: false),
                        CountryId = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: false)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: false)
                .Index(t => t.Email, unique: true)
                .Index(t => t.CountryId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.University",
                c => new
                    {
                        UniversityId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UniversityId);
            
            CreateTable(
                "dbo.Bid",
                c => new
                    {
                        BidId = c.Guid(nullable: false),
                        JobOfferId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.JobOffer", t => t.JobOfferId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.JobOfferId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.JobOffer",
                c => new
                    {
                        JobOfferId = c.Guid(nullable: false),
                        EmployerId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobOfferId)
                .ForeignKey("dbo.Employer", t => t.UserId)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.JobOfferComment",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        JobOfferId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Comment = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.JobOffer", t => t.JobOfferId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.JobOfferId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tecnology",
                c => new
                    {
                        TecnologyId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        JobOffer_JobOfferId = c.Guid(),
                        Project_ProjectId = c.Guid(),
                        Student_UserId = c.Guid(),
                        Course_CourseId = c.Guid(),
                    })
                .PrimaryKey(t => t.TecnologyId)
                .ForeignKey("dbo.JobOffer", t => t.JobOffer_JobOfferId)
                .ForeignKey("dbo.Project", t => t.Project_ProjectId)
                .ForeignKey("dbo.Student", t => t.Student_UserId)
                .ForeignKey("dbo.Course", t => t.Course_CourseId)
                .Index(t => t.JobOffer_JobOfferId)
                .Index(t => t.Project_ProjectId)
                .Index(t => t.Student_UserId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LenguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Student_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.LenguageId)
                .ForeignKey("dbo.Student", t => t.Student_UserId)
                .Index(t => t.Student_UserId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        NotificationId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Message = c.String(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        IsActive = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        BadgeId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        AchievementId = c.Guid(nullable: false),
                        Bragged = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BadgeId)
                .ForeignKey("dbo.Achievement", t => t.AchievementId, cascadeDelete: false)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.AchievementId);
            
            CreateTable(
                "dbo.ProjectComment",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Comment = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CourseAchievement",
                c => new
                    {
                        AchievementId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AchievementId, t.CourseId })
                .ForeignKey("dbo.Achievement", t => t.AchievementId, cascadeDelete: false)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .Index(t => t.AchievementId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.CourseTechnology",
                c => new
                    {
                        TechnologyId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnologyId, t.CourseId })
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Tecnology", t => t.TechnologyId, cascadeDelete: false)
                .Index(t => t.TechnologyId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.JobOfferTechnology",
                c => new
                    {
                        JobOfferId = c.Guid(nullable: false),
                        TechnologyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobOfferId, t.TechnologyId })
                .ForeignKey("dbo.JobOffer", t => t.JobOfferId, cascadeDelete: false)
                .ForeignKey("dbo.Tecnology", t => t.TechnologyId, cascadeDelete: false)
                .Index(t => t.JobOfferId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.ProjectTechnology",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        TechnologyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.TechnologyId })
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Tecnology", t => t.TechnologyId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentLanguage",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LanguageId })
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.StudentTechnology",
                c => new
                    {
                        TechnologyId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnologyId, t.UserId })
                .ForeignKey("dbo.Student", t => t.UserId)
                .ForeignKey("dbo.Tecnology", t => t.TechnologyId, cascadeDelete: false)
                .Index(t => t.TechnologyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StudentCourse1",
                c => new
                    {
                        Student_UserId = c.Guid(nullable: false),
                        Course_CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_UserId, t.Course_CourseId })
                .ForeignKey("dbo.Student", t => t.Student_UserId, cascadeDelete: false)
                .ForeignKey("dbo.Course", t => t.Course_CourseId, cascadeDelete: false)
                .Index(t => t.Student_UserId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.Employer",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                        ContactName = c.String(nullable: false, maxLength: 30),
                        Website = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Professor",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                        Schedule = c.String(nullable: false),
                        UniversityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.University", t => t.UniversityId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        CardId = c.String(nullable: false),
                        RepoLink = c.String(),
                        ResumeLink = c.String(),
                        UniversityId = c.Guid(nullable: false),
                        AvgProjects = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgCourses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumSuceedCourses = c.Int(nullable: false),
                        NumFailedCourses = c.Int(nullable: false),
                        NumSuceedProjects = c.Int(nullable: false),
                        NumFailedProjects = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.University", t => t.UniversityId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.UniversityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "UniversityId", "dbo.University");
            DropForeignKey("dbo.Student", "UserId", "dbo.User");
            DropForeignKey("dbo.Professor", "UniversityId", "dbo.University");
            DropForeignKey("dbo.Professor", "UserId", "dbo.User");
            DropForeignKey("dbo.Employer", "UserId", "dbo.User");
            DropForeignKey("dbo.StudentTechnology", "TechnologyId", "dbo.Tecnology");
            DropForeignKey("dbo.StudentTechnology", "UserId", "dbo.Student");
            DropForeignKey("dbo.StudentLanguage", "UserId", "dbo.Student");
            DropForeignKey("dbo.StudentLanguage", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.StudentCourse", "UserId", "dbo.Student");
            DropForeignKey("dbo.StudentCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.ProjectTechnology", "TechnologyId", "dbo.Tecnology");
            DropForeignKey("dbo.ProjectTechnology", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.JobOfferTechnology", "TechnologyId", "dbo.Tecnology");
            DropForeignKey("dbo.JobOfferTechnology", "JobOfferId", "dbo.JobOffer");
            DropForeignKey("dbo.CourseTechnology", "TechnologyId", "dbo.Tecnology");
            DropForeignKey("dbo.CourseTechnology", "CourseId", "dbo.Course");
            DropForeignKey("dbo.CourseAchievement", "CourseId", "dbo.Course");
            DropForeignKey("dbo.CourseAchievement", "AchievementId", "dbo.Achievement");
            DropForeignKey("dbo.Achievement", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "UniversityId", "dbo.University");
            DropForeignKey("dbo.Tecnology", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.Tecnology", "Student_UserId", "dbo.Student");
            DropForeignKey("dbo.Tecnology", "Project_ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "UserId", "dbo.Student");
            DropForeignKey("dbo.ProjectComment", "UserId", "dbo.User");
            DropForeignKey("dbo.ProjectComment", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Badge", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Badge", "AchievementId", "dbo.Achievement");
            DropForeignKey("dbo.Notification", "UserId", "dbo.Student");
            DropForeignKey("dbo.Language", "Student_UserId", "dbo.Student");
            DropForeignKey("dbo.StudentCourse1", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.StudentCourse1", "Student_UserId", "dbo.Student");
            DropForeignKey("dbo.Bid", "UserId", "dbo.Student");
            DropForeignKey("dbo.Bid", "JobOfferId", "dbo.JobOffer");
            DropForeignKey("dbo.Tecnology", "JobOffer_JobOfferId", "dbo.JobOffer");
            DropForeignKey("dbo.JobOffer", "UserId", "dbo.Student");
            DropForeignKey("dbo.JobOfferComment", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.User", "CountryId", "dbo.Country");
            DropForeignKey("dbo.JobOfferComment", "JobOfferId", "dbo.JobOffer");
            DropForeignKey("dbo.JobOffer", "UserId", "dbo.Employer");
            DropForeignKey("dbo.Course", "ProfessorId", "dbo.Professor");
            DropIndex("dbo.Student", new[] { "UniversityId" });
            DropIndex("dbo.Student", new[] { "UserId" });
            DropIndex("dbo.Professor", new[] { "UniversityId" });
            DropIndex("dbo.Professor", new[] { "UserId" });
            DropIndex("dbo.Employer", new[] { "UserId" });
            DropIndex("dbo.StudentCourse1", new[] { "Course_CourseId" });
            DropIndex("dbo.StudentCourse1", new[] { "Student_UserId" });
            DropIndex("dbo.StudentTechnology", new[] { "UserId" });
            DropIndex("dbo.StudentTechnology", new[] { "TechnologyId" });
            DropIndex("dbo.StudentLanguage", new[] { "LanguageId" });
            DropIndex("dbo.StudentLanguage", new[] { "UserId" });
            DropIndex("dbo.StudentCourse", new[] { "CourseId" });
            DropIndex("dbo.StudentCourse", new[] { "UserId" });
            DropIndex("dbo.ProjectTechnology", new[] { "TechnologyId" });
            DropIndex("dbo.ProjectTechnology", new[] { "ProjectId" });
            DropIndex("dbo.JobOfferTechnology", new[] { "TechnologyId" });
            DropIndex("dbo.JobOfferTechnology", new[] { "JobOfferId" });
            DropIndex("dbo.CourseTechnology", new[] { "CourseId" });
            DropIndex("dbo.CourseTechnology", new[] { "TechnologyId" });
            DropIndex("dbo.CourseAchievement", new[] { "CourseId" });
            DropIndex("dbo.CourseAchievement", new[] { "AchievementId" });
            DropIndex("dbo.ProjectComment", new[] { "UserId" });
            DropIndex("dbo.ProjectComment", new[] { "ProjectId" });
            DropIndex("dbo.Badge", new[] { "AchievementId" });
            DropIndex("dbo.Badge", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "CourseId" });
            DropIndex("dbo.Project", new[] { "UserId" });
            DropIndex("dbo.Notification", new[] { "UserId" });
            DropIndex("dbo.Language", new[] { "Student_UserId" });
            DropIndex("dbo.Tecnology", new[] { "Course_CourseId" });
            DropIndex("dbo.Tecnology", new[] { "Student_UserId" });
            DropIndex("dbo.Tecnology", new[] { "Project_ProjectId" });
            DropIndex("dbo.Tecnology", new[] { "JobOffer_JobOfferId" });
            DropIndex("dbo.JobOfferComment", new[] { "UserId" });
            DropIndex("dbo.JobOfferComment", new[] { "JobOfferId" });
            DropIndex("dbo.JobOffer", new[] { "UserId" });
            DropIndex("dbo.Bid", new[] { "UserId" });
            DropIndex("dbo.Bid", new[] { "JobOfferId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "CountryId" });
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.Course", new[] { "UniversityId" });
            DropIndex("dbo.Course", new[] { "ProfessorId" });
            DropIndex("dbo.Achievement", new[] { "CourseId" });
            DropTable("dbo.Student");
            DropTable("dbo.Professor");
            DropTable("dbo.Employer");
            DropTable("dbo.StudentCourse1");
            DropTable("dbo.StudentTechnology");
            DropTable("dbo.StudentLanguage");
            DropTable("dbo.StudentCourse");
            DropTable("dbo.ProjectTechnology");
            DropTable("dbo.JobOfferTechnology");
            DropTable("dbo.CourseTechnology");
            DropTable("dbo.CourseAchievement");
            DropTable("dbo.ProjectComment");
            DropTable("dbo.Badge");
            DropTable("dbo.Project");
            DropTable("dbo.Notification");
            DropTable("dbo.Language");
            DropTable("dbo.Tecnology");
            DropTable("dbo.JobOfferComment");
            DropTable("dbo.JobOffer");
            DropTable("dbo.Bid");
            DropTable("dbo.University");
            DropTable("dbo.Role");
            DropTable("dbo.Country");
            DropTable("dbo.User");
            DropTable("dbo.Course");
            DropTable("dbo.Achievement");
        }
    }
}
