using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funciones
{
    public class FuncionesFavoritos
    {       
        public List<Articulos> ListaFavoritos(int IdUser)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulos> lista = new List<Articulos>();
            try
            {
                datos.SetearConsulta("select a.ImagenUrl,a.Nombre,a.Precio,a.Id from ARTICULOS A join FAVORITOS F  on A.Id=f.IdArticulo join USERS U on U.Id=F.IdUser where u.Id=@IdUser");
                datos.SetearParametro("@IdUser", IdUser);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    //aux.Categoria = new Categorias();
                    //aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    //aux.Marca = new Marcas();
                    //aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AñadirFavoritos(FavoritosArticulos nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into FAVORITOS (IdArticulo,IdUser) values(@IdArticulo,@IdUser)");
                datos.SetearParametro("@IdUser", nuevo.IdUser);
                datos.SetearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();

            }
        }
        public void EliminarFavoritos(FavoritosArticulos articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from FAVORITOS where IdArticulo=@IdArticulo and IdUser=@IdUser");
                datos.SetearParametro("IdArticulo", articulo.IdArticulo);
                datos.SetearParametro("IdUser", articulo.IdUser);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                //cambiar por stored procedure
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public bool ContarFavoritos(FavoritosArticulos articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.SetearParametro("@IdUser", articulo.IdUser);
                datos.SetearParametro("IdArticulo", articulo.IdArticulo);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    int N = (int)datos.Lector[0];
                    if (N > 0)
                    {
                        datos.CerrarConexion();
                        return false;
                    }
                }
                datos.CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



