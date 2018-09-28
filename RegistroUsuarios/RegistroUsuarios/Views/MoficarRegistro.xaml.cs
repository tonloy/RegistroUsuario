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
	public partial class MoficarRegistro : ContentPage
	{
        private PersonaDBContext dbPersona;
        private string baseDatosUser = "dbRegistro.db3";
        private string ubicacion = "";
        ObservableCollection<Persona> listadPersona;

        public MoficarRegistro ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            ubicacion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.baseDatosUser);
            dbPersona = new PersonaDBContext(ubicacion);
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
                ListaModificar.ItemsSource = listadPersona;

            }

        }

        //Metodo para Refrescar
        private void RefrescarLista(object sender, EventArgs e)
        {
            CargarRegistros();
            ListaModificar.EndRefresh();
        }
        //Metodo para actualizar 
        private async void ActualizarItem(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            //Conversion de Objeto
            var lista = (ListView)sender;
            var myseleccion = (lista.SelectedItem as Persona);

            var _actualizar = new ActualizarPersona();
            ActualizarPersona._id = myseleccion.Id;
            _actualizar.BindingContext = myseleccion;
            await Navigation.PushModalAsync(_actualizar);

        }

    }
}