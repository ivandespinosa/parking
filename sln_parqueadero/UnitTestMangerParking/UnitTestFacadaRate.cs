using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace UnitTestMangerParking
{
    [TestClass]
    public class UnitTestFacadaRate
    {
        private Mock<IDataAccessRate> _mockDataAccessRate;
        private FacadaRate _facadaRate;

        [TestInitialize]
        public void Initializer()
        {
            List<Rate> rateList = new List<Rate>
            {
                new Rate
                {
                    Description = "Día Carro",
                    EndTime = 24,
                    Id = 1,
                    Price = 8000,
                    StartTime = 9,
                    VehicleId = 1
                },
                new Rate
                {
                    Description = "Hora Carro",
                    EndTime = 9,
                    Id = 2,
                    Price = 1000,
                    StartTime = 0,
                    VehicleId = 1
                },
                new Rate
                {
                    Description = "Día Moto",
                    EndTime = 24,
                    Id = 3,
                    Price = 4000,
                    StartTime = 9,
                    VehicleId = 2
                },
                new Rate
                {
                    Description = "Hora Moto",
                    EndTime = 9,
                    Id = 4,
                    Price = 500,
                    StartTime = 0,
                    VehicleId = 2
                }
            };
            List<Rate> rateCarList = new List<Rate>
            {
                new Rate
                {
                    Description = "Día Carro",
                    EndTime = 24,
                    Id = 1,
                    Price = 8000,
                    StartTime = 9,
                    VehicleId = 1
                },
                new Rate
                {
                    Description = "Hora Carro",
                    EndTime = 9,
                    Id = 2,
                    Price = 1000,
                    StartTime = 0,
                    VehicleId = 1
                }
            };
            List<Rate> rateMotorcycleList = new List<Rate>
            {
                new Rate
                {
                    Description = "Día Moto",
                    EndTime = 24,
                    Id = 1,
                    Price = 4000,
                    StartTime = 9,
                    VehicleId = 2
                },
                new Rate
                {
                    Description = "Hora Moto",
                    EndTime = 9,
                    Id = 2,
                    Price = 500,
                    StartTime = 0,
                    VehicleId = 2
                }
            };
            Rate rate = new Rate
            {
                Description = "Día Carro",
                EndTime = 24,
                Id = 1,
                Price = 8000,
                StartTime = 9,
                VehicleId = 1
            };

            _mockDataAccessRate = new Mock<IDataAccessRate>();
            _mockDataAccessRate.Setup(r => r.GetRatesAsync()).ReturnsAsync(rateList);
            _mockDataAccessRate.Setup(r => r.GetRateVehicleIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(rate);

            _facadaRate = new FacadaRate(_mockDataAccessRate.Object);
        }

        #region TestGetRatesAsync
        [TestMethod]
        public void TestGetRatesAsync_Empty()
        {
            var records = _facadaRate.GetRatesAsync();
            Assert.IsFalse(records.Result.Count == 0);
        }

        [TestMethod]
        public void TestGetRatesAsync_No_Empty()
        {
            var records = _facadaRate.GetRatesAsync();
            Assert.IsTrue(records.Result.Count == 4);
        }
        #endregion

        #region GetRateVehicleIdAsync
        [TestMethod]
        public async Task GetRateVehicleIdAsync_Found()
        {
            var VehicleId = 1;
            var Hour = 1;

            var rate = await _facadaRate.GetRateVehicleIdAsync(VehicleId, Hour);

            Assert.IsTrue(rate.VehicleId == 1);
        }

        [TestMethod]
        public async Task GetRateVehicleIdAsync_No_Found()
        {
            var VehicleId = 5;
            var Hour = 1;

            var rate = await _facadaRate.GetRateVehicleIdAsync(VehicleId, Hour);

            Assert.IsFalse(rate.VehicleId == 5);
        } 
        #endregion
    }
}
