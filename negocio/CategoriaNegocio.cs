using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
       public List<Categoria> list()
        {
			AccesoDatos datos = new AccesoDatos();
			List<Categoria> lista = new List<Categoria>();
			try
			{
				datos.setearConsulta("select id , descripcion from CATEGORIAS");
				datos.ejecutarLectura();
				while (datos.Lector.Read())
				{
					Categoria aux = new Categoria();
					aux.id = (int)datos.Lector["id"];
					aux.descripcion = (string)datos.Lector["descripcion"];

					lista.Add(aux);
				}
				
                return lista;
            }
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerraConexion();
			}
			
        }
    }
}
