using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IDataAccessParking
    {        
        Parking GetParking();
        int InsertParking(Parking parking);
    }
}
