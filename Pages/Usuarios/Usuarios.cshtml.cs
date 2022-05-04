using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetBasics.Pages
{
    public class UsuariosModel : PageModel
    {
        public List <UsuarioViewModel> Usuarios { get; set; }
        public async Task OnGet()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=Un1v3rse");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM usuario";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            Usuarios = new List<UsuarioViewModel>();

            while (await reader.ReadAsync())
            {
                Usuarios.Add(new UsuarioViewModel()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Usuario = reader.GetString(2)
                });
            }

            await mySqlConnection.CloseAsync();
        }

        
    }

    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }

    }
}
