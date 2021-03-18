namespace ToDoListTeldat.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class item_updated_again : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "IsDone");
        }

        public override void Down()
        {
            AddColumn("dbo.Items", "IsDone", c => c.Boolean(nullable: false));
        }
    }
}
