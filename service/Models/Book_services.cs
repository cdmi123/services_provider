using System.Data;
using System.Data.SqlClient;

namespace service.Models
{
    public class Book_services
    {
        public string services_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string location { get; set; }
        public string amount { get; set; }
        public string date { get; set; }
        public string time_sloat { get; set; }
        public string notes { get; set; }
        public string phone_no { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=ServicesDataBase;User Id=sa;pwd=cdmi@3420");

        public int Book_user_services(string booking_date,string services_id,string phone_no,string user_id,string location,string provider_id)
        {
            SqlCommand cmd = new SqlCommand ("insert into book_servives (booking_date,services_id,phone_no,user_id,provider_id,status,location) values ('"+booking_date+"','"+services_id+"','"+phone_no+"','"+user_id+"','"+provider_id+"','0','"+location+"')",con);
            con.Open ();

            return cmd.ExecuteNonQuery();
        }

        public DataSet Get_user_Booking_Details(string provider_id)
        {
            SqlCommand cmd = new SqlCommand("select * from book_servives bs inner join provider on bs.provider_id = provider.id inner join user_register ur on ur.id = bs.user_id inner join services on bs.services_id = services.id where bs.provider_id = '" + provider_id + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet Get_user_Booking_Details_admin()
        {
            SqlCommand cmd = new SqlCommand("select * from book_servives bs inner join provider on bs.provider_id = provider.id inner join user_register ur on ur.id = bs.user_id inner join services on bs.services_id = services.id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public int Update_status(int booking_id)
        {
            SqlCommand cmd = new SqlCommand("update book_servives set status='1' where booking_id = '" + booking_id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        public int Update_status_complete(int booking_id)
        {
            SqlCommand cmd = new SqlCommand("update book_servives set status='2' where booking_id = '" + booking_id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }

        public int Update_status_cancel(int booking_id)
        {
            SqlCommand cmd = new SqlCommand("update book_servives set status='3' where booking_id = '" + booking_id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
    }
}
