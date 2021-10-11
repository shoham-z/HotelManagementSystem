using System;
using System.Globalization;

namespace dotNet_5780_01._2_9890_7533_7283
{
    class Program
    {
        /*static void check(int d, int m)
        {
            Console.WriteLine(d+"/"+m);
        }*/ 
        static void RedPrint()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Error");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Order_vacation(ref bool[,] order)
        {
            int arrivalday = 0, arrivalmonth = 0, days_of_stay = 0, lastday = 0, lastmonth = 0;
        out_of_balance:
            Console.WriteLine("From what day do you want to be in our apartment? Please use the next format: DD/MM");
            string arrival_time;
            Console.Write("Enter your arival date:");
            arrival_time = Console.ReadLine();
            string[] lineSplitted = arrival_time.Split('/');
            arrivalday = int.Parse(lineSplitted[0]) - 1;
            arrivalmonth = int.Parse(lineSplitted[1]) - 1;
            if (arrivalday > 30 || arrivalday < 0 || arrivalmonth > 11 || arrivalmonth < 0)
            {
                Program.RedPrint();
                Console.WriteLine(", the date you chose is invalid.");
                goto out_of_balance;
            }
            Console.WriteLine("For how many days would you like to stay in our apartment?");
            days_of_stay = int.Parse(Console.ReadLine());
            lastday = arrivalday + days_of_stay ;
            lastmonth = arrivalmonth;
            while (lastday >= 30)
            {
                lastmonth += 1;
                lastday = lastday -30 ;
            }
            if (lastmonth > 11)
            {
                Program.RedPrint();
                Console.WriteLine(", you can order a vacation in the current year!"); 
                goto out_of_balance;
            }
           
            bool booly=true;
            int tempmonth = arrivalmonth;
            int tempday = arrivalday;
            for (int i =0; i < days_of_stay-1; i++)
            {
                if (order[tempday,tempmonth])
                {
                    booly = false;
                    
                }
                tempday++;
               if(tempday>30)
                {
                    tempday -= 31;
                    tempmonth++;
                }
            } 
            if (booly==false)
                {
                Console.WriteLine("Your request has been denied.");
                }
            else
            {
                tempmonth = arrivalmonth;
                tempday = arrivalday;
                for (int i = 0; i < days_of_stay-1; i++)
                {
                    order[tempday, tempmonth] = true ;  
                    tempday++;
                    if (tempday > 30)
                    {
                        tempday -= 31;
                        tempmonth++;
                    }
                }
                Console.WriteLine("Your request has been approved!");
            }
        }
        static void check_orders(ref bool[,] order)
        {
            /* דבר ראשון שצריך לעשות : 1-למצוא רצף של ימים תפוסים וגם מה הימים הראשון והאחרון
             2- להדפיס*/
            for (int i = 0; i < 12; i++)
            {
                for (int  j = 0;  j <31;  j++)
                {
                    if (order[j,i])
                    {
                        Console.Write((j+1)+"/"+(i+1)+" - ");
                       bully:
                        while (j<=30 && order[j, i])
                        {
                            j++;                
                        }
                        if (j>30)
                        {
                            i++;
                            j -= 30;
                            goto bully;
                        }
                        Console.WriteLine((j+1)+"/"+(i+1)); 
                    }   
                }
            }
        }
        static void all_orders(ref bool[,] order)
        {
            float counter = 0;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (order[j,i])
                    {
                        counter++;
                    }
                }
            }
            float pre_of_take = ((counter)/(12 * 31)) *100;
            Console.WriteLine("Total number of occupied days per year is-"+counter);
            Console.WriteLine("Percentage of annual occupancy is-"+pre_of_take+"%");
        }
       
        static void Main(string[] args)
        {
            int actionKey = 0;
           bool [,] order=new bool [31,12];
            for (int i = 0; i < 12; i++)//set the matrix value to false
            {
                for (int j = 0; j < 31; j++)
                {
                    order[j, i] = false;
                }
            }       
            Console.WriteLine("Hey! welcome to our new vacation site!");
            while (actionKey != 4)
            {
                Console.WriteLine("What would you like to do?");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1 is to order a vacation.");
                Console.WriteLine("2 is to see taken days.");
                Console.WriteLine("3 is to check the yearly tonnage.");
                Console.WriteLine("4 is to close the system.");
                Console.ForegroundColor = ConsoleColor.Gray;
                try {
                    actionKey = int.Parse(Console.ReadLine());

                }
                catch {
                    Program.RedPrint();
                    Console.WriteLine("What you have chose is invalid");
                    continue;
                }
                                switch (actionKey)
                {
                    case (1):
                        Program.Order_vacation(ref order);
                        break;
                    case (2):
                        Program.check_orders(ref order);
                        break;
                    case(3):
                        Program.all_orders(ref order);
                        break;
                    case(4):
                        Console.WriteLine("You chose exit, good bye!!!");
                        break;
                    default:
                        Program.RedPrint();
                        Console.WriteLine(" ,the number you chose is invalid.");
                        break;
                }
            }
            Console.ReadKey();
        }
    }  
}
