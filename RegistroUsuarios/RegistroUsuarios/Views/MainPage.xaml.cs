using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RegistroUsuarios.ViewModels;
using RegistroUsuarios.Models;
using System.IO;

namespace RegistroUsuarios
{
    public partial class MainPage : ContentPage
    {

        //para la conexion a la bd
        private UsuarioDBContext dbUsuario;
        private string baseDatosUser = "dbRegistro.db3";
        private string ubicacion = "";
        Usuario user = new Usuario();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CommandViewModel(Navigation,ref txtPassword,ref iconoOjo);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatosUser);
            dbUsuario = new UsuarioDBContext(ubicacion);
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            Usuario usrExiste = new Usuario(dbUsuario);
            var usuario = usrExiste.QueryAsincrona("Select * from Usuario where N_usuario='"+txtUsuario.Text+"' and Clave='"+txtPassword.Text+"'").Result;
            if (usuario.Count==1)
            {
                foreach (var item in usuario)
                {
                    user = usuario.ElementAt(0);
                }
                await DisplayAlert("Bienvenido",user.N_usuario,"Aceptar");
            }
            else
            {
                await DisplayAlert("Error", "No existes, sorry "+ txtUsuario.Text, "Aceptar");
            }
        }
    }
}
