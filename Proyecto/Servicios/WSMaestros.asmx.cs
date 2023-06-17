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
    /// Descripción breve de WSMaestros
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSMaestros : System.Web.Services.WebService
    {
        //Fecha y hora

        [WebMethod]
        public string getDate()
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("SELECT SYSDATE FROM DUAL", con);
                string fecha = cmd.ExecuteScalar().ToString();
                con.Close();
                return fecha;
            }
            catch (Exception ex)
            {
                con.Close();
                return "Error " + ex.ToString();
            }
        }

        
        //Traer a todos los registros
        [WebMethod]
        public List<Maestros> GetAllMaestros()
        {
            DataTable dt = new DataTable();
            List<Maestros> lista = new List<Maestros>();
            String sqlMaes = MaestrosQuerys.selectMaes().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlMaes, con))
                    {

                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Maestros
                                {
                                    ID_MAESTRO = dt.Rows[i]["ID_MAESTRO"].ToString(),
                                    DPI = dt.Rows[i]["DPI"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    APELLIDO = dt.Rows[i]["APELLIDO"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    EDAD = dt.Rows[i]["EDAD"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Maestros
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
                lista.Add(new Maestros
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Traer los datos por su ID
        [WebMethod]
        public List<Maestros> GetMaestro(int ID_MAESTRO)
        {
            DataTable dt = new DataTable();
            List<Maestros> lista = new List<Maestros>();
            String sqlMaes = MaestrosQuerys.selectMaesEspecifico().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlMaes, con))
                    {
                        cmd.Parameters.Add("P_ID_MAESTRO", ID_MAESTRO);
                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Maestros
                                {
                                    ID_MAESTRO = dt.Rows[i]["ID_MAESTRO"].ToString(),
                                    DPI = dt.Rows[i]["DPI"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    APELLIDO = dt.Rows[i]["APELLIDO"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    EDAD = dt.Rows[i]["EDAD"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    ESTABLECIMIENTO = dt.Rows[i]["ESTABLECIMIENTO"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Maestros
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
                lista.Add(new Maestros
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Insertar Maestros
        [WebMethod]
        public string InsertMaes(string DPI, string NOMBRE, string APELLIDO, string DIRECCION, string EDAD, string TELEFONO, int ID_ESTABLECIMIENTO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlInsMaes = MaestrosQuerys.insertMaes().ToString();
            try
            {

                con.Open();
                OracleCommand cmd = new OracleCommand(sqlInsMaes, con);
                cmd.Parameters.Add("P_DPI", DPI.Trim());
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim());
                cmd.Parameters.Add("P_APELLIDO", APELLIDO.Trim());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim());
                cmd.Parameters.Add("P_EDAD", EDAD.Trim());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO.Trim());
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
        public string updateMaes(int ID_MAESTRO, string DPI, string NOMBRE, string APELLIDO, string DIRECCION, string EDAD, string TELEFONO, int ID_ESTABLECIMIENTO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            string sqlUpdMaes = MaestrosQuerys.updateMaes().ToString();

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sqlUpdMaes, con);

                
                cmd.Parameters.Add("P_DPI", DPI.Trim().ToUpper());
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim().ToUpper());
                cmd.Parameters.Add("P_APELLIDO", APELLIDO.Trim().ToUpper());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim().ToUpper());
                cmd.Parameters.Add("P_EDAD", EDAD.Trim().ToUpper());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO.Trim().ToUpper());
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                cmd.Parameters.Add("P_ID_MAESTRO", ID_MAESTRO);
                

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

        //Eliminar Maestro

        [WebMethod]
        public string DeleteMaes(string ID_MAESTRO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlDelMaes =  MaestrosQuerys.deleteMaes().ToString();

            try
            {
                con.Open();

                OracleCommand cmd = new OracleCommand(sqlDelMaes, con);

                cmd.Parameters.Add("P_ID_MAESTRO", ID_MAESTRO.Trim());
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



            
    

