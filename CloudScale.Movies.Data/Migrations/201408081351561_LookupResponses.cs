using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class LookupResponses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieLookupResults",
                c => new
                {
                    Id = c.Guid(false),
                    Data = c.String(),
                })
                .PrimaryKey(t => t.Id);

            DropColumn("dbo.Movies", "LookupResults");
        }

        public override void Down()
        {
            AddColumn("dbo.Movies", "LookupResults", c => c.String());
            DropTable("dbo.MovieLookupResults");
        }
    }
}