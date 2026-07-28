using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> list()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo>lista = new List<Articulo>();
            try
            {
                datos.setearConsulta("select codigo,nombre,A.Descripcion,imagenUrl,Precio,C.Descripcion categoria ,M.Descripcion marca from ARTICULOS A ,CATEGORIAS C ,MARCAS M where IdCategoria = C.Id and IdMarca = M.id");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.codigoArticulo = (string)datos.Lector["codigo"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    aux.urlImagen = (string)datos.Lector["imagenUrl"];
                    aux.precio = (decimal)datos.Lector["precio"];
                    aux.categorias = new Categoria();
                    aux.categorias.descripcion = (string)datos.Lector["categoria"];
                    aux.marcas = new Marca();
                    aux.marcas.descripcion = (string)datos.Lector["marca"];

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
        public void agregarProducto(Articulo articulos)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio)values(@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)");
                datos.setearParametros("@Codigo",articulos.codigoArticulo);
                datos.setearParametros("@Nombre",articulos.nombre);
                datos.setearParametros("@Descripcion",articulos.descripcion);
                datos.setearParametros("@IdMarca",articulos.marcas.id);
                datos.setearParametros("@IdCategoria",articulos.categorias.id);
                datos.setearParametros("@ImagenUrl",articulos.urlImagen);
                datos.setearParametros("@Precio",articulos.precio);
                datos.ejecutarAccion();
                datos.cerraConexion();
            }
            catch (Exception ex)
            {

                throw ex ;
            }
        }
        

       
    }
}
