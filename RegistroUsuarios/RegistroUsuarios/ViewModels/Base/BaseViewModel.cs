using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RegistroUsuarios.ViewModels.Base
{
    public class BaseViewModel : ExtendedBinableObject
    {
        private bool ocupado;
        public bool IsBusy {
            get {
                return ocupado;
            }
            set {
                ocupado = value;
                RaisePropertyChanged(() => ocupado);
            } }
    }
}
