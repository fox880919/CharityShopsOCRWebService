namespace CharityShopsOCRWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAuthorsAndCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookTables", "Authors", c => c.String());
            AddColumn("dbo.BookTables", "Categories", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookTables", "Categories");
            DropColumn("dbo.BookTables", "Authors");
        }
    }
}
