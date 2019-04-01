using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(Chat));
            sh.Open();
            Console.WriteLine("Для завершения нажмите <ENTER>\n");
            do
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);

            sh.Close();
        }
    }
}
