using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp18
{
    public class checkEmployeeNumber : TimeRegistering
    {
        TimeRegistering tr = new TimeRegistering();
        dateGrabbing dg = new dateGrabbing();
        checkingDayCompletion cdc = new checkingDayCompletion();

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\reception2\Documents\Downloads\FranciscoLairtonRodolfoCalaca (1)\FranciscoLairtonRodolfoCalaca\ConsoleApp18\ConsoleApp18\Clock.mdf"";Integrated Security=True");


        public bool checkEmployeeNo(int emp_no)
        {

            con.Open();

            SqlDataReader dr = null;

            string select = "SELECT * FROM Employee WHERE EmpNo = @en";

            SqlCommand cmd = new SqlCommand(select, con);

            cmd.Parameters.AddWithValue("@en", emp_no);

            dr = cmd.ExecuteReader();



            if (dr.Read())
            {

                con.Close();
                return true;

            }
            else
            {
                con.Close();
                return false;

            }



        }

        public void checkShiftCode(int emp_no)
        {

            con.Open();


            string select = "SELECT shiftCode FROM Employee WHERE EmpNo = @en";

            SqlCommand cmd = new SqlCommand(select, con);

            cmd.Parameters.AddWithValue("@en", emp_no);

            string sc = cmd.ExecuteScalar().ToString();

            int shiftCode = int.Parse(sc);
            string today = dg.dateGrabber().ToString();
            string timeNow = DateTime.UtcNow.ToShortTimeString();

            con.Close();

            if (shiftCode == 0)
            {

                bool dateExists = cdc.checkDayCompletion(emp_no, today);

                if (dateExists)
                {
                    InsertTime(shiftCode, emp_no);

                }
                else
                {
                    Console.WriteLine($"\n\tClocking in at {timeNow}");
                    InsertTime(shiftCode, emp_no);
                    updateShiftCode(shiftCode + 1, emp_no);

                }

            }
            else if (shiftCode == 1)
            {
                Console.WriteLine($"\n\tClocking in to break at {timeNow}");
                InsertTime(shiftCode, emp_no);
                updateShiftCode(shiftCode + 1, emp_no);

            }
            else if (shiftCode == 2)
            {
                Console.WriteLine($"\n\tClocking out of break at {timeNow}");
                InsertTime(shiftCode, emp_no);
                updateShiftCode(shiftCode + 1, emp_no);

            }
            else if (shiftCode == 3)
            {
                Console.WriteLine($"\n\tClocking out at {timeNow}. See you tomorrow.");
                InsertTime(shiftCode, emp_no);
                updateShiftCode(0, emp_no);

            }
            else
            {
                Console.WriteLine("\nERROR, no shift found. NOT SAVED!");

            }


        }

        public void updateShiftCode(int shiftCode, int empNo)
        {

            SqlCommand cmd = new SqlCommand();


            string update = "UPDATE Employee SET shiftCode = @sc WHERE EmpNo = @en";
            cmd = new SqlCommand(update, con);
            cmd.Parameters.AddWithValue("@sc", shiftCode);
            cmd.Parameters.AddWithValue("@en", empNo);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
    }
}
