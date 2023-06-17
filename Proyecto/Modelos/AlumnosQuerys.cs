using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Modelos
{
    class AlumnosQuerys
    {
        //Traer todos los registros
        internal static string selectAlm()
        {
            string selectAlm = "SELECT AL.*, CA.NOMBRE CARRERA, ES.NOMBRE ESTABLECIMIENTO FROM ALUMNOS AL INNER JOIN CARRERAS CA ON CA.ID_CARRERA = AL.ID_CARRERA " +
                "INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = AL.ID_ESTABLECIMIENTO";
            return selectAlm;
        }

        //Traer todos los registros por ID
        internal static string selectAlmEspecifico()
        {
            string selectAlmEspecifico = "SELECT AL.*, CA.NOMBRE CARRERA, ES.NOMBRE ESTABLECIMIENTO FROM ALUMNOS AL INNER JOIN CARRERAS CA ON CA.ID_CARRERA = AL.ID_CARRERA " +
                "INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = AL.ID_ESTABLECIMIENTO WHERE AL.CODIGO_ALUMNO = :P_CODIGO_ALUMNO";
            return selectAlmEspecifico;
        }

        //Insertar los registros
        internal static string insertAlm()
        {
            string insertAlm = "INSERT INTO ALUMNOS (CODIGO_ALUMNO, NOMBRE, APELLIDO, DIRECCION, EDAD, TELEFONO, ID_ESTABLECIMIENTO, ID_CARRERA)" + 
                " VALUES (INCREMENTOALUMNOS.NEXTVAL, :P_NOMBRE, :P_APELLIDO, :P_DIRECCION, :P_EDAD, :P_TELEFONO, :P_ID_ESTABLECIMIENTO, :P_ID_CARRERA)";
            return insertAlm;
        }

        //Insertar los registros
        internal static string updateAlm()
        {
            string updateAlm = "UPDATE ALUMNOS SET NOMBRE = :P_NOMBRE, APELLIDO = :P_APELLIDO, DIRECCION = :P_DIRECCION, EDAD = :P_EDAD, TELEFONO = :P_TELEFONO, ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO, ID_CARRERA = :P_ID_CARRERA" +
                " WHERE CODIGO_ALUMNO = :P_CODIGO_ALUMNO";
            return updateAlm;
        }

        //Eliminar los registros
        internal static string deleteAlm()
        {
            string deleteAlm = "DELETE FROM ALUMNOS WHERE CODIGO_ALUMNO = :P_CODIGO_ALUMNO";
            return deleteAlm;
        }
    }
}