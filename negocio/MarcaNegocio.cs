using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> list()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> lista = new List<Marca>();

            try
            {
                datos.setearConsulta("select id , descripcion from MARCAS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.id = (int)datos.Lector["id"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(aux);
                }
                datos.cerraConexion();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

          
        }
    }
}
