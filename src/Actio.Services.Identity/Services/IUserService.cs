using System.Threading.Tasks;
using Actio.Common.Auth;

namespace Actio.Services.Identity.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string name, string email, string password);
        Task<JsonWebToken> LoginAsync(string email, string password);
    }
}