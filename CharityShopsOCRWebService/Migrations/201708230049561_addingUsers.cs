namespace CharityShopsOCRWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BookTables", "user_ID", c => c.Int());
            CreateIndex("dbo.BookTables", "user_ID");
            AddForeignKey("dbo.BookTables", "user_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookTables", "user_ID", "dbo.Users");
            DropIndex("dbo.BookTables", new[] { "user_ID" });
            DropColumn("dbo.BookTables", "user_ID");
            DropTable("dbo.Users");
        }
    }
}
