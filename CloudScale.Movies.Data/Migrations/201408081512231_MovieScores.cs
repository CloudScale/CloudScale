using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class MovieScores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieScores",
                c => new
                {
                    Id = c.Guid(false),
                    TimeLogged = c.DateTimeOffset(false, 7),
                    MovieName = c.String(),
                    PersonName = c.String(),
                    Score = c.Double(false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.MovieScores");
        }
    }
}