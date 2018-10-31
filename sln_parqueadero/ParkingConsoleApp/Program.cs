using BusinessLogic;
using DataAccess.Contracts;
using DataAccess.SqlServer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ParkingConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<Context, ContextSqlServer>();
            unityContainer.RegisterType<IDataAccessParking, DataAccessSqlServerParking>();

            FacadaParking facadaParking = unityContainer.Resolve<FacadaParking>();
            
            var parking = facadaParking.GetParking();

            Console.WriteLine("Ok " + parking.Name);
            Console.ReadKey();
        }
    }
}
