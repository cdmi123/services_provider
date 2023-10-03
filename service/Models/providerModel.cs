using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    public class providerModel
    {
        public string name { get; set; }    
        public string email { get; set; }
        public string password { get; set; }


        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");

        public int  provider_register(string name, string email, string password,string image)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[provider](provider_name,email,password,p_status,provider_image)values('" + name + "','" + email + "','" + password + "','0','"+image+"')",con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        public DataSet provider_details()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[provider]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public int update_provider_status(int provider_id)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[provider] set p_status = '1' where id = '" + provider_id + "'",con);
            con.Open();

            return cmd.ExecuteNonQuery();

        }

        public DataSet Getproviderinfo(string id)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[provider] where id='"+id+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;

        }

        public DataSet getservices(string id,int status)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[services] INNER JOIN provider ON provider.id = services.provider_id AND services.status = '"+status+ "' AND provider.id = '"+id+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet get_booking_details(string provider_id)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[book_servives] where provider_id='" + provider_id + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;

        }

        public DataSet get_services_details(string provider_id)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[services] where provider_id='" + provider_id + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;

        }

    }
}
