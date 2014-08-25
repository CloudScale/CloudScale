using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class MovieLinkInScoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieScores", "MovieId", c => c.Guid(true));
            CreateIndex("dbo.MovieScores", "MovieId");
            AddForeignKey("dbo.MovieScores", "MovieId", "dbo.Movies", "Id", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.MovieScores", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieScores", new[] {"MovieId"});
            DropColumn("dbo.MovieScores", "MovieId");
        }
    }
}