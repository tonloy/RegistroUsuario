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
        private string baseDatos = "dbRegistro.db3";
        private string ubicacion = "";

        public Registrar ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatos);
            this.dbPersona = new PersonaDBContext(ubicacion);
        }

        private async void btnGuardar1_Clicked(object sender, EventArgs e)
        {
            Persona pers = new Persona(dbPersona);
            pers.Nombre = txtNombre.Text;
            pers.Apellido = txtApellido.Text;
            pers.Correo = txtCorreo.Text;
            pers.Direccion = txtDireccion.Text;
            pers.Telefono = txtTelefono.Text;

            bool resultado = await pers.GuardarTablaAsincrona(pers);
            if (resultado)
            {
                await DisplayAlert("Exito!","Persona agregada","Aceptar");
            }
            else
            {
                await DisplayAlert("Error!", "Persona no agregada", "Aceptar");
            }
        }
    }
}