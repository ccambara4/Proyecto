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
    /// Descripción breve de WSAlumnos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSAlumnos : System.Web.Services.WebService
    {

        //Traer a todos los registros
        [WebMethod]
        public List<Alumnos> GetAllAlumnos()
        {
            DataTable dt = new DataTable();
            List<Alumnos> lista = new List<Alumnos>();
            String sqlAlm = AlumnosQuerys.selectAlm().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlAlm, con))
                    {

                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Alumnos
                                {
                                    CODIGO_ALUMNO = dt.Rows[i]["CODIGO_ALUMNO"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    APELLIDO = dt.Rows[i]["APELLIDO"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    EDAD = dt.Rows[i]["EDAD"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString(),
                                    ID_CARRERA = dt.Rows[i]["ID_CARRERA"].ToString(),
                                    CARRERA = dt.Rows[i]["CARRERA"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Alumnos
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
                lista.Add(new Alumnos
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Traer los datos por su ID
        [WebMethod]
        public List<Alumnos> GetAlm(int CODIGO_ALUMNO)
        {
            DataTable dt = new DataTable();
            List<Alumnos> lista = new List<Alumnos>();
            String sqlAlm = AlumnosQuerys.selectAlmEspecifico().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlAlm, con))
                    {
                        cmd.Parameters.Add("P_CODIGO_ALUMNO", CODIGO_ALUMNO);
                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Alumnos
                                {
                                    CODIGO_ALUMNO = dt.Rows[i]["CODIGO_ALUMNO"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    APELLIDO = dt.Rows[i]["APELLIDO"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    EDAD = dt.Rows[i]["EDAD"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString(),
                                    ID_CARRERA = dt.Rows[i]["ID_CARRERA"].ToString(),
                                    CARRERA = dt.Rows[i]["CARRERA"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Alumnos
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
                lista.Add(new Alumnos
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Insertar Alumnos
        [WebMethod]
        public string InsertAlm(string NOMBRE, string APELLIDO, string DIRECCION, string EDAD, string TELEFONO, int ID_ESTABLECIMIENTO, int ID_CARRERA)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlInsAlm = AlumnosQuerys.insertAlm().ToString();
            try
            {

                con.Open();
                OracleCommand cmd = new OracleCommand(sqlInsAlm, con);
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim());
                cmd.Parameters.Add("P_APELLIDO", APELLIDO.Trim());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim());
                cmd.Parameters.Add("P_EDAD", EDAD.Trim());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO);
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                cmd.Parameters.Add("P_ID_CARRERA", ID_CARRERA);
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
        public string updateAlm(int CODIGO_ALUMNO, string NOMBRE, string APELLIDO, string DIRECCION, string EDAD, string TELEFONO, int ID_ESTABLECIMIENTO, int ID_CARRERA)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            string sqlUpdAlm = AlumnosQuerys.updateAlm().ToString();

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sqlUpdAlm, con);


                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim().ToUpper());
                cmd.Parameters.Add("P_APELLIDO", APELLIDO.Trim().ToUpper());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim().ToUpper());
                cmd.Parameters.Add("P_EDAD", EDAD.Trim().ToUpper());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO.Trim().ToUpper());
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                cmd.Parameters.Add("P_ID_CARRERA", ID_CARRERA);
                cmd.Parameters.Add("P_CODIGO_ALUMNO", CODIGO_ALUMNO);



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

        //Eliminar alumnos
        [WebMethod]
        public string DeleteAlm(string CODIGO_ALUMNO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlDelAlm = AlumnosQuerys.deleteAlm().ToString();

            try
            {
                con.Open();

                OracleCommand cmd = new OracleCommand(sqlDelAlm, con);

                cmd.Parameters.Add("P_CODIGO_ALUMNO", CODIGO_ALUMNO.Trim());
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
