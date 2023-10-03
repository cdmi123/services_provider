

using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    
    public class services
    {
       
        public string name { get; set; }
        public string image { get; set; }
        public string price { get; set; }
        public string contactno { get; set; }
        public string cat_name{ get; set; }
        public int provider_id{ get; set; }

        public string src_keyword { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");
        public int addservices(string name,string image, string price, string contactno, string cat_name,int provider_id)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[services](name,image,price,contactno,cat_name,provider_id,status)values('" + name + "','" + image + "','" + price + "','" + contactno+ "','" + cat_name+ "','"+ provider_id + "','0')", con);
               con.Open();

            return cmd.ExecuteNonQuery();
        }

        public DataSet select(string name)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[services] where cat_name like '%"+name+"%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet select_provider()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[provider] where p_status = '1'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }


        public DataSet select_details(int id)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[services] where id ="+id, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public int update_services_status(int id)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[services] set status = '1' where id = '" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        public DataSet getservices()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[services] INNER JOIN provider ON provider.id = services.provider_id ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet select_services_details(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * from services where id = '"+id+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet GetTime()
        {
            SqlCommand cmd = new SqlCommand("SELECT * from services_time", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}
