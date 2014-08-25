using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudScale.Api.Models;
using CloudScale.Movies.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CloudScale.Api.Repositories
{
    public class AuthRepository : IDisposable
    {
        private readonly MoviesDataContext db;

        private readonly CloudScaleUserManager userManager;

        public AuthRepository(MoviesDataContext db, CloudScaleUserManager userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void Dispose()
        {
            db.Dispose();
            userManager.Dispose();
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            IdentityResult result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public OAuthClient FindClient(string clientId)
        {
            OAuthClient client = db.OAuthClients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(OAuthRefreshToken token)
        {
            OAuthRefreshToken existingToken =
                db.OAuthRefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId)
                    .SingleOrDefault();

            if (existingToken != null)
            {
                bool result = await RemoveRefreshToken(existingToken);
            }

            db.OAuthRefreshTokens.Add(token);

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            OAuthRefreshToken token = await db.OAuthRefreshTokens.FindAsync(refreshTokenId);

            if (token != null)
            {
                db.OAuthRefreshTokens.Remove(token);

                return await db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(OAuthRefreshToken OAuthRefreshToken)
        {
            db.OAuthRefreshTokens.Remove(OAuthRefreshToken);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<OAuthRefreshToken> FindRefreshToken(string tokenId)
        {
            OAuthRefreshToken OAuthRefreshToken = await db.OAuthRefreshTokens.FindAsync(tokenId);

            return OAuthRefreshToken;
        }

        public List<OAuthRefreshToken> GetAllRefreshTokens()
        {
            return db.OAuthRefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            IdentityResult result = await userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            IdentityResult result = await userManager.AddLoginAsync(userId, login);

            return result;
        }
    }
}