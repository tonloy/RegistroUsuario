using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RegistroUsuarios.ViewModels;

namespace RegistroUsuarios
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CommandViewModel(Navigation,ref txtPassword,ref iconoOjo);
        }
    }
}
