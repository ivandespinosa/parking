using DataAccess.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FacadaParking
    {
        private readonly IDataAccessParking _dataAccessParking;

        public FacadaParking(IDataAccessParking dataAccessParking)
        {
            _dataAccessParking = dataAccessParking;
        }

        public Parking GetParking()
        {
            return _dataAccessParking.GetParking();
        }

        public bool InsertParking(Parking parking)
        {
            return _dataAccessParking.InsertParking(parking) == 1;
        }

    }
}
