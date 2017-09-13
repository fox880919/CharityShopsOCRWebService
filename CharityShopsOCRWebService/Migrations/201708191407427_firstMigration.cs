namespace CharityShopsOCRWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Isbn = c.String(),
                        Title = c.String(),
                        publisher = c.String(),
                        DatePublished = c.String(),
                        PageCount = c.Double(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookTables");
        }
    }
}
