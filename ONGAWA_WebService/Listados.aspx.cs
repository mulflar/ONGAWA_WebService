using ONGAWA_WebService.servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ONGAWA_WebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string connection = "Data Source=H4GSERVER\\SQLEXPRESS;Initial Catalog = ONGAWA; Integrated Security = True; Connect Timeout = 15; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Clientes.DataSource = ClientesDataSource.GetTableFromCli("codigo, Nombre, Direccion, Localidad, Telefono");
            //Clientes.DataBind();

            /*ConexionBD servicio = new ConexionBD();
            OdbcCommand query = new OdbcCommand();*/
            ConexionSQL servicio = new ConexionSQL(connection);
            SqlCommand query = new SqlCommand();

     
            query.CommandText = "SELECT * FROM usuarios" ;

          
            GridView1.DataSource = servicio.Ejecuta(query);
            GridView1.DataBind();
        }
    }
}