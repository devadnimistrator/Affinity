namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClockData_Remove_SystemId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClockData", "SystemId", "dbo.AspNetUsers");
            DropIndex("dbo.ClockData", new[] { "SystemId" });
            DropColumn("dbo.ClockData", "SystemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClockData", "SystemId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ClockData", "SystemId");
            AddForeignKey("dbo.ClockData", "SystemId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
