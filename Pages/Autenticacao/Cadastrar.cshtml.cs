using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetBasics.Pages
{
    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage = "Nome obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Usuario obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Senha obrigatória!")]
        [DataType(DataType.Password)]
        [BindProperty(SupportsGet = true)]
        public string Senha { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"INSERT INTO usuario (nome,username,senha) VALUES ('{Nome}','{Usuario}','{Senha}')";

            await mySqlCommand.ExecuteReaderAsync();

            await mySqlConnection.CloseAsync();

            return new JsonResult(new { Msg = "Usuario cadastrado com sucesso!", Url = Url.Page("/home") });
        }
    }
}
