using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DotNetBasics.Pages.Usuarios
{
    public class editarModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Usuario obrigatório!")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }


        public async Task OnGet()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM usuario WHERE  id = '{Id}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                Nome = reader.GetString(1);
                Usuario = reader.GetString(2);
            }

            await mySqlConnection.CloseAsync();


        }

        public async Task<IActionResult> OnPost()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"UPDATE usuario SET username = '{Usuario}', nome = '{Nome}' WHERE Id = '{Id}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
                      
            await mySqlConnection.CloseAsync();

            return new JsonResult(new { Msg = "Usuario editado com sucesso!", Url = Url.Page("/usuarios/usuarios") });

        }

        public async Task<IActionResult> OnGetApagar()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"DELETE FROM usuario WHERE  id = '{Id}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            

            await mySqlConnection.CloseAsync();


            return new JsonResult(new { Msg = "Usuario  removido com sucesso!", Url = Url.Page("/usuarios/usuarios") });
        }
    }
}
