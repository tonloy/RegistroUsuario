using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroUsuarios.Models;
using RegistroUsuarios.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RegistroUsuarios.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrarUser : ContentPage
	{
        //para la conexion a la bd
        private UsuarioDBContext dbUsuario;
        private PersonaDBContext dbPersona;
        private string baseDatosUser = "dbUsuario.db3";
        private string ubicacion = "";
        private string ubicacion1 = "";
        Persona per = new Persona();

        public RegistrarUser ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatosUser);
            ubicacion1= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbPersona.db3");
            this.dbUsuario = new UsuarioDBContext(ubicacion);
            this.dbPersona = new PersonaDBContext(ubicacion1);
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Usuario usr = new Usuario(dbUsuario);
                Persona p = new Persona(dbPersona);
                usr.N_usuario = txtUsuario.Text;
                usr.Clave = txtClave.Text;
                var persona = p.QueryAsincrona("Select * from [Persona] order by Id desc limit 1").Result;
                if (persona.Count == 1)
                {
                    foreach (var item in persona)
                    {
                        per = persona.ElementAt(0);
                    }
                }
                usr.Rol = 1;
                usr.IdPersona = per.Id;
                bool resultado = await usr.GuardarTablaAsincrona(usr);
                if (resultado)
                {
                    await DisplayAlert("Exito!", "Usuario agregado", "Aceptar");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error!", "Usuario no agregado", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", ex.Message, "Aceptar");
            }
        }
    }
}