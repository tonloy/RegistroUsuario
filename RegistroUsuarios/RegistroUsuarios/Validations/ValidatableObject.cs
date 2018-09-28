using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegistroUsuarios.ViewModels.Base;

namespace RegistroUsuarios.Validations
{
    public class ValidatableObject<T> :ExtendedBinableObject, IValidaty
    {
        //Definición de variables
        private readonly List<IValidationRule<T>> validaciones;
        private List<string> errores;
        private T valores;
        private bool esValido;

        public ValidatableObject()
        {
            esValido = true;
            errores = new List<string>();
            validaciones = new List<IValidationRule<T>>();

        }
 
        //Definicion de propiedades
        public List<IValidationRule<T>> Validations => validaciones;
        //Lista de Errores
        public List<String> Errores {
            get {
                return this.errores;
            }
            set {
                this.errores = value;
                RaisePropertyChanged(()=> Errores);
            }
        }
        //El valor que almacena el objeto
        public T Value {
            get { return valores; }
            set {
                valores = value;
                RaisePropertyChanged(()=>Value);
            }
        }
        //Permite establecer si a pasado o no la validación
        public bool IsValid
        {
            get { return esValido; }
            set
            {
                esValido = value;
                RaisePropertyChanged(() => IsValid);
            }
        }
        //Función que permite la validación de los valores
        public bool Validate() {
            Errores.Clear();
            IEnumerable<string> error = this.validaciones.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);
            Errores = error.ToList();
            IsValid = !Errores.Any();
           return this.IsValid;
        }
    }
}
