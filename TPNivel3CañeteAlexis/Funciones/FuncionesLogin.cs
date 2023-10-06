using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Funciones
{
    public class FuncionesLogin
    {
        public bool Login(Users Usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id,Nombre,Apellido,Email,Pass,UrlImagenPerfil,Admin from USERS where email=@email AND pass=@pass");
                datos.SetearParametro("@email", Usuario.Email);
                datos.SetearParametro("@pass", Usuario.Pass);              
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    Usuario.Id = (int)datos.Lector["Id"];
                    Usuario.Admin = (bool)datos.Lector["Admin"];
                    if (!(datos.Lector["UrlImagenPerfil"]is DBNull))
                    Usuario.UrlImagenPerfil = (string)datos.Lector["UrlImagenPerfil"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        Usuario.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Apellido"] is DBNull))
                        Usuario.Apellido = (string)datos.Lector["Apellido"];
                    return true;
                }
                return false;
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
        public int NuevoUsuario(Users nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("NuevoUsuario");
                datos.SetearParametro("@Email",nuevo.Email);
                datos.SetearParametro("@Pass",nuevo.Pass);
                return datos.EjecutarAccionId();
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
        public void Actualizarperfil(Users Usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update USERS set urlImagenPerfil=@urlImagenPerfil,Nombre=@Nombre,Apellido=@Apellido where Id=@id");
                datos.SetearParametro("@urlImagenPerfil", Usuario.UrlImagenPerfil!=null?Usuario.UrlImagenPerfil:(object)DBNull.Value);
                datos.SetearParametro("@id", Usuario.Id);
                datos.SetearParametro("@Nombre", Usuario.Nombre);
                datos.SetearParametro("@Apellido", Usuario.Apellido);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    
}
