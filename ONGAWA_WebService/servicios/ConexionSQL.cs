using System;
using System.Data;
using System.Data.SqlClient;

namespace ONGAWA_WebService.servicios
{
    public class ConexionSQL
    {
        string connectionString;

        public bool conectado;
        public string error;
        public ConexionSQL(string conn)
        {
            connectionString = conn.Replace("\\\\", "\\");
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                conectado = true;
                cnn.Close();
            }
            catch (Exception ex)
            {
                conectado = false;
                error = ex.ToString();
            }
        }

        public int Ejecuta(SqlCommand query)
        {
            int values = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    query.Connection = cnn;
                    cnn.Open();
                    values = query.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                values = -1;
                error = ex.ToString();
            }
            return values;
        }
        /// <summary>
        /// Consulta a base de datos con retorno de Tabla de datos
        /// </summary>
        /// <param name="respuesta">Si hay un error este valor se actualiza y el retorno es null</param>
        /// <param name="select">OdbcCommand con la QUERY</param>
        /// <returns>Tabla de datos</returns>
        public DataTable Select(SqlCommand select)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    select.Connection = cnn;
                    cnn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = select;
                    da.Fill(dt);
                    da.Dispose();
                    cnn.Close();
                    select.Dispose();
                }
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        /// <summary>
        /// Metodo que realiza una consulta que devuelve un valor
        /// </summary>
        /// <param name="respuesta">Si hay un error este valor se actualiza y el retorno es null</param>
        /// <param name="query">string con la QUERY</param>
        /// <returns>Si la consulta es correcta devuelve un valor, si no devuelve null y pasa el codigo de error por referencia </returns>
        public string Scalar(SqlCommand select)
        {
            string result = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    select.Connection = cnn;
                    cnn.Open();
                    result = select.ExecuteScalar().ToString();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                result = null;
            }
            return result;
        }
    }
}



