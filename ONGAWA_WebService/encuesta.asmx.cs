using ONGAWA_WebService.objetos;
using ONGAWA_WebService.servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ONGAWA_WebService
{

    /// <summary>
    /// Summary description for encuesta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class encuesta : System.Web.Services.WebService
    {
        static string connection = "Data Source=H4GSERVER\\SQLEXPRESS;Initial Catalog = ONGAWA; Integrated Security = True; Connect Timeout = 15; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static string comprobante = "jkdgnnjbgjk34564534";
        [WebMethod]
        public void insertuser(string usuario, string contraseña, string email, int telefono, string zona)
        {

            if(comprobante == zona)
            {
                ConexionSQL nueva = new ConexionSQL(connection);

                //Obtención del último id
                SqlCommand query1 = new SqlCommand();
                query1.CommandText = "SELECT COUNT(ID) FROM usuarios";
                int IDuser = int.Parse(nueva.Scalar(query1));

                //Introducción de usuario
                SqlCommand query2 = new SqlCommand();
                query2.Parameters.Add("@id", SqlDbType.Int).Value = IDuser+1;
                query2.Parameters.Add("@user", SqlDbType.Char).Value = usuario;
                query2.Parameters.Add("@pass", SqlDbType.Char).Value = contraseña;
                query2.Parameters.Add("@email", SqlDbType.Char).Value = email;
                query2.Parameters.Add("@telefono", SqlDbType.Char).Value = telefono;
                query2.CommandText = "INSERT INTO usuarios VALUES (@id,@user,@pass,@email,@telefono)";
                  nueva.Ejecuta(query2);
            }
        }
        [WebMethod]
        public int comprobaruser(string usuario,string pass, string zona)
        {
            int IDuser = 0;
            if (comprobante == zona)
            {
                ConexionSQL nueva = new ConexionSQL(connection);
          
                SqlCommand query3 = new SqlCommand();
                query3.Parameters.Add("@user", SqlDbType.Char).Value = usuario;
                query3.Parameters.Add("@pass", SqlDbType.Char).Value = pass;
                query3.CommandText = "SELECT id FROM usuarios WHERE usuario = @user AND contraseña = @pass";
                IDuser = int.Parse(nueva.Scalar(query3));

                
            }
            return IDuser;
        }



        [WebMethod]
        public void insertreg (string encuesta_r, string zona)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (comprobante == zona)
            {
                encuesta_obj datos = new encuesta_obj(serializer.Deserialize<encuesta_raw>(encuesta_r));
                ConexionSQL nueva = new ConexionSQL(connection);

                //Obtención del último id
                SqlCommand query = new SqlCommand();
                query.CommandText = "SELECT COUNT(ID) FROM registros";
                int IDreg = int.Parse(nueva.Scalar(query));

                //Introducción de registro objetos
                SqlCommand query1 = new SqlCommand();
                query1.Parameters.Add("@id", SqlDbType.Int).Value = IDreg + 1;
                query1.Parameters.Add("@lon", SqlDbType.BigInt).Value = datos.longitud;
                query1.Parameters.Add("@lat", SqlDbType.BigInt).Value = datos.latitud;
                query1.Parameters.Add("@pais", SqlDbType.Char).Value = datos.pais;
                query1.Parameters.Add("@ciudad", SqlDbType.Char).Value = datos.ciudad;
                query1.Parameters.Add("@direccion", SqlDbType.Char).Value = datos.direccion;
                query1.Parameters.Add("@CP", SqlDbType.Char).Value = datos.CP;
                query1.Parameters.Add("@ventilacion", SqlDbType.Bit).Value = datos.ventilacion;
                query1.Parameters.Add("@privacidad", SqlDbType.Bit).Value = datos.privacidad;
                query1.Parameters.Add("@cie", SqlDbType.Int).Value = datos.cie;
                query1.Parameters.Add("@mos", SqlDbType.Bit).Value = datos.mos;
                query1.Parameters.Add("@olo", SqlDbType.Bit).Value = datos.olo;
                query1.Parameters.Add("@agu", SqlDbType.Bit).Value = datos.agu;
                query1.Parameters.Add("@mat", SqlDbType.Int).Value = datos.mat;
                query1.Parameters.Add("@referencia", SqlDbType.Bit).Value = datos.referencia;

                query1.CommandText = "INSERT INTO registros VALUES (@id,@lon,@lat,@pais,@ciudad,@direccion,@CP,@ventilacion,@privacidad,@cie,@mos,@olo,@agu,@mat,@referencia)";


                nueva.Ejecuta(query1);
            }
        }

    }
}
