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
    /// Descripción breve de WSEstablecimientos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSEstablecimientos : System.Web.Services.WebService
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
        public List<Establecimientos> GetAllEstablecimientos()
        {
            DataTable dt = new DataTable();
            List<Establecimientos> lista = new List<Establecimientos>();
            String sqlEsta = EstablecimientosQuerys.selectEsta().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlEsta, con))
                    {

                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Establecimientos
                                {
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    FAX = dt.Rows[i]["FAX"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Establecimientos
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
                lista.Add(new Establecimientos
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Traer los datos por su ID
        [WebMethod]
        public List<Establecimientos> GetPais(int ID_ESTABLECIMIENTO)
        {
            DataTable dt = new DataTable();
            List<Establecimientos> lista = new List<Establecimientos>();
            String sqlEsta = EstablecimientosQuerys.selectEstaEspecifico().ToString();
            try
            {
                using (OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM"].ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(sqlEsta, con))
                    {
                        cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                        con.Open();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(dt);
                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lista.Add(new Establecimientos
                                {
                                    ID_ESTABLECIMIENTO = dt.Rows[i]["ID_ESTABLECIMIENTO"].ToString(),
                                    NOMBRE = dt.Rows[i]["NOMBRE"].ToString(),
                                    DIRECCION = dt.Rows[i]["DIRECCION"].ToString(),
                                    TELEFONO = dt.Rows[i]["TELEFONO"].ToString(),
                                    FAX = dt.Rows[i]["FAX"].ToString()
                                });
                            }
                            return lista;
                        }
                        else
                        {
                            lista.Add(new Establecimientos
                            {
                                NOMBRE= "No hay datos".ToString()
                            });
                            return lista;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lista.Add(new Establecimientos
                {
                    NOMBRE = "Error " + ex.ToString()
                });
                return lista;
            }
        }

        //Insertar Establecimientos
        [WebMethod]
        public string InsertEsta(string NOMBRE, string DIRECCION, string TELEFONO, string FAX)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlInsEsta = EstablecimientosQuerys.insertEsta().ToString();
            try
            {

                con.Open();
                OracleCommand cmd = new OracleCommand(sqlInsEsta, con);
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO.Trim());
                cmd.Parameters.Add("P_FAX", FAX.ToUpper());
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
        public string updateEsta(int ID_ESTABLECIMIENTO, string NOMBRE, string DIRECCION, string TELEFONO, string FAX)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            string sqlUpdEsta = EstablecimientosQuerys.updateEsta().ToString();

            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sqlUpdEsta, con);

                
                cmd.Parameters.Add("P_NOMBRE", NOMBRE.Trim().ToUpper());
                cmd.Parameters.Add("P_DIRECCION", DIRECCION.Trim().ToUpper());
                cmd.Parameters.Add("P_TELEFONO", TELEFONO.Trim().ToUpper());
                cmd.Parameters.Add("P_FAX", FAX.Trim().ToUpper());
                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO);
                
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

        //Eliminar Establecimiento

        [WebMethod]
        public string DeleteEsta(string ID_ESTABLECIMIENTO)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SYSTEM"].ToString());
            String sqlDelEsta = EstablecimientosQuerys.deleteEsta().ToString();

            try
            {
                con.Open();

                OracleCommand cmd = new OracleCommand(sqlDelEsta, con);

                cmd.Parameters.Add("P_ID_ESTABLECIMIENTO", ID_ESTABLECIMIENTO.Trim());
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
