namespace MMA1.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RosterTableAddUserRef : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rosters", "System_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Rosters", "EmployerName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Rosters", "SiteName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Rosters", "SiteAddress", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Rosters", "SiteDept", c => c.String(maxLength: 20));
            AlterColumn("dbo.Rosters", "Position", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Rosters", "EmployeeEmail", c => c.String(nullable: false, maxLength: 80));
            CreateIndex("dbo.Rosters", "System_Id");
            AddForeignKey("dbo.Rosters", "System_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);

            CreateIndex("dbo.Rosters", new[] { "System_Id", "DateTimeOn", "SiteName" });
            CreateIndex("dbo.Rosters", new [] { "EmployeeEmail", "DateTimeOn" });
        }
        
        public override void Down()
        {

            DropIndex("dbo.Rosters", new [] { "System_Id", "DateTimeOn", "SiteName" });
            DropIndex("dbo.Rosters", new [] { "EmployeeEmail", "DateTimeOn" });

            DropForeignKey("dbo.Rosters", "System_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Rosters", new[] { "System_Id" });
            AlterColumn("dbo.Rosters", "EmployeeEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Rosters", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.Rosters", "SiteDept", c => c.String());
            AlterColumn("dbo.Rosters", "SiteAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Rosters", "SiteName", c => c.String(nullable: false));
            AlterColumn("dbo.Rosters", "EmployerName", c => c.String(nullable: false));
            DropColumn("dbo.Rosters", "System_Id");
        }
    }
}
