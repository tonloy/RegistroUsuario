using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegistroUsuarios.Validations
{
    public class NoNumRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var texto = value as string;
            Regex expReg = new Regex(@"([a-zA-Z]+)");
            Match comp = expReg.Match(texto);

            return comp.Success;
        }
    }
}
