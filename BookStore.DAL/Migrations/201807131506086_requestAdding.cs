namespace BookStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requestAdding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerToSellerRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerToSellerRequests", "UserId", "dbo.Users");
            DropIndex("dbo.CustomerToSellerRequests", new[] { "UserId" });
            DropTable("dbo.CustomerToSellerRequests");
        }
    }
}
