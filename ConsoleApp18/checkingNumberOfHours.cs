using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp18
{
    public class checkingNumberOfHours
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\reception2\Documents\Downloads\FranciscoLairtonRodolfoCalaca (1)\FranciscoLairtonRodolfoCalaca\ConsoleApp18\ConsoleApp18\Clock.mdf"";Integrated Security=True");


        public float checkNumberOfHours(int empNo)
        {
            float numberOfHours = 0;

            con.Open();

            string select = "SELECT SUM(total_hours) FROM hourBank WHERE empNo = @en";

            SqlCommand cmd = new SqlCommand(select, con);

            cmd.Parameters.AddWithValue("@en", empNo);

            try
            {
                string totalHours = cmd.ExecuteScalar().ToString();
                numberOfHours = float.Parse(totalHours);

            }
            catch (Exception)
            {
                Console.WriteLine("\n\tSearching hours...");
            }

            con.Close();

            return numberOfHours;

        }

    }
}
