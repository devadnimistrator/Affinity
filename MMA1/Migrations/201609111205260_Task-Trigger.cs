namespace MMA1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskTrigger : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE TRIGGER [dbo].[Task_AU]
   ON  [dbo].[Task] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update  dbo.Task 
	set updatedOn=getDate() 
	where id in (select id from inserted)
    -- Insert statements for trigger here

END");
        }
        
        public override void Down()
        {
            Sql("drop TRIGGER [dbo].[Task_AU];");
        }
    }
}
