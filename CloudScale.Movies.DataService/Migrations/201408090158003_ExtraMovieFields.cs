namespace CloudScale.Movies.DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraMovieFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "BackdropPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "BackdropPath");
            DropColumn("dbo.Movies", "Year");
        }
    }
}
