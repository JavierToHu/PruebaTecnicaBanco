using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class Persona
    {
        //Propiedades
        public int IdPersona { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        public TipoPersonaFiscal tipoPersonaFiscal { get; set; }
        //public string TipoPersonaFiscal { get; set; }

        public PaisOrigen paisOrigen { get; set; }
        //public string PaisOrigen { get; set; }

        public Sexo sexo { get; set; }
        //public char Sexo { get; set; }

        public string CURP { get; set; }
        public string RFC { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        public List<Persona> Personas { get; set; }
    }

    public partial class Persona
    {
        //Metodos
        public static List<Persona> GetAllPersona()
        {
            try
            {
                //Instancia la lista
                List<Persona> ListPersona = new List<Persona>();

                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "SELECT IdPersona, Nombre, ApellidoPaterno, ApellidoMaterno, IdTipoPersonaFiscal, " +
                        "IdPaisOrigen, IdSexo, Curp, RFC, FechaNacimiento FROM Persona";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablePersona = new DataTable();

                    adapter.Fill(tablePersona);

                    if (tablePersona != null)
                    {
                        foreach (DataRow row in tablePersona.Rows)
                        {
                            Persona personaObJ = new Persona();
                            personaObJ.IdPersona =  int.Parse(row[0].ToString());
                            personaObJ.Nombre = row[1].ToString();
                            personaObJ.ApellidoPaterno = row[2].ToString();
                            personaObJ.ApellidoMaterno = row[3].ToString();

                            personaObJ.tipoPersonaFiscal = new TipoPersonaFiscal();
                            personaObJ.tipoPersonaFiscal.IdTipoPersonaFisica = int.Parse(row[4].ToString());

                            ListPersona.Add(personaObJ);    
                        }

                        return ListPersona;
                    }
                    else
                    {
                        return ListPersona;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Persona GetByIdPersona(int IdPersona)
        {
            try
            {
                //Instanciar un objeto
                Persona personaObj = new Persona();

                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "SELECT IdPersona, Nombre, ApellidoPaterno, ApellidoMaterno, IdTipoPersonaFiscal, " +
                        "IdPaisOrigen, IdSexo, Curp, RFC, FechaNacimiento FROM Persona WHERE IdPersona = @IdPersona";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;

                    SqlParameter parameters = new SqlParameter();
                    parameters = new SqlParameter("@IdPersona", SqlDbType.Int);
                    parameters.Value = IdPersona;

                    cmd.Parameters.Add(parameters);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dataTablePersona = new DataTable();

                    adapter.Fill(dataTablePersona);

                    if (dataTablePersona != null && dataTablePersona.Rows.Count > 0)
                    {
                        DataRow row = dataTablePersona.Rows[0];

                        personaObj.IdPersona = int.Parse(row[0].ToString());
                        personaObj.Nombre = row[1].ToString();
                        personaObj.ApellidoPaterno = row[2].ToString();
                        personaObj.ApellidoMaterno = row[3].ToString();

                        personaObj.tipoPersonaFiscal = new TipoPersonaFiscal();
                        personaObj.tipoPersonaFiscal.IdTipoPersonaFisica = int.Parse(row[4].ToString());

                        personaObj.paisOrigen = new PaisOrigen();
                        personaObj.paisOrigen.IdPaisOrigen = int.Parse(row[5].ToString());

                        personaObj.sexo = new Sexo();
                        personaObj.sexo.IdSexo = int.Parse(row[6].ToString());

                        personaObj.CURP = row[7].ToString();
                        personaObj.RFC = row[8].ToString();
                        personaObj.FechaNacimiento = row[9].ToString();

                        return personaObj;
                    }
                    else
                    {
                        return personaObj;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool DeletePersona(int IdPersona)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "DELETE FROM Persona WHERE IdPersona = @IdPersona";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter parameters = new SqlParameter();
                    parameters = new SqlParameter("@IdPersona", SqlDbType.Int);
                    parameters.Value = IdPersona;

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

        public static bool UpdatePersona(Persona personaUpdate)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "UPDATE Persona " +
                        "SET Nombre = @Nombre, " +
                        "ApellidoPaterno = @ApellidoPaterno, " +
                        "ApellidoMaterno = @ApellidoMaterno, " +
                        "IdTipoPersonaFiscal = @IdTipoPersonaFiscal, " +
                        "IdPaisOrigen = @IdPaisOrigen, " +
                        "IdSexo = @IdSexo, " +
                        "Curp = @Curp, " +
                        "RFC = @RFC, " +
                        "FechaNacimiento = @FechaNacimiento " +
                        "WHERE IdPersona = @IdPersona;";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parameters[0].Value = personaUpdate.Nombre;

                    parameters[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parameters[1].Value = personaUpdate.ApellidoPaterno;

                    parameters[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    parameters[2].Value = personaUpdate.ApellidoMaterno;

                    parameters[3] = new SqlParameter("@IdTipoPersonaFiscal", SqlDbType.Int);
                    parameters[3].Value = personaUpdate.tipoPersonaFiscal.IdTipoPersonaFisica;

                    parameters[4] = new SqlParameter("@IdPaisOrigen", SqlDbType.Int);
                    parameters[4].Value = personaUpdate.paisOrigen.IdPaisOrigen;

                    parameters[5] = new SqlParameter("@IdSexo", SqlDbType.Int);
                    parameters[5].Value = personaUpdate.sexo.IdSexo;

                    parameters[6] = new SqlParameter("@Curp", SqlDbType.VarChar);
                    parameters[6].Value = personaUpdate.CURP;

                    parameters[7] = new SqlParameter("@RFC", SqlDbType.VarChar);
                    parameters[7].Value = personaUpdate.RFC;

                    parameters[8] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
                    parameters[8].Value = personaUpdate.FechaNacimiento;

                    parameters[9] = new SqlParameter("@IdPersona", SqlDbType.Int);
                    parameters[9].Value = personaUpdate.IdPersona;

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

        public static bool AddPersona(Persona personaAdd)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(Datos.Conexion.Get()))
                {
                    string query = "INSERT INTO Persona (Nombre, ApellidoPaterno, ApellidoMaterno, IdTipoPersonaFiscal, IdPaisOrigen, IdSexo, Curp, RFC, FechaNacimiento) " +
                        "VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @IdTipoPersonaFiscal, @IdPaisOrigen, @IdSexo, @Curp, @RFC, @FechaNacimiento)";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] parameters = new SqlParameter[9];
                    parameters[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parameters[0].Value = personaAdd.Nombre;

                    parameters[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parameters[1].Value = personaAdd.ApellidoPaterno;

                    parameters[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                    parameters[2].Value = personaAdd.ApellidoMaterno;

                    parameters[3] = new SqlParameter("@IdTipoPersonaFiscal", SqlDbType.Int);
                    parameters[3].Value = personaAdd.tipoPersonaFiscal.IdTipoPersonaFisica;

                    parameters[4] = new SqlParameter("@IdPaisOrigen", SqlDbType.Int);
                    parameters[4].Value = personaAdd.paisOrigen.IdPaisOrigen;

                    parameters[5] = new SqlParameter("@IdSexo", SqlDbType.Int);
                    parameters[5].Value = personaAdd.sexo.IdSexo;

                    parameters[6] = new SqlParameter("@Curp", SqlDbType.VarChar);
                    parameters[6].Value = personaAdd.CURP;

                    parameters[7] = new SqlParameter("@RFC", SqlDbType.VarChar);
                    parameters[7].Value = personaAdd.RFC;

                    parameters[8] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
                    parameters[8].Value = personaAdd.FechaNacimiento;

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
    }
}