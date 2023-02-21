using Amalia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Amalia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomingEmailController : ControllerBase
    {
        private readonly ILogger<IncomingEmailController> _logger;
        private DataService _dataService;
        private IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        public IncomingEmailController(ILogger<IncomingEmailController> logger,DataService dataService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _dataService = dataService;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string userName = string.Empty;
            string emailToAddresses = string.Join(",", Request.Form["To"]);
            var availableUsers = _dataService.GetAvailableUsers();
            string fromEmail = Request.Form["From"];
            foreach (var email in availableUsers)
            {
                var identity = emailToAddresses.ToLower().Trim().Contains(email.ToLower().Trim());
                if (identity)
                {
                    userName = email;
                }
            }
            if (userName == string.Empty)
            {
                foreach (var email in availableUsers)
                {
                    var identity = fromEmail.ToLower().Trim().Contains(email.ToLower().Trim());
                    if (identity)
                    {
                        userName = email;
                    }
                }
            }
            string emailSubject = Request.Form["Subject"];
            //_logger.LogTrace("To:"+string.Join(", ",emailToAddresses)+", From: "+ Request.Form["From"]+", Texto: "+ Request.Form["Text"]);
            try
            {
                await _dataService.SaveMessage(_configuration, emailSubject, Request.Form["Text"], fromEmail, emailToAddresses, userName);
            }
            catch (Exception ex)
            {
                throw new Exception("To:" + emailToAddresses + ", From: " + Request.Form["From"] + ", Texto: " + Request.Form["Text"]+", Username: "+userName);
            }
            
            return Ok();

        }
    }
}
