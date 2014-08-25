using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class MovieScores1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieScores", "UserId", c => c.Guid());
            DropColumn("dbo.MovieScores", "MovieName");
            DropColumn("dbo.MovieScores", "PersonName");
        }

        public override void Down()
        {
            AddColumn("dbo.MovieScores", "PersonName", c => c.String());
            AddColumn("dbo.MovieScores", "MovieName", c => c.String());
            DropColumn("dbo.MovieScores", "UserId");
        }
    }
}