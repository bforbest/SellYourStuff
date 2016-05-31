namespace SellYourStuff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagesender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Subject", c => c.String());
            AddColumn("dbo.Messages", "SenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "SenderId");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropColumn("dbo.Messages", "SenderId");
            DropColumn("dbo.Messages", "Subject");
        }
    }
}
