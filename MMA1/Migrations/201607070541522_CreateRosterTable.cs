namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRosterTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rosters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmployerId = c.Guid(nullable: false),
                        EmployerName = c.String(nullable: false),
                        SiteName = c.String(nullable: false),
                        SiteAddress = c.String(nullable: false),
                        SiteDept = c.String(),
                        Position = c.String(nullable: false),
                        EmployeeEmail = c.String(nullable: false),
                        DateTimeOn = c.DateTime(nullable: false),
                        DateTimeOff = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rosters");
        }
    }
}
