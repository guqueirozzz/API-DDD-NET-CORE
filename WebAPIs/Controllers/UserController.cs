using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;
using WebAPIs.Token;

namespace WebAPIs.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            var resultado = await
                _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                // Recupera o usuario logado
                var user = new ApplicationUser
                {
                    UserName = login.email,
                    Email = login.email,
                };

                var currentUser = await _userManager.FindByEmailAsync(login.email);
                var userId = currentUser.Id;

                var token = new TokenJWTBuilder()
                   .AddSecurityKey(JWTSecurityKey.Create("Secret_Key-12345678"))
               .AddSubject("Empresa - Canal Dev Net Core")
               .AddIssuer("Teste.Securiry.Bearer")
               .AddAudience("Teste.Securiry.Bearer")
               .AddClaim("idUsuario", userId)
               .AddExpireInMinutes(5)
               .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
