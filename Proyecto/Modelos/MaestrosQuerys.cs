using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Modelos
{
    class MaestrosQuerys
    {
        //Traer todos los registros
        internal static string selectMaes()
        {
            string selectMaes = "SELECT MA.*, ES.NOMBRE ESTABLECIMIENTO FROM MAESTROS MA INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = MA.ID_ESTABLECIMIENTO";
            return selectMaes;
        }

        //Traer maestro especifico
        internal static string selectMaesEspecifico()
        {
            string selectMaesEspecifico = "SELECT MA.*, ES.NOMBRE ESTABLECIMIENTO FROM MAESTROS MA INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = MA.ID_ESTABLECIMIENTO WHERE MA.ID_MAESTRO = :P_ID_MAESTRO";
            return selectMaesEspecifico;
        }

        //Insertar datos
        internal static string insertMaes()
        {
            string insertMaes = "INSERT INTO MAESTROS (ID_MAESTRO, DPI, NOMBRE, APELLIDO, DIRECCION, EDAD, TELEFONO, ID_ESTABLECIMIENTO) VALUES (INCREMENTOMAESTROS.NEXTVAL, :P_DPI, :P_NOMBRE, :P_APELLIDO, :P_DIRECCION, :P_EDAD, :P_TELEFONO, :P_ID_ESTABLECIMIENTO)";
            return insertMaes;
        }

        //Actualizar datos
        internal static string updateMaes()
        {
            string updateMaes = "UPDATE MAESTROS SET DPI = :P_DPI, NOMBRE = :P_NOMBRE, APELLIDO = :P_APELLIDO, DIRECCION = :P_DIRECCION, EDAD = :P_EDAD, TELEFONO = :P_TELEFONO, ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO WHERE ID_MAESTRO = :P_ID_MAESTRO";
            return updateMaes;
        }

        //Eliminar datos
        internal static string deleteMaes()
        {
            string deleteMaes = "DELETE FROM MAESTROS WHERE ID_MAESTRO = :P_ID_MAESTRO";
            return deleteMaes;
        }
    }
}