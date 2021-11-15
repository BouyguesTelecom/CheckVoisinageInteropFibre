using CVI.Domain.Commun;
using CVI.Service.Authent.Contract;
using CVI.Service.Authent.DTO;
using CVI.Service.Authent.Request;
using log4net;
using System.Threading.Tasks;

namespace CVI.Service.Authent
{
    /// <summary>
    /// The AuthenticationService class
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AuthenticationService));

        /// <summary>
        /// Get user from AnnuaireFIT
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(AuthentUserDTO, ServiceMessage)> GetUser(GetLoggedUserRequest request)
        {
            ServiceMessage serviceMessage;

            if (request is null)
            {
                serviceMessage = new ServiceMessage
                {
                    ErrorMessage = "Param est pas valide.",
                    OperationSuccess = false
                };
                _logger.Error("The request is null.");
                return (null, serviceMessage);
            }

            return (new AuthentUserDTO
            {
                LastName = "BOUYGUES TELECOM",
                FirstName = "FIT DEV",
                Profil = "Administrateur"
            }, new ServiceMessage
            {
                OperationSuccess = true
            });
        }
    }
}
