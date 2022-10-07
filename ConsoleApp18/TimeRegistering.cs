using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp18
{
    public class TimeRegistering

    {
        dateGrabbing dg = new dateGrabbing();
        convertingHours ch = new convertingHours();
        checkingDayCompletion cdc = new checkingDayCompletion();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\reception2\Documents\Downloads\FranciscoLairtonRodolfoCalaca (1)\FranciscoLairtonRodolfoCalaca\ConsoleApp18\ConsoleApp18\Clock.mdf"";Integrated Security=True");


        public int month = -1;

        public void InsertTime(int shift_code, int empNo)
        {

            SqlCommand cmd = new SqlCommand();

            string timeNow = DateTime.UtcNow.ToShortTimeString();

            string today = dg.dateGrabber().ToString();




            switch (shift_code)
            {


                case 0:
                    bool dateExists = cdc.checkDayCompletion(empNo, today);

                    if (dateExists)
                    {
                        Console.WriteLine("\n\nEmployee has already registered time today. See you tomorrow!");

                    }
                    else
                    {
                        string insert = "INSERT INTO hourBank(empNo, date, start_time, start_break, end_break, end_time, total_hours) VALUES(@en, @today, @st, @sb, @eb, @et, @th)";
                        //string update = "UPDATE hourBank SET start_time = @st  WHERE empNo = @en";
                        cmd = new SqlCommand(insert, con);
                        cmd.Parameters.AddWithValue("@en", empNo);
                        cmd.Parameters.AddWithValue("@today", today);
                        cmd.Parameters.AddWithValue("@st", timeNow);
                        cmd.Parameters.AddWithValue("@sb", "0");
                        cmd.Parameters.AddWithValue("@eb", "0");
                        cmd.Parameters.AddWithValue("@et", "0");
                        cmd.Parameters.AddWithValue("@th", "0");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    break;

                case 1:
                    string update = "UPDATE hourBank SET start_break = @st WHERE date = @today";
                    cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@st", timeNow);
                    cmd.Parameters.AddWithValue("@today", today);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    break;

                case 2:
                    update = "UPDATE hourBank SET end_break = @st WHERE date = @today";
                    cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@st", timeNow);
                    cmd.Parameters.AddWithValue("@today", today);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    break;

                case 3:
                    string startHour = selectStartHour(empNo);
                    decimal totalHours = ch.convertHours(startHour, timeNow);
                    updateTotal_hours(totalHours);
                    update = "UPDATE hourBank SET end_time = @st WHERE date = @today";
                    cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@st", timeNow);
                    cmd.Parameters.AddWithValue("th", totalHours);
                    cmd.Parameters.AddWithValue("@today", today);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    break;
            }


        }
    

        public string selectStartHour(int empNo)
        {

            con.Open();

            string today = dg.dateGrabber().ToString();


            string select = "SELECT start_time FROM hourBank WHERE empNo = @en AND date = @today";

            SqlCommand cmd = new SqlCommand(select, con);

            cmd.Parameters.AddWithValue("@en", empNo);
            cmd.Parameters.AddWithValue("@today", today);


            string startHour = cmd.ExecuteScalar().ToString();

            con.Close();

            return startHour;
        }

        public void updateTotal_hours(decimal total_hours)
        {

            con.Open();
            string today = dg.dateGrabber().ToString();

            string update = "UPDATE hourBank SET total_hours = @th WHERE date = @today";
            SqlCommand cmd = new SqlCommand(update, con);

            cmd.Parameters.AddWithValue("@th", total_hours);
            cmd.Parameters.AddWithValue("@today", today);

            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
