using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class NameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieLookupResults", "Name", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.MovieLookupResults", "Name");
        }
    }
}