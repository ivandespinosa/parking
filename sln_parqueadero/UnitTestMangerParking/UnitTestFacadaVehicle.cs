using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess.Contracts;
using DataAccess.SqlServer;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Unity;

namespace UnitTestMangerParking
{
    [TestClass]
    public class UnitTestFacadaVehicle
    {        
        private Mock<IDataAccessVehicle> _mockDataAccessVehicle;
        private FacadaVehicle _facadaVehicle;

        [TestInitialize]
        public void Initializer()
        {
            List<Vehicle> VehicleList = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Description = "Carros"
                },
                new Vehicle
                {
                    Id = 2,
                    Description = "Motos"
                }
            };
            Vehicle vehicle = new Vehicle
            {
                Id = 10,
                Description = "Carros"
            };

            _mockDataAccessVehicle = new Mock<IDataAccessVehicle>();

            _mockDataAccessVehicle.Setup(r => r.GetVehiclesAsync()).ReturnsAsync(VehicleList);
            _mockDataAccessVehicle.Setup(r => r.GetVehicleAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            _mockDataAccessVehicle.Setup(v => v.InsertVehicle(It.IsAny<Vehicle>())).ReturnsAsync(1);
            _facadaVehicle = new FacadaVehicle(_mockDataAccessVehicle.Object);
        }

        #region TestGetVehicleAsync
        [TestMethod]
        public async Task TestGetVehicleAsync_Found()
        {
            var recordFind = await _facadaVehicle.GetVehicle(1);
            Assert.IsTrue(10 == recordFind.Id);
        }

        [TestMethod]
        public async Task TestGetVehicleAsync_Not_Found()
        {
            var recordFind = await _facadaVehicle.GetVehicle(1);
            Assert.IsFalse(5 == recordFind.Id);
        } 
        #endregion

        #region TestGetVehiclesAsync
        [TestMethod]
        public void TestGetVehiclesAsync_No_Empty()
        {
            var records = _facadaVehicle.GetVehiclesAsync();
            Assert.IsTrue(records.Result.Count == 2);
        }

        [TestMethod]
        public void TestGetVehiclesAsync_Empty()
        {
            var records = _facadaVehicle.GetVehiclesAsync();
            Assert.IsFalse(records.Result.Count == 0);
        }
        #endregion

        
    }
}
