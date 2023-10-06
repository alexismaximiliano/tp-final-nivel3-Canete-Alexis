using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Funciones
{
    public static class Seguridad
    {
        public static bool SessionActiva(object Usuario)
        {
            Users Activo = Usuario != null?(Users)Usuario : null;
            if (Activo != null && Activo.Id != 0)
                return true;
            else
                return false;
        }
        public static bool EsAdmin(object Usuario)
        {
            Users Activo = Usuario != null ? (Users)Usuario : null;
            return Activo.Admin;//return Activo!=null?Activo.Admin:false;
        }
    }
}
