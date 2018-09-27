using System;
using System.Collections.Generic;
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
	public partial class Registrar : ContentPage
	{
        //Para la conexion a bd
        private PersonaDBContext dbPersona;
        private UsuarioDBContext dbUsuario;
        private string baseDatos = "dbRegistro.db3";
        private string ubicacion = "";
        Persona per = new Persona();
        Persona pers;

        public Registrar ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatos);
            this.dbPersona = new PersonaDBContext(ubicacion);
            this.dbUsuario = new UsuarioDBContext(ubicacion);
        }

        private  void btnGuardar1_Clicked(object sender, EventArgs e)
        {
            pers = new Persona(dbPersona);
            pers.Nombre = txtNombre.Text;
            pers.Apellido = txtApellido.Text;
            pers.Correo = txtCorreo.Text;
            pers.Direccion = txtDireccion.Text;
            pers.Telefono = txtTelefono.Text;

            
            boxUsuario.IsVisible = true;
            boxPersona.IsVisible = false;

        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool resultado1 = await pers.GuardarTablaAsincrona(pers);
                Usuario usr = new Usuario(dbUsuario);
                Persona p = new Persona(dbPersona);
                usr.N_usuario = txtUsuario.Text;
                usr.Clave = txtClave.Text;
                usr.Imagen = imgUsuario.Source.ToString();
                //recupero la ultima persona insertada
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
                
                if (resultado && resultado1)
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