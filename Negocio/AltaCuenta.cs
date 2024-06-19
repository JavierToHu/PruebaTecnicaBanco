using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class AltaCuenta
    {
        //Propiedades
        public int IdAltaCuenta { get; set; }

        //Propiedad de navegacion
        public Persona Persona { get; set; }

        public string NumeroCuenta { get; set; }

        public TipoBanco TipoBanco { get; set; }

        //Lista
        public List<AltaCuenta> AltasCuentas { get; set; }
    }

    public partial class AltaCuenta
    {
        public static List<AltaCuenta> GetAllCuentaByIdPersona(int IdPersona)
        {
            try
            {
                //Instancia una Lista
                List<AltaCuenta> ListaAltasCuentas = new List<AltaCuenta>();

                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "SELECT IdAltaCuenta, NumeroCuenta, IdTipoBanco FROM AltaCuenta WHERE IdPersona = @IdPersona";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;

                    SqlParameter parameters = new SqlParameter();
                    parameters = new SqlParameter("@IdPersona", SqlDbType.Int);
                    parameters.Value = IdPersona;

                    cmd.Parameters.Add(parameters);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablePersona = new DataTable();

                    adapter.Fill(tablePersona);

                    if (tablePersona != null)
                    {
                        foreach (DataRow row in tablePersona.Rows)
                        {
                            AltaCuenta altaCuentaObj = new AltaCuenta();
                            altaCuentaObj.IdAltaCuenta = int.Parse(row[0].ToString());
                            altaCuentaObj.NumeroCuenta = row[1].ToString();
                            altaCuentaObj.TipoBanco = new TipoBanco();
                            altaCuentaObj.TipoBanco.IdTipoBanco = int.Parse(row[2].ToString());
                            altaCuentaObj.TipoBanco.NombreTipoBanco = GetNombreBanco(altaCuentaObj.TipoBanco.IdTipoBanco);
                            ListaAltasCuentas.Add(altaCuentaObj);
                        }

                        return ListaAltasCuentas;
                    }
                    else
                    {
                        return ListaAltasCuentas;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool DeleteCuentaByIdAltaCuenta(int IdAltaCuenta)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "DELETE FROM AltaCuenta WHERE IdAltaCuenta = @IdAltaCuenta";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter parameters = new SqlParameter();
                    parameters = new SqlParameter("@IdAltaCuenta", SqlDbType.Int);
                    parameters.Value = IdAltaCuenta;

                    cmd.Parameters.Add(parameters);

                    cmd.Connection.Open();

                    int filaAfectada = cmd.ExecuteNonQuery();

                    if (filaAfectada > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddCuentaByIdAltaCuenta(AltaCuenta altaCuenta)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "INSERT INTO AltaCuenta (IdPersona, NumeroCuenta, IdTipoBanco) VALUES (@IdPersona, @NumeroCuenta, @IdTipoBanco)";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@IdPersona", SqlDbType.Int);
                    parameters[0].Value = altaCuenta.Persona.IdPersona;

                    parameters[1] = new SqlParameter("@NumeroCuenta", SqlDbType.VarChar);
                    parameters[1].Value = altaCuenta.NumeroCuenta;

                    parameters[2] = new SqlParameter("@IdTipoBanco", SqlDbType.Int);
                    parameters[2].Value = altaCuenta.TipoBanco.IdTipoBanco;

                    cmd.Parameters.AddRange(parameters);

                    cmd.Connection.Open();

                    int filaAfectada = cmd.ExecuteNonQuery();

                    if (filaAfectada > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateAltaCuentaByIdAltaCuenta(AltaCuenta altaCuenta)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "UPDATE AltaCuenta " +
                        "SET NumeroCuenta = @NumeroCuenta, IdTipoBanco = @IdTipoBanco " +
                        "WHERE IdAltaCuenta = @IdAltaCuenta";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@NumeroCuenta", SqlDbType.VarChar);
                    parameters[0].Value = altaCuenta.NumeroCuenta;

                    parameters[1] = new SqlParameter("@IdTipoBanco", SqlDbType.Int);
                    parameters[1].Value = altaCuenta.TipoBanco.IdTipoBanco;

                    parameters[2] = new SqlParameter("@IdAltaCuenta", SqlDbType.Int);
                    parameters[2].Value = altaCuenta.Persona.IdPersona;

                    cmd.Parameters.AddRange(parameters);

                    cmd.Connection.Open();

                    int filaAfectada = cmd.ExecuteNonQuery();

                    if (filaAfectada > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetNombreBanco(int IdBanco)
        {
            List<TipoBanco> ListaTiposBanco = new List<TipoBanco>
            {
                  new TipoBanco
                  {
                      IdTipoBanco = 1,
                      NombreTipoBanco = "BBVA"
                  },
                  new TipoBanco
                  {
                      IdTipoBanco= 2,
                      NombreTipoBanco = "Santander"
                  },
                  new TipoBanco
                  {
                      IdTipoBanco = 3,
                      NombreTipoBanco = "ScotiaBank"
                  }
            };

            //string value = ListaTiposBanco.ElementAt(IdBanco).ToString();

            var match = ListaTiposBanco.FirstOrDefault(p => p.IdTipoBanco == IdBanco);
            return match.NombreTipoBanco;
        }
    }
} 