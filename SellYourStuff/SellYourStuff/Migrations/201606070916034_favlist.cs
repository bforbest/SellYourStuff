namespace SellYourStuff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavLists",
                c => new
                    {
                        FavListId = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FavListId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            AddColumn("dbo.Products", "FavList_FavListId", c => c.Int());
            CreateIndex("dbo.Products", "FavList_FavListId");
            AddForeignKey("dbo.Products", "FavList_FavListId", "dbo.FavLists", "FavListId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "FavList_FavListId", "dbo.FavLists");
            DropForeignKey("dbo.FavLists", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.FavLists", new[] { "ApplicationUserID" });
            DropIndex("dbo.Products", new[] { "FavList_FavListId" });
            DropColumn("dbo.Products", "FavList_FavListId");
            DropTable("dbo.FavLists");
        }
    }
}
