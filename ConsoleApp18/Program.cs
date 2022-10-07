using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp18
{
    public class Program
    {

        static void Main(string[] args)
        {
            checkEmployeeNumber cen = new checkEmployeeNumber();
            checkingNumberOfHours cnh = new checkingNumberOfHours();

            bool employee = false;
            int empNo = -1;
            int option = -1;

            Console.WriteLine("\n\t\t-------O'CLOCK------");

            do
            {

                Console.Write("\n\tEnter Employee Number for MAIN MENU or HIT 0 TO EXIT APP: ");
                try
                {
                    empNo = int.Parse(Console.ReadLine());
                    if (empNo != 0)
                    {
                        employee = cen.checkEmployeeNo(empNo);
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("\n\tInvalid employee number. Please use numbers ONLY.");
                    empNo = -1;
                }

                if (employee == false && empNo != 0)
                {
                    Console.Write("\n\tEmployee not found, try again or HIT 0 TO EXIT: ");

                }

                else if (employee)
                {
                    Console.WriteLine("\n\tMAIN MENU - Select one option - or HIT 0 TO MAIN MENU");
                    Console.WriteLine("\n\t1 - Time Registering");
                    Console.Write("\n\t2 - Check all Hours: ");

                    while (option == -1 || option > 12)
                    {
                        try
                        {
                            option = int.Parse(Console.ReadLine());

                            if (option > 12)
                            {
                                option = -1;
                                Console.Write("\n\tInvalid option, please choose between 1-3 or HIT 0 TO HOME: ");
                            }
                            else if (option == 0)
                            {
                                option = -1;
                                break;

                            }
                        }
                        catch (Exception)
                        {
                            option = -1;
                            Console.Write("\n\tInvalid option, please choose between 1-3 or HIT 0 TO HOME: ");
                        }
                    }

                    switch (option)
                    {

                        case 0:
                            break;

                        case 1:
                            Console.WriteLine("\n\tChecking Employee's Hour Bank...");
                            cen.checkShiftCode(empNo);
                            option = -1;
                            break;

                        case 2:
                            float totalHours = cnh.checkNumberOfHours(empNo);
                            if (totalHours < 1)
                            {
                                Console.WriteLine("\n\tEmployee do not have any hours due. Call 012 for assistance");
                            }
                            else
                            {
                                Console.WriteLine($"\n\tEmployee has {totalHours} hours due.");
                            }
                            option = -1;
                            break;

                   
                    }

                }


            } while (empNo != 0);

            Console.WriteLine("\n\tThanks for using the application.");
            Console.ReadLine();


        }
        
    }
}
