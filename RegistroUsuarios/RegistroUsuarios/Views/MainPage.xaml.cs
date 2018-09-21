using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegistroUsuarios
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowPass(object sender, EventArgs args)
        {
            txtPassword.IsPassword = txtPassword.IsPassword ? false : true;
            iconoOjo.Source = ImageSource.FromFile(txtPassword.IsPassword==true?"eye.png":"hide.png");
        }
    }
}
