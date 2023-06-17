using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Modelos
{
    class EstablecimientosQuerys
    {
        //Mostrar todos los registros
        internal static string selectEsta()
        {
            string selectEsta = "SELECT * FROM ESTABLECIMIENTO";
            return selectEsta;
        }

        //Mostrar todos los registros
        internal static string selectEstaEspecifico()
        {
            string selectEstaEspecifico = "SELECT * FROM ESTABLECIMIENTO WHERE ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO";
            return selectEstaEspecifico;
        }

        //Insertar registros
        internal static string insertEsta()
        {
            string insertEsta = "INSERT INTO ESTABLECIMIENTO (ID_ESTABLECIMIENTO, NOMBRE, DIRECCION, TELEFONO, FAX) VALUES (INCREMENTOESTABLECIMIENTO.NEXTVAL, :P_NOMBRE, :P_DIRECCION, :P_TELEFONO, :P_FAX)";
            return insertEsta;
        }

        //Actualizar registros
        internal static string updateEsta()
        {
            string updateEsta = "UPDATE ESTABLECIMIENTO SET NOMBRE = :P_NOMBRE, DIRECCION = :P_DIRECCION, TELEFONO = :P_TELEFONO, FAX = :P_FAX WHERE ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO";
            return updateEsta;
        }

        //Eliminar registros
        internal static string deleteEsta()
        {
            string deleteEsta = "DELETE FROM ESTABLECIMIENTO WHERE ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO";
            return deleteEsta;
        }
    }
}