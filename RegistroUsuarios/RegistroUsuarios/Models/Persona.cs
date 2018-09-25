using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios.Models
{
    public class Persona
    {
        //Campos de la tabla Persona
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }

        private PersonaDBContext db;

        public Persona()
        {

        }

        public Persona(PersonaDBContext BaseDatos)
        {
            this.db = BaseDatos;
        }

        //Método para guardar
        public Task<bool> GuardarTablaAsincrona(Persona tabla)
        {
            if (this.db.Conexion.InsertAsync(tabla).Result == 1)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        //Método para la ejecución de una Query
        public Task<List<Persona>> QueryAsincrona(string query)
        {
            
            return this.db.Conexion.QueryAsync<Persona>(query);
        }

        //Metodo para eliminar
        public Task<bool> EliminarTablaAsincrona(Persona tabla)
        {
            if (this.db.Conexion.DeleteAsync(tabla).Result == 1)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        //metodo para actualizar datos
        public Task<bool> ActualizarTablaAsincrona(Persona tabla)
        {
            if (this.db.Conexion.UpdateAsync(tabla).Result == 1)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
