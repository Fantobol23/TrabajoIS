using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess
{
    public abstract class Mapper<T>
    {
        internal AccessDB acceso;

        public abstract int Insertar(T obj);
        public abstract int Editar(T obj);
        public abstract int Borrar(T obj);
        public abstract List<T> ListarTodo();
    }
}
