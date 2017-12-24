namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClockData_Table1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClockData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemId = c.String(nullable: false, maxLength: 128),
                        RosterId = c.Guid(nullable: false),
                        TimeOn = c.DateTime(nullable: false),
                        LocationOnX = c.Double(nullable: false),
                        LocationOnY = c.Double(nullable: false),
                        TimeOff = c.DateTime(),
                        LocationOffX = c.Double(nullable: false),
                        LocationOffY = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rosters", t => t.RosterId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.SystemId, cascadeDelete: false)
                .Index(t => t.SystemId)
                .Index(t => new { t.RosterId, t.TimeOn }, name: "IX_RosterTimeOn");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClockData", "SystemId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClockData", "RosterId", "dbo.Rosters");
            DropIndex("dbo.ClockData", "IX_RosterTimeOn");
            DropIndex("dbo.ClockData", new[] { "SystemId" });
            DropTable("dbo.ClockData");
        }
    }
}
