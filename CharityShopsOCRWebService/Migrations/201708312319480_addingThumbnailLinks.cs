namespace CharityShopsOCRWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingThumbnailLinks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookTables", "ThumbnailLink", c => c.String());
            AddColumn("dbo.BookTables", "SmallThumbnailLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookTables", "SmallThumbnailLink");
            DropColumn("dbo.BookTables", "ThumbnailLink");
        }
    }
}
