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
        private PersonaDBContext dbPersona;
        private string baseDatosUser = "dbRegistro.db3";
        private string ubicacion = "";
        Usuario user = new Usuario();
        Persona _persona = new Persona();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CommandViewModel(Navigation,ref txtPassword,ref iconoOjo,this);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatosUser);
            dbUsuario = new UsuarioDBContext(ubicacion);
            dbPersona = new PersonaDBContext(ubicacion);
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (CommandViewModel.ValidarInicio())
                {
                    Usuario usrExiste = new Usuario(dbUsuario);
                    Persona persona = new Persona(dbPersona);
                    var usuario = usrExiste.QueryAsincrona("Select * from Usuario where N_usuario='" + txtUsuario.Text + "' and Clave='" + txtPassword.Text + "'").Result;
                    if (usuario.Count == 1)
                    {
                        foreach (var item in usuario)
                        {
                            user = usuario.ElementAt(0);
                        }
                        var persona_ = persona.QueryAsincrona("Select * from Persona where Id=" + user.IdPersona).Result;
                        foreach (var item in persona_)
                        {
                            _persona = item;
                        }
                        await DisplayAlert("Bienvenido", _persona.Nombre, "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No existes, sorry " + txtUsuario.Text, "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Error!", "Campos vacíos o inválidos", "Aceptar");
                }
                
            }catch(Exception er)
            {
                await DisplayAlert("Error",er.Message, "Aceptar");
            }
        }
    }
}
