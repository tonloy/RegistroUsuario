using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using System.Linq;
using RegistroUsuarios.Views;
using RegistroUsuarios.Models;

namespace RegistroUsuarios.ViewModels
{
    public class CommandViewModel
    {
        //controles de password y la imagen del ojo
        public Entry password;
        public Image ojo;

        public INavigation navegacion { get; set; }//para obtener el elemento que controla la navegacion        
        public ICommand ClicComando { get; set; }//guarda la accion que se ejecutara tras el clic de un boton
        public ICommand TapImagen { get; set; }//para cambiar la visibilidad de la clave de usuario
        public ICommand Cancelar { get; set; }//para cancelar el registro de usuario
        public ICommand AddPersona { get; set; }//para agregar una persona a la base
        public ICommand AddUsuario { get; set; }//para agregar un usuario a la base

        public CommandViewModel(INavigation nav, ref Entry clave, ref Image icono)
        {
            this.navegacion = nav;
            this.password = clave;
            this.ojo = icono;
            ClicComando = new Command(FuncionNavegacion);
            TapImagen = new Command(FuncionCambiarClave);
        }
        public CommandViewModel(INavigation nav)
        {
            this.navegacion = nav;
            ClicComando = new Command(FuncionNavegacion);
            TapImagen = new Command(FuncionCambiarClave);
            Cancelar = new Command(FuncionCancelar);
        }

        async void FuncionNavegacion()
        {
            await this.navegacion.PushModalAsync(new Registrar());
        }

        async void FuncionCancelar()
        {
            await this.navegacion.PopModalAsync();
        }

        void FuncionCambiarClave()
        {
            password.IsPassword = password.IsPassword ? false : true;
            ojo.Source = ImageSource.FromFile(password.IsPassword == true ? "eye.png" : "hide.png");
        }
    }
}
