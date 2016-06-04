namespace SellYourStuff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Region : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionCode = c.String(),
                        RegionName = c.String(),
                        ApplicationUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "region_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "region_Id");
            AddForeignKey("dbo.AspNetUsers", "region_Id", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "region_Id", "dbo.Regions");
            DropIndex("dbo.AspNetUsers", new[] { "region_Id" });
            DropColumn("dbo.AspNetUsers", "region_Id");
            DropTable("dbo.Regions");
        }
    }
}
