using System;
using BusinessLogic;
using DataAccess.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace UnitTestMangerParking
{
    [TestClass]
    public class UnitTestFacadaParking
    {
        private Mock<IDataAccessParking> _mockDataAccessParking;
        private FacadaParking _facadaParking;

        [TestInitialize]
        public void Initializer()
        {
            Parking parking = new Parking
            {
                Address = "Calle 25 Carrera 20",
                Id = 1,
                Name = "Parquearse01",
                NitRut = "100200300400"
            };

            _mockDataAccessParking = new Mock<IDataAccessParking>();
            _mockDataAccessParking.Setup(r => r.GetParking()).Returns(parking);
            _facadaParking = new FacadaParking(_mockDataAccessParking.Object);
        }

        #region TestGetParking
        [TestMethod]
        public void TestGetParking_Empty()
        {
            var records = _facadaParking.GetParking();
            Assert.IsFalse(records.Id == 0);
        }

        [TestMethod]
        public void TestGetParking_No_Empty()
        {
            var records = _facadaParking.GetParking();
            Assert.IsTrue(records.Id == 1);
        } 
        #endregion
    }
}
