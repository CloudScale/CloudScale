namespace CloudScale.Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieLinkInScoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieScores", "MovieId", c => c.Guid(nullable: true));
            CreateIndex("dbo.MovieScores", "MovieId");
            AddForeignKey("dbo.MovieScores", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieScores", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieScores", new[] { "MovieId" });
            DropColumn("dbo.MovieScores", "MovieId");
        }
    }
}
