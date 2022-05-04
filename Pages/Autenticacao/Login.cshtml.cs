using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DotNetBasics.Pages
{
    public class LoginModel : PageModel
    {
        [Required(ErrorMessage = "Usuario obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [Required(ErrorMessage ="Senha obrigatoria!")]
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ManterLogado { get; set; }

        
        public void OnGet()
        {   
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM usuario WHERE  username = '{Username}' AND senha = '{Senha}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                int usuarioId = reader.GetInt32(0);
                string nome = reader.GetString(1);

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
                    new Claim(ClaimTypes.Name, nome)

                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync("Identity.Login", userPrincipal, new AuthenticationProperties
                {
                    IsPersistent = ManterLogado,
                    ExpiresUtc = System.DateTime.Now.AddHours(1)
                });
                return new JsonResult(new { Msg = "Usuario logado com sucesso!", Url = Url.Page("/home") });
            }

            await mySqlConnection.CloseAsync();


            return new JsonResult(new { Msg = "Usuario não encontrado", Url = Url.Page("/home") });
        }


    }
}
