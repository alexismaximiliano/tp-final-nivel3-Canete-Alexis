using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using Funciones;
using System.Linq.Expressions;
using System.Configuration;

namespace Funciones
{
    public class FuncionesArticulos
    {
        public List<Articulos> FiltrarporId(string id = "")
        {
            List<Articulos> lista = new List<Articulos>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "workstation id=CATALOGOWEBbd.mssql.somee.com;packet size=4096;user id=alexis97_SQLLogin_4;pwd=1m1pyrh5mt;data source=CATALOGOWEBbd.mssql.somee.com;persist security info=False;initial catalog=CATALOGOWEBbd";
                //conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select codigo,nombre,a.descripcion,imagenUrl,precio,c.Descripcion Categoria,m.Descripcion Marca,A.Id from ARTICULOS A,CATEGORIAS C, MARCAS M where a.IdCategoria=c.Id and a.IdMarca=m.Id and ";
                if (id != "")
                    comando.CommandText += "a.Id=" + id;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Id = (int)lector["Id"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Categoria = new Categorias();
                    aux.Categoria.Descripcion = (string)lector["Categoria"];
                    aux.Marca = new Marcas();
                    aux.Marca.Descripcion = (string)lector["Marca"];
                    lista.Add(aux);
                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public List<Articulos> listarconSP()
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("ListaconSP");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Categoria = new Categorias();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marcas();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public void Agregar(Articulos nuevo)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.SetearConsulta("insert into ARTICULOS (Codigo,nombre,Descripcion,Precio,idMarca,idCategoria,ImagenUrl) values('" + nuevo.Codigo + "','" + nuevo.Nombre + "','" + nuevo.Descripcion + "'," + nuevo.Precio + ",@idMarca,@idCategoria,@ImagenUrl)");
        //        datos.SetearParametro("@idMarca", nuevo.Marca.Id);
        //        datos.SetearParametro("@idCategoria", nuevo.Categoria.Id);
        //        datos.SetearParametro("@ImagenUrl", nuevo.ImagenUrl);
        //        datos.EjecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.CerrarConexion();
        //    }
        //}
        public void AgregarconSP(Articulos nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("NuevoArticuloconSP");
                datos.SetearParametro("@Codigo", nuevo.Codigo);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@IdMarca", nuevo.Marca.Id);
                datos.SetearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.SetearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.SetearParametro("@Precio", nuevo.Precio);
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
        //public void Modificar(Articulos modificado)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.SetearConsulta("update ARTICULOS set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@ImagenUrl,Precio=@precio where id=@Id");
        //        datos.SetearParametro("@Codigo", modificado.Codigo);
        //        datos.SetearParametro("@Nombre", modificado.Nombre);
        //        datos.SetearParametro("@Descripcion", modificado.Descripcion);
        //        datos.SetearParametro("@IdMarca", modificado.Marca.Id);
        //        datos.SetearParametro("@IdCategoria", modificado.Categoria.Id);
        //        datos.SetearParametro("@ImagenUrl", modificado.ImagenUrl);
        //        datos.SetearParametro("@Precio", modificado.Precio);
        //        datos.SetearParametro("@Id", modificado.Id);
        //        datos.EjecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }
        //    finally
        //    {
        //        datos.CerrarConexion();
        //    }
        //}
        public void eliminar(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from articulos where id=@Id");
                datos.SetearParametro("@Id", Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Articulos> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select nombre,imagenUrl,Precio,c.Descripcion Categoria,m.Descripcion Marca,A.Id from ARTICULOS A,CATEGORIAS C, MARCAS M where a.IdCategoria=c.Id and a.IdMarca=m.Id and ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a:":
                            consulta += "Precio>" + filtro;
                            break;
                        case "Menor a:":
                            consulta += "Precio<" + filtro;
                            break;
                        case "Igual a:":
                            consulta += "Precio=" + filtro;
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con:":
                            consulta += "M.Descripcion like'" + filtro + "%'";
                            break;
                        case "Termina con:":
                            consulta += "M.Descripcion like'%" + filtro + "'";
                            break;
                        case "Contiene:":
                            consulta += "M.Descripcion like'%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con:":
                            consulta += "C.Descripcion like'" + filtro + "%'";
                            break;
                        case "Termina con:":
                            consulta += "C.Descripcion like'%" + filtro + "'";
                            break;
                        case "Contiene:":
                            consulta += "C.Descripcion like'%" + filtro + "%'";
                            break;
                    }
                }
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Categoria = new Categorias();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marcas();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ModificarconSP(Articulos articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("ModificarArticuloconSP");
                datos.SetearParametro("@Codigo", articulo.Codigo);
                datos.SetearParametro("@Nombre", articulo.Nombre);
                datos.SetearParametro("@Descripcion", articulo.Descripcion);
                datos.SetearParametro("@IdMarca", articulo.Marca.Id);
                datos.SetearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.SetearParametro("@ImagenUrl", articulo.ImagenUrl);
                datos.SetearParametro("@Precio", articulo.Precio);
                datos.SetearParametro("@Id", articulo.Id);
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
    }
}
