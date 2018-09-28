using RegistroUsuarios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RegistroUsuarios.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Bienvenido : ContentPage
	{
		public Bienvenido ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
            lblMensaje.Text ="Bienvenido "+ MainPage._persona.Nombre + " " + MainPage._persona.Apellido;
        }

        private void btnCerrarSeccion_Clicked(object sender, EventArgs e)
        {            
            Navigation.PopToRootAsync();
        }
    }
}