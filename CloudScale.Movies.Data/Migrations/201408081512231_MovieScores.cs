namespace CloudScale.Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieScores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieScores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeLogged = c.DateTimeOffset(nullable: false, precision: 7),
                        MovieName = c.String(),
                        PersonName = c.String(),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieScores");
        }
    }
}
