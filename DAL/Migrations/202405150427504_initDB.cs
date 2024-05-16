namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        CommentedBy = c.String(maxLength: 128),
                        RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId)
                .ForeignKey("dbo.Users", t => t.CommentedBy)
                .Index(t => t.CommentedBy)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Ingredient = c.String(nullable: false),
                        PostedBy = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.PostedBy)
                .Index(t => t.PostedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Uname = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 20),
                        Type = c.String(nullable: false),
                        Comment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Uname)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TKey = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "CommentedBy", "dbo.Users");
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "PostedBy", "dbo.Users");
            DropIndex("dbo.Tokens", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Comment_Id" });
            DropIndex("dbo.Recipes", new[] { "PostedBy" });
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            DropIndex("dbo.Comments", new[] { "CommentedBy" });
            DropTable("dbo.Tokens");
            DropTable("dbo.Users");
            DropTable("dbo.Recipes");
            DropTable("dbo.Comments");
        }
    }
}
