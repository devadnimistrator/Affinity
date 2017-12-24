namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rosters", name: "System_Id", newName: "SystemId");
            RenameIndex(table: "dbo.Rosters", name: "IX_System_Id", newName: "IX_SystemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rosters", name: "IX_SystemId", newName: "IX_System_Id");
            RenameColumn(table: "dbo.Rosters", name: "SystemId", newName: "System_Id");
        }
    }
}
