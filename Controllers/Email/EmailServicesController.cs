using System.Linq;
using System.Threading.Tasks;
using LivMoneyAPI.Data;
using LivMoneyAPI.Model.Email;
using LivMoneyAPI.Repository.AuthenticationRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivMoneyAPI.Controllers.Email {
    [ApiController]
    [Route ("api/[controller]")]
    public class EmailServicesController : ControllerBase {
        private readonly IEmailSender _emailSender;
        private readonly IAuthRepo _authrepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DataContext _context;
        public EmailServicesController (DataContext context, IAuthRepo authrepo, IEmailSender emailSender, IWebHostEnvironment hostingEnvironment) {
            _context = context;
            _emailSender = emailSender;
            _authrepo = authrepo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost ("SendMail")]
        public async Task<ActionResult> SendMail ([FromBody] EmailBody emailBody) {
            await _emailSender.SendEmailAsync (emailBody.useremail, "Verify LivMoney Account", emailBody.mailBody);
            return Ok (emailBody);
        }

        [HttpPost ("IsTokenMatch/{email}/{token}")]
        public async Task<bool> IsTokenMatch (string email, string token) {
            try {
                if (await _authrepo.IsTokenEmailExist (email, token))
                    return true;

                return false;
            } catch (System.Exception ex) {
               throw new System.Exception($"Token Match throw exception ${ex}");
            }
        }

        [HttpPost ("ConfirmEmail/{email}/{token}")]
        public async Task<bool> ConfirmEmail (string email, string token) {
            try {
                if (await _authrepo.IsTokenEmailExist (email, token)) {
                    var userToConfirmEmail = await _authrepo.getUserByToken (email, token);
                    userToConfirmEmail.IsEmailConfirm = true;
                    userToConfirmEmail.Token = null;
                    _context.AuthUsers.Update (userToConfirmEmail);
                    await _context.SaveChangesAsync ();
                    return true;
                }
                return false;
            } catch (System.Exception ex) {
                throw new System.Exception ($"Error has occurred durring email confirmation {ex}");
            }
        }
    }
}