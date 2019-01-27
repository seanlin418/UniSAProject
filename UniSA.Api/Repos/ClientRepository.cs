using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UniSA.Data;
using UniSA.Data.AppClients;

namespace UniSA.Api.Repos
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        private ApplicationDbContext _ctx;

        public ClientRepository()
        {
            this._ctx = new ApplicationDbContext();
        }

        public ClientRepository(ApplicationDbContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<bool> AddClient(Client newClient)
        {
            var clientExisted = _ctx.Clients.FirstOrDefault(w => w.Name == newClient.Name && w.AllowedOrigin == newClient.AllowedOrigin && w.Active == true);

            if (clientExisted == null)
            {
                _ctx.Clients.Add(newClient);
                return await _ctx.SaveChangesAsync() > 0;
            }
            return await Task.FromResult<bool>(false);
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public IEnumerable<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            this._ctx.Dispose();
        }
    }
}