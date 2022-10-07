using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp18
{
    public class checkingDayCompletion
    {
        // class to prevent duplicate data 

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\reception2\Documents\Downloads\FranciscoLairtonRodolfoCalaca (1)\FranciscoLairtonRodolfoCalaca\ConsoleApp18\ConsoleApp18\Clock.mdf"";Integrated Security=True");


        public bool checkDayCompletion(int empNo, string date)
        {
            

            bool dateExits = true;


            con.Open();


            string select = "SELECT COUNT(1) FROM hourBank WHERE empNo = @en AND date = @today";

            SqlCommand cmd = new SqlCommand(select, con);

            cmd.Parameters.AddWithValue("@en", empNo);
            cmd.Parameters.AddWithValue("@today", date);


            string sc = cmd.ExecuteScalar().ToString();

            con.Close();

            if (sc == "1")
            {

                return dateExits;
            }
            else
            {
                dateExits = false;
                return dateExits;
            }

        }
    }
}
