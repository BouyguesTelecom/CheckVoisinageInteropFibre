using CVI.Domain.Commun;
using CVI.Service.Authent.DTO;
using CVI.Service.Authent.Request;
using System.Threading.Tasks;

namespace CVI.Service.Authent.Contract
{
    public interface IAuthenticationService
    {
        Task<(AuthentUserDTO, ServiceMessage)> GetUser(GetLoggedUserRequest request);
    }
}
