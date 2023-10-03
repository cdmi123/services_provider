using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    public class provider_model
    {
        public string password { get; set; }
        public string email { get; set; }


        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");

        public DataSet Login(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[provider] where email='" + email + "' and password='" + password + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

    }
}
