using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class ExtraMovieFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Year", c => c.Int(false));
            AddColumn("dbo.Movies", "BackdropPath", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Movies", "BackdropPath");
            DropColumn("dbo.Movies", "Year");
        }
    }
}