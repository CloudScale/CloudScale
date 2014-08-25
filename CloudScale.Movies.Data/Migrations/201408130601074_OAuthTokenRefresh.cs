using System.Data.Entity.Migrations;

namespace CloudScale.Movies.Data.Migrations
{
    public partial class OAuthTokenRefresh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OAuthClients",
                c => new
                {
                    Id = c.String(false, 128),
                    Secret = c.String(false),
                    Name = c.String(false, 100),
                    ApplicationType = c.Int(false),
                    Active = c.Boolean(false),
                    RefreshTokenLifeTime = c.Int(false),
                    AllowedOrigin = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.OAuthRefreshTokens",
                c => new
                {
                    Id = c.String(false, 128),
                    Subject = c.String(false, 50),
                    ClientId = c.String(false, 50),
                    IssuedUtc = c.DateTime(false),
                    ExpiresUtc = c.DateTime(false),
                    ProtectedTicket = c.String(false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.OAuthRefreshTokens");
            DropTable("dbo.OAuthClients");
        }
    }
}