﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RegistroUsuarios.Models
{
    public class PersonaDBContext
    {
        //Propiedad de conexion a la base de datos
        public SQLiteAsyncConnection Conexion { get; set; }

        //Constructor que solicita la base de datos
        public PersonaDBContext(String sqlitepath)
        {
            try
            {
                this.Conexion = new SQLiteAsyncConnection(sqlitepath);
                this.Conexion.CreateTableAsync<Persona>().Wait();
            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message);
            }
        }
    }
}
