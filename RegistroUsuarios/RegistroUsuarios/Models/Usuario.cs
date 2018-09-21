using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RegistroUsuarios.Models
{
    public class Usuario
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string N_usuario { get; set; }
        public string Clave { get; set; }
        public string Imagen { get; set; }

        private UsuarioDBContext db;

        public Usuario()
        {

        }

        public Usuario(UsuarioDBContext BaseDatos)
        {
            this.db = BaseDatos;
        }

        //Método para guardar
        public Task<bool> GuardarTablaAsincrona(Usuario tabla)
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
        public Task<List<Usuario>> QueryAsincrona(string query)
        {
            return this.db.Conexion.QueryAsync<Usuario>(query);
        }

        //Metodo para eliminar
        public Task<bool> EliminarTablaAsincrona(Usuario tabla)
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
        public Task<bool> ActualizarTablaAsincrona(Usuario tabla)
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
