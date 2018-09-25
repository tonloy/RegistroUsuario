﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroUsuarios.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RegistroUsuarios.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrarUser : ContentPage
	{
		public RegistrarUser ()
		{
			InitializeComponent ();
            BindingContext = new CommandViewModel(Navigation);
        }
	}
}