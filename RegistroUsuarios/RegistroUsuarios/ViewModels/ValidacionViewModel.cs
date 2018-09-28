using RegistroUsuarios.Validations;
using RegistroUsuarios.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RegistroUsuarios.ViewModels
{
    public class ValidacionViewModel:BaseViewModel
    {
        //Declaracion de variables
        private static ValidatableObject<string> nombre;
        private static ValidatableObject<string> apellido;
        private static ValidatableObject<string> direccion;
        private static ValidatableObject<string> telefono;
        private static ValidatableObject<string> correo;
        private static ValidatableObject<string> usuario;
        private static ValidatableObject<string> clave;
        private static ValidatableObject<string> clave2;

        //Declaracion de propiedades
        public ValidatableObject<string> Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
                RaisePropertyChanged(() => Nombre);
            }
        }

        public ValidatableObject<string> Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
                RaisePropertyChanged(() => Apellido);
            }
        }

        public ValidatableObject<string> Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
                RaisePropertyChanged(() => Direccion);
            }
        }

        public ValidatableObject<string> Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
                RaisePropertyChanged(() => Telefono);
            }
        }

        public ValidatableObject<string> Correo
        {
            get
            {
                return correo;
            }
            set
            {
                correo = value;
                RaisePropertyChanged(() => Correo);
            }
        }

        public ValidatableObject<string> Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
                RaisePropertyChanged(() => Usuario);
            }
        }

        public ValidatableObject<string> Clave
        {
            get
            {
                return clave;
            }
            set
            {
                clave = value;
                RaisePropertyChanged(() => Clave);
            }
        }

        public ValidatableObject<string> Clave2
        {
            get
            {
                return clave2;
            }
            set
            {
                clave2 = value;
                RaisePropertyChanged(() => Clave2);
            }
        }

        //Declaracion de Commandos
        public ICommand CommandValidacionPersona { get; set; }
        public ICommand CommandValidacionUsuario { get; set; }
        public ICommand ValidateUsuarioCommand => new Command(() => ValidarUsuario());
        public ICommand ValidateCorreoCommand => new Command(() => ValidarCorreo());
        public ICommand ValidateDireccionCommand => new Command(() => ValidarDireccion());
        public ICommand ValidateNombreCommand => new Command(() => ValidarNombre());
        public ICommand ValidateApellidoCommand => new Command(() => ValidarApellido());
        public ICommand ValidateTelefonoCommand => new Command(() => ValidarTelefono());
        public ICommand ValidateClaveCommand => new Command(() => ValidarClave());
        public ICommand ValidateClave2Command => new Command(() => ValidarClave2());
        private static Page contexto;

        public ValidacionViewModel(Page pagina)
        {
            
            //Validaciones
            //Inicializamos las propiedades
            nombre = new ValidatableObject<string>();
            apellido = new ValidatableObject<string>();
            direccion = new ValidatableObject<string>();
            telefono = new ValidatableObject<string>();
            correo = new ValidatableObject<string>();
            usuario = new ValidatableObject<string>();
            clave = new ValidatableObject<string>();
            clave2 = new ValidatableObject<string>();

            //Agregamos las validaciones
            this.AgregarValidaciones();
            //this.CommandValidacionPersona = new Command(Validar);
            //this.CommandValidacionUsuario = new Command(this.ValidarU);
            contexto = pagina;
        }

        private void AgregarValidaciones()
        {
            //Agregamos la validacion del Usuario
            usuario.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El Usuario es requerido"
            });
            //Agregamos la validacion de longitud del usuario
            usuario.Validations.Add(new LengthRule<string>
            {
                ValidationMessage = "El nombre de usuario es muy largo"
            });
            //Agregamos la validacion del Correo
            correo.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "El correo no es válido"
            });
            correo.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El correo es requerido"
            });
            //Agregamos la validacion de nombre
            nombre.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El nombre es requerido"
            });

            //Agregamos la validacion de apellido
            apellido.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El apellido es requerido"
            });

            //Agregamos la validacion de direccion
            direccion.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La dirección es requerida"
            });

            //Agregamos la validacion de telefono
            telefono.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El teléfono es requerido"
            });
            telefono.Validations.Add(new PhoneRule<string>
            {
                ValidationMessage = "El teléfono no es válido"
            });

            //Agregamos la validacion de clave
            clave.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La clave es requerida"
            });

            //Agregamos la validacion de clave2
            clave2.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Por favor repita la clave"
            });
        }

        public static bool Validar()
        {
            //Verificamos que pasan las validaciones
            if (ValidarCorreo() && ValidarDireccion() && ValidarTelefono() && ValidarNombre() && ValidarApellido())
            {
                //Mostramos el mensaje de validacion correcta
                //await contexto.DisplayAlert("Validaciones", "Validación Correcta!", "Aceptar");
                return true;
            }
            else
            {
                //await contexto.DisplayAlert("Validaciones", "Error en la validación!", "Aceptar");
                return false;
            }
        }

        public  async void ValidarU()
        {
            //Verificamos que pasan las validaciones
            if (ValidarUsuario() && ValidarClave() && ValidarClave2())
            {
                //Mostramos el mensaje de validacion correcta
                await contexto.DisplayAlert("Validaciones", "Validación Correcta!", "Aceptar");
            }
            else
            {
                await contexto.DisplayAlert("Validaciones", "Error en la validación!", "Aceptar");
            }
        }

        //Permite la validación del campo del Usuario
        private static bool ValidarUsuario()
        {
            return usuario.Validate();
        }

        //Permite la validación del Correo
        private static bool ValidarCorreo()
        {
            return correo.Validate();
        }

        //Permite verificar la direccion
        private static bool ValidarDireccion()
        {
            return direccion.Validate();
        }

        //Permite verificar el telefono
        private static bool ValidarTelefono()
        {
            return telefono.Validate();
        }

        //Permite verificar la clave
        private static bool ValidarClave()
        {
            return clave.Validate();
        }

        //Permite verificar la clave2
        private static bool ValidarClave2()
        {
            return clave2.Validate();
        }

        //Permite verificar el nombre
        private static bool ValidarNombre()
        {
            return nombre.Validate();
        }

        //Permite verificar el apellido
        private static bool ValidarApellido()
        {
            return apellido.Validate();
        }
    }
}
