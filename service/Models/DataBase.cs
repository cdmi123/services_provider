using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace service.Models
{
    
    public class DataBase
    {
        public String email { get; set; }

        public String password { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");
        public DataSet LoginAdmin(string email,string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[admin] where email='" + email + "' and password='" + password + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet user_count()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[user_register]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;    
        }

        public DataSet provider_count()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[provider]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}
