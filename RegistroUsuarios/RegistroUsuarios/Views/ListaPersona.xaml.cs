using RegistroUsuarios.Models;
using RegistroUsuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RegistroUsuarios.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPersona : ContentPage
	{
        private PersonaDBContext dbPersona;
        private UsuarioDBContext dbUsuario;
        private string baseDatosUser = "dbRegistro.db3";
        private string ubicacion = "";
        ObservableCollection<Persona> listadPersona;
        Usuario usuario=new Usuario();

        public ListaPersona ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatosUser);
            dbPersona = new PersonaDBContext(ubicacion);
            dbUsuario = new UsuarioDBContext(ubicacion);
            CargarRegistros();
        }

        void CargarRegistros()
        {
            Persona nuevaPersona = new Persona(this.dbPersona);
            var Personas = nuevaPersona.QueryAsincrona("Select * FROM [Persona]").Result;

            if (!(Personas.Count == 0))
            {
                listadPersona = new ObservableCollection<Persona>();

                foreach (var item in Personas)
                {
                    listadPersona.Add(item);
                }
                PersonaLista.ItemsSource = listadPersona;

            }

        }

        //Metodo para Refrescar
        private void RefrescarLista(object sender, EventArgs e)
        {
            CargarRegistros();
            PersonaLista.EndRefresh();
        }

        //Metodo para Eliminar 
        private async void EliminarItem(object sender, ItemTappedEventArgs e)
        {
            var answer = await DisplayAlert("Confirmación","¿Desea eliminar la persona?","Si","No");
            if (answer)
            {
                if (e.Item == null)
                {
                    return;
                }

                //Convertiremos el tipo object al tipo Tipo Persona
                var lista = (ListView)sender;
                var myseleccion = (lista.SelectedItem as Persona);

                Usuario usr = new Usuario(this.dbUsuario);
                var usu = usr.QueryAsincrona("Select * from Usuario where IdPersona=" + myseleccion.Id).Result;

                if (usu.Count == 1)
                {
                    foreach (var u in usu)
                    {
                        usuario = u;
                    }
                }
                //Procedemos a eliminar

                Persona _eliminarPersona = new Persona(this.dbPersona);
                bool resultado = await _eliminarPersona.EliminarTablaAsincrona(myseleccion);
                bool resultado2 = await usr.EliminarTablaAsincrona(usuario);
                if (resultado && resultado2)
                {
                    await DisplayAlert("Exito", "Se ha eliminado la persona con su usuario", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar la persona", "Aceptar");
                }

                CargarRegistros();
                ((ListView)sender).SelectedItem = null;
            }         


        }


    }
}