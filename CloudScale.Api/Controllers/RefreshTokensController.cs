using System;
using System.Threading.Tasks;
using System.Web.Http;
using CloudScale.Api.Repositories;

namespace CloudScale.Api.Controllers
{
    [RoutePrefix("refreshtokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly AuthRepository repo;

        public RefreshTokensController(AuthRepository repo)
        {
            if (repo == null) throw new ArgumentNullException("repo");

            this.repo = repo;
        }

        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(repo.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            bool result = await repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}