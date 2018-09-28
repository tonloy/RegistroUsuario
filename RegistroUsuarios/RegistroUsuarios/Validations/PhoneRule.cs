using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroUsuarios.Validations
{
    public class PhoneRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var texto = value as string;
            if (texto.Length != 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
