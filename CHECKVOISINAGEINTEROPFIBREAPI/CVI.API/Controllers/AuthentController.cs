using CVI.API.SendWork;
using CVI.Service.CVI.DTO;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CVI.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthentController : ControllerBase
    {
        #region PRIVATE PROPS

        /// <summary>
        /// The logger 
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AuthentController));

        /// <summary>
        /// httpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public AuthentController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// GetLoggedUser
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetLoggedUser()
        {
            try
            {
                UserDTO user = new UserDTO
                {
                    LastName = "FIT DEV",
                    FirstName = "BOUYGUES TELECOM",
                    Login = "fitdev",
                    Profil = "Administrateur",
                    UserId = 1
                };
                _httpContextAccessor.HttpContext.Session.SetString("LoggedUser", JsonConvert.SerializeObject(user));
                return  new ResponseMessage(user, null, true);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
    }
}
