namespace ToDoListTeldat.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Item_updated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Day");
        }

        public override void Down()
        {
            AddColumn("dbo.Items", "Day", c => c.DateTime(nullable: false));
        }
    }
}
