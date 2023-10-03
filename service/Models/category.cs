

using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    
    public class category
    {
       
        public string cat_name { get; set; }
        public string cat_image { get; set; }
        public string discription { get; set; }


        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");
        public int addcategory(string name,string image_name)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[category](cat_name,cat_image)values('" + name + "','" + image_name + "')", con);
               con.Open();

            return cmd.ExecuteNonQuery();
        }

        public DataSet select()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[category]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}
