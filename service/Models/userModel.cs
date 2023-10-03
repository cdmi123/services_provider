using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    public class userModel
    {

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");

        public int user_register(string name, string email, string password)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[user_register](user_name,email,password)values('" + name + "','" + email + "','" + password+ "')", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        public DataSet Login(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[user_register] where email='" + email + "' and password='" + password + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}
