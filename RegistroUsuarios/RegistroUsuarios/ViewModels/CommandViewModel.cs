using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using RegistroUsuarios.Views;

namespace RegistroUsuarios.ViewModels
{
    public class CommandViewModel
    {
        public INavigation navegacion { get; set; }//para obtener el elemento que controla la navegacion 
        public Entry password;
        public Image ojo;
        public ICommand ClicComando { get; set; }//guarda la accion que se ejecutara tras el clic de un boton
        public ICommand TapImagen { get; set; }//para cambiar la visibilidad de la clave de usuario

        public CommandViewModel(INavigation nav, ref Entry clave, ref Image icono)
        {
            this.navegacion = nav;
            this.password = clave;
            this.ojo = icono;
            ClicComando = new Command(FuncionNavegacion);
            TapImagen = new Command(FuncionCambiarClave);
        }

        void FuncionNavegacion()
        {
            this.navegacion.PushAsync(new Registrar());
        }

        void FuncionCambiarClave()
        {
            password.IsPassword = password.IsPassword ? false : true;
            ojo.Source = ImageSource.FromFile(password.IsPassword == true ? "eye.png" : "hide.png");
        }
    }
}
