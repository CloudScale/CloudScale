using CloudScale.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CloudScale.Movies.Data;

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

        public async Task<IdentityResult> RegisterUser(RegisterUserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public OAuthClient FindClient(string clientId)
        {
            var client = db.OAuthClients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(OAuthRefreshToken token)
        {

            var existingToken = db.OAuthRefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            db.OAuthRefreshTokens.Add(token);

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var token = await db.OAuthRefreshTokens.FindAsync(refreshTokenId);

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
            var OAuthRefreshToken = await db.OAuthRefreshTokens.FindAsync(tokenId);

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
            var result = await userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            db.Dispose();
            userManager.Dispose();

        }
    }
}