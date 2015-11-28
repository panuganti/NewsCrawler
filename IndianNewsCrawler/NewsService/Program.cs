using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace NewsService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(NewsServiceLibrary.NewsService)))
            {
                host.Open();
                Console.WriteLine("Service Started..");
                Console.ReadLine();
            }
        }
    }
}
