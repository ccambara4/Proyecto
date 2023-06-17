using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Proyecto.Modelos;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Proyecto.Servicios
{
    /// <summary>
    /// Descripción breve de WSCarreras
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSCarreras : System.Web.Services.WebService
    {

        //Traer a todos los registros
        [WebMethod]
        public List<Carreras> GetAllCarreras()
        {
            DataTable dt = new DataTable();
            List<Carreras> lista = new List<Carreras>();
            String sqlCar= CarrerasQuerys.selectCar().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlCar, con))
                    {

                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Carreras
                                {
                                    ID_CARRERA = dt.Rows[i]["ID_CARRERA"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    HORARIO = dt.Rows[i]["HORARIO"].ToString(),
                                    DURACION = dt.Rows[i]["DURACION"].ToString(),
                                    ID_MAESTRO = dt.Rows[i]["ID_MAESTRO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    MAESTRO = dt.Rows[i]["MAESTRO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Carreras
                            {
                                NOMBRE = "No hay datos".ToString()
                            });
                            return lista;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista.Add(new Carreras
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Traer los datos por su ID
        [WebMethod]
        public List<Carreras> GetCar(int ID_CARRERA)
        {
            DataTable dt = new DataTable();
            List<Carreras> lista = new List<Carreras>();
            String sqlCar = CarrerasQuerys.selectCarEspecifico().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlCar, con))
                    {
                        cmd.Parameters.Add("P_ID_CARRERA", ID_CARRERA);
                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Carreras
                                {
                                    ID_CARRERA = dt.Rows[i]["ID_CARRERA"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    HORARIO = dt.Rows[i]["HORARIO"].ToString(),
                                    DURACION = dt.Rows[i]["DURACION"].ToString(),
                                    ID_MAESTRO = dt.Rows[i]["ID_MAESTRO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    MAESTRO = dt.Rows[i]["MAESTRO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Carreras
                            {
                                NOMBRE = "No hay datos".ToString()
                            });
                            return lista;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lista.Add(new Carreras
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Insertar Carreras
        [WebMethod]
        public string InsertCar(string NOMBRE, string HORARIO, string DURACION, int ID_MAESTRO, int ID_ESTABLECIMIENTO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlInsCar = CarrerasQuerys.insertCar().ToString();
            try
            {

                con.Open();
                OracleCommand cmd = new OracleCommand(sqlInsCar, con);
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim());
                cmd.Parameters.Add("P_HORARIO", HORARIO.Trim());
                cmd.Parameters.Add("P_DURACION", DURACION.Trim());
                cmd.Parameters.Add("P_ID_MAESTRO", ID_MAESTRO);
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                cmd.ExecuteNonQuery();
                con.Close();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                con.Close();
                return "Error" + ex.ToString();
            }
        }

        //Actualizar datos
        [WebMethod]
        public string updateCar(int ID_CARRERA, string NOMBRE, string HORARIO, string DURACION, int ID_MAESTRO, int ID_ESTABLECIMIENTO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            string sqlUpdCar = CarrerasQuerys.updateCar().ToString();

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sqlUpdCar, con);


                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim().ToUpper());
                cmd.Parameters.Add("P_HORARIO", HORARIO.Trim().ToUpper());
                cmd.Parameters.Add("P_DURACION", DURACION.Trim());
                cmd.Parameters.Add("P_ID_MAESTRO", ID_MAESTRO);
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                cmd.Parameters.Add("P_ID_CARRERA", ID_CARRERA);



                cmd.ExecuteNonQuery();
                con.Close();
                return "SUCCESS";

            }
            catch (Exception ex)
            {
                con.Close();
                return "Error " + ex.ToString();
            }
        }

        //Eliminar carreras
        [WebMethod]
        public string DeleteCar(string ID_CARRERA)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlDelCar = CarrerasQuerys.deleteCar().ToString();

            try
            {
                con.Open();

                OracleCommand cmd = new OracleCommand(sqlDelCar, con);

                cmd.Parameters.Add("P_ID_CARRERA", ID_CARRERA.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                con.Close();
                return "Error" + ex.ToString();
            }

        }

    }
}
