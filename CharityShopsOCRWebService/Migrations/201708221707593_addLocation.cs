namespace CharityShopsOCRWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookTables", "ShelfLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookTables", "ShelfLocation");
        }
    }
}
