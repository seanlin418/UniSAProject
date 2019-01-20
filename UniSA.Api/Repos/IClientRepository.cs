using UniSA.Api.Models.AppClients;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace UniSA.Api.Repos
{
    public interface IClientRepository
    {
        Client FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        IEnumerable<RefreshToken> GetAllRefreshTokens();
    }
}