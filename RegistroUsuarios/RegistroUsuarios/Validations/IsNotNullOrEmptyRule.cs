using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroUsuarios.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set ; }

        public bool Check(T value)
        {
            //Validamos que el objeto no sea nulo
            if (value == null)
            {
                return false;
            }

            var texto = value as string;
            return !string.IsNullOrWhiteSpace(texto);
        }
    }
}
