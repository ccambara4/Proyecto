using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Modelos
{
    class CarrerasQuerys
    {
        //Traer todos los registros
        internal static string selectCar()
        {
            string selectCar = "SELECT CA.*, MA.NOMBRE MAESTRO, ES.NOMBRE ESTABLECIMIENTO FROM CARRERAS CA INNER JOIN MAESTROS MA ON MA.ID_MAESTRO = CA.ID_MAESTRO INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = CA.ID_ESTABLECIMIENTO";
            return selectCar;
        }

        //Traer todos los registros especificos
        internal static string selectCarEspecifico()
        {
            string selectCarEspecifico = "SELECT CA.*, MA.NOMBRE MAESTRO, ES.NOMBRE ESTABLECIMIENTO FROM CARRERAS CA INNER JOIN MAESTROS MA ON MA.ID_MAESTRO = CA.ID_MAESTRO INNER JOIN ESTABLECIMIENTO ES ON ES.ID_ESTABLECIMIENTO = CA.ID_ESTABLECIMIENTO WHERE CA.ID_CARRERA = :P_ID_CARRERA";
            return selectCarEspecifico;
        }

        //Insertar
        internal static string insertCar()
        {
            string insertCar = "INSERT INTO CARRERAS (ID_CARRERA, NOMBRE, HORARIO, DURACION, ID_MAESTRO, ID_ESTABLECIMIENTO) VALUES (INCREMENTOCARRERAS.NEXTVAL, :P_NOMBRE, :P_HORARIO, :P_DURACION, :P_ID_MAESTRO, :P_ID_ESTABLECIMIENTO)";
            return insertCar;
        }

        //Actualizar
        internal static string updateCar()
        {
            string updateCar = "UPDATE CARRERAS SET NOMBRE = :P_NOMBRE, HORARIO = :P_HORARIO, DURACION = :P_DURACION, ID_MAESTRO = :P_ID_MAESTRO, ID_ESTABLECIMIENTO = :P_ID_ESTABLECIMIENTO WHERE ID_CARRERA = :P_ID_CARRERA";
            return updateCar;
        }

        //Eliminar
        internal static string deleteCar()
        {
            string deleteCar = "DELETE FROM CARRERAS WHERE ID_CARRERA = :P_ID_CARRERA";
            return deleteCar;
        }
    }
}