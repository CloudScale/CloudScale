namespace CloudScale.Movies.DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
