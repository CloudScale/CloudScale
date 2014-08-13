namespace CloudScale.Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LookupImportFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "TMDBId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "OriginalTitle", c => c.String());
            AddColumn("dbo.Movies", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Movies", "PosterPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "PosterPath");
            DropColumn("dbo.Movies", "Rating");
            DropColumn("dbo.Movies", "OriginalTitle");
            DropColumn("dbo.Movies", "TMDBId");
        }
    }
}
