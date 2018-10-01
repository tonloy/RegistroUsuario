using RegistroUsuarios.Models;
using RegistroUsuarios.Utilitario;
using RegistroUsuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RegistroUsuarios.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActualizarPersona : ContentPage
	{
        //Para la conexion a bd
        private PersonaDBContext dbPersona;
        private UsuarioDBContext dbUsuario;
        private string baseDatos = "dbRegistro.db3";
        private string ubicacion = "";
        public static int _id;
        Persona per = new Persona();
        Usuario usua = new Usuario();

        public ActualizarPersona ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatos);
            this.dbPersona = new PersonaDBContext(ubicacion);
            this.dbUsuario = new UsuarioDBContext(ubicacion);
            
        }

        void CargarDatosUsuario()
        {
            try
            {
                Usuario usuario = new Usuario(dbUsuario);
                var _usu = usuario.QueryAsincrona("Select * from [Usuario] where IdPersona=" + modId1.Text).Result;
                if (_usu.Count == 1)
                {
                    foreach (var item in _usu)
                    {
                        usua = item;
                    }
                    modId2.Text = usua.Id.ToString();
                    txtUsuario.Text = usua.N_usuario;
                    txtClave.Text = Seguridad.DesEncriptar(usua.Clave);
                    txtClaveRepetir.Text = usua.Clave;
                }
            }catch(Exception e)
            {

            }
            
        }

        private async void UpdatePersona(object sender, EventArgs e)
        {
            Persona _actualizarPersona = new Persona(this.dbPersona);
            _actualizarPersona.Id = Int32.Parse(this.modId1.Text);
            _actualizarPersona.Nombre = this.txtNombre.Text;
            _actualizarPersona.Apellido = this.txtApellido.Text;
            _actualizarPersona.Direccion = this.txtDireccion.Text;
            _actualizarPersona.Correo = this.txtCorreo.Text;
            _actualizarPersona.Telefono = this.txtTelefono.Text;
            _id= Int32.Parse(this.modId1.Text);

            var resultado = await _actualizarPersona.ActualizarTablaAsincrona(_actualizarPersona);
            if (resultado)
            {
                await DisplayAlert("Info", "Datos guardados", "OK");
                CargarDatosUsuario();
                boxUsuario.IsVisible = true;
                boxPersona.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Error", "El registro no fue actualizado", "OK");
            }
        }

        private async void UpdateUsuario(object sender, EventArgs e)
        {
            Usuario _actualizarUsuario = new Usuario(this.dbUsuario);
            _actualizarUsuario.Id = Int32.Parse(this.modId2.Text);
            _actualizarUsuario.N_usuario = this.txtUsuario.Text;
            _actualizarUsuario.Clave = Seguridad.Encriptar(this.txtClave.Text);
            _actualizarUsuario.IdPersona = _id;

            var resultado = await _actualizarUsuario.ActualizarTablaAsincrona(_actualizarUsuario);
            if (resultado)
            {
                await DisplayAlert("Exito", "El usuario fue actualizado", "OK");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "El registro no fue actualizado", "OK");
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}