namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClockData_Table2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClockData", "LocationOnX", c => c.Double());
            AlterColumn("dbo.ClockData", "LocationOnY", c => c.Double());
            AlterColumn("dbo.ClockData", "LocationOffX", c => c.Double());
            AlterColumn("dbo.ClockData", "LocationOffY", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClockData", "LocationOffY", c => c.Double(nullable: false));
            AlterColumn("dbo.ClockData", "LocationOffX", c => c.Double(nullable: false));
            AlterColumn("dbo.ClockData", "LocationOnY", c => c.Double(nullable: false));
            AlterColumn("dbo.ClockData", "LocationOnX", c => c.Double(nullable: false));
        }
    }
}
