using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess.Contracts;
using DataAccess.SqlServer;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Dtos;
using Moq;
using Unity;

namespace UnitTestMangerParking
{
    [TestClass]
    public class UnitTestFacadaRecord
    {
        private Mock<IDataAccessRecord> _mockDataAccessRecord;
        private Mock<IDataAccessRate> _mockDataAccessRate;
        private FacadaRecord _facadaRecord;
        
        [TestInitialize]
        public void Initializer()
        {
            List<RecordDto> RecordDtoList = new List<RecordDto>
            {
                new RecordDto
                {
                    CellNumber = "123465789",
                    EntryDate = DateTime.Now,
                    Id = 1,
                    OutputDate = DateTime.Now,
                    Plate = "FGH-956",
                    PriceTimeParking = 6000,
                    State = false,
                    TotalDays = 0,
                    TotalHours = 3,
                    VehicleDescription = "Moto"
                },
                new RecordDto
                {
                    CellNumber = "987654321",
                    EntryDate = DateTime.Now,
                    Id = 2,
                    OutputDate = DateTime.Now,
                    Plate = "QAS-850",
                    PriceTimeParking = 12000,
                    State = false,
                    TotalDays = 0,
                    TotalHours = 5,
                    VehicleDescription = "Carro"
                }
            };
            Record record = new Record
            {
                EntryDate = DateTime.Now,
                Id = 10,
                OutputDate = DateTime.Now,
                Plate = "FGH-956",
                PriceTimeParking = 6000,
                State = false,
                TotalDays = 0,
                TotalHours = 3,
                Displacement = 500
            };
            RecordDto recordDto = new RecordDto
            {

                
            };

            _mockDataAccessRecord = new Mock<IDataAccessRecord>();
            _mockDataAccessRate = new Mock<IDataAccessRate>();
            _mockDataAccessRecord.Setup(r => r.GetRecordsAsync()).ReturnsAsync(RecordDtoList);
            _mockDataAccessRecord.Setup(r => r.GetRecordAsync(It.IsAny<int>())).ReturnsAsync(record);
            _mockDataAccessRecord.Setup(r => r.GetRecordPlateStateAsync(It.IsAny<string>())).ReturnsAsync(recordDto);

            _facadaRecord = new FacadaRecord(_mockDataAccessRecord.Object, _mockDataAccessRate.Object);
        }
        
        #region TestGetRecordAsync
        [TestMethod]
        public void TestGetRecordAsync_Found()
        {
            var recordFind = _facadaRecord.GetRecordAsync(1);
            Assert.IsTrue(10 == recordFind.Result.Id);
        }

        [TestMethod]
        public void TestGetRecordAsync_Not_Found()
        {
            var recordFind = _facadaRecord.GetRecordAsync(1);
            Assert.IsFalse(5 == recordFind.Result.Id);
        } 
        #endregion

        #region TestGetRecordsAsync
        [TestMethod]
        public void TestGetRecordsAsync_No_Empty()
        {
            var records = _facadaRecord.GetRecordsAsync();
            Assert.IsTrue(records.Result.Count == 2);
        }

        [TestMethod]
        public void TestGetRecordsAsync_Empty()
        {
            var records = _facadaRecord.GetRecordsAsync();
            Assert.IsFalse(records.Result.Count == 0);
        } 
        #endregion

        #region TestValidationLetter
        [TestMethod]
        public void TestvalidationLetter_Diff_A()
        {
            var Plate = "WSD-123";
            var Day = 0;
            var privateTest = new PrivateType(typeof(FacadaRecord));
            var result = privateTest.InvokeStatic("ValidationLetterAndDay", Plate, Day);
            Assert.IsTrue((bool)result);
        }

        [TestMethod]
        public void TestvalidationLetter_Equal_A_In_Permited_Day()
        {
            var Plate = "ASD-123";
            string[] Days = { "0", "1" };
            var result = false;

            var privateTest = new PrivateType(typeof(FacadaRecord));

            foreach (var item in Days)
            {
                result = (bool)privateTest.InvokeStatic("ValidationLetterAndDay", Plate, Convert.ToInt32(item));
                if (result == false)
                {
                    Assert.IsTrue(result);
                }
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestvalidationLetter_Equal_A_In_No_Permited_Day()
        {
            var Plate = "AHG-963";
            string[] Days = { "1", "5" };
            var result = false;

            var privateTest = new PrivateType(typeof(FacadaRecord));

            foreach (var item in Days)
            {
                result = (bool)privateTest.InvokeStatic("ValidationLetterAndDay", Plate, Convert.ToInt32(item));



                if (result == false)
                {
                    Assert.IsFalse(result);
                }
            }



            Assert.IsFalse(result);
        } 
        #endregion

        #region TestValidationMotorcycleDisplacement
        [TestMethod]
        public void TestValidationMotorcycleDisplacement_Vehicle_As_Car()
        {
            int Displacement = 700;
            int VehicleId = 1;
            decimal overrun = 0;
            var privateTest = new PrivateType(typeof(FacadaRecord));

            var result = privateTest.InvokeStatic("ValidationMotorcycleDisplacement", Displacement, VehicleId);

            var resultGetValue = result.GetType().GetProperty("Result").GetValue(result);

            Assert.AreEqual(overrun, Convert.ToDecimal(resultGetValue));
        }

        [TestMethod]
        public void TestValidationMotorcycleDisplacement_Vehicle_As_Motorcycle()
        {
            int Displacement = 700;
            int VehicleId = 2;
            decimal overrun = 2000;
            var privateTest = new PrivateType(typeof(FacadaRecord));

            var result = privateTest.InvokeStatic("ValidationMotorcycleDisplacement", Displacement, VehicleId);

            var resultGetValue = result.GetType().GetProperty("Result").GetValue(result);

            Assert.AreEqual(overrun, Convert.ToDecimal(resultGetValue));
        } 
        #endregion

        #region TestCalculateTimeParking
        [TestMethod]
        public void TestCalculateTimeParking_Vehicle_As_Car()
        {

            ////Triple AAA

            //    //Arrange
            //    HttpConfiguration httpConfiguration 
            //    WebApi webApi 

            //var fechaAyer = "2018/07/05";
            //var fechaHoy = "2018/07/05";



            ////Act

            //webApi.Riges*(httpConfiguration)



            ////Assert

            //Assert

            //var date = DateTime.Now.AddDays(-1);
            //date = date.AddHours(-3);

            var entryDate = new DateTime(2018, 7, 9, 11, 0, 0);
            var outputDate = new DateTime(2018, 7, 10, 15, 0, 0);            

            decimal dayPrice = 8000;
            decimal hourPrice = 1000;
            var priceParking = 11000;

            Record record = new Record
            {
                EntryDate = entryDate,
                CellId = 1,
                Displacement = 700,
                Id = 1,
                Plate = "RTG-905",
                State = true
            };

            var privateTest = new PrivateType(typeof(FacadaRecord));
            var result = privateTest.InvokeStatic("CalculateTimeParking", outputDate, record, dayPrice, hourPrice);
            var resultGetValue = result.GetType().GetProperty("Result").GetValue(result);
            Assert.AreEqual(priceParking, Convert.ToDecimal(resultGetValue));
        }

        [TestMethod]
        public void TestCalculateTimeParking_Vehicle_As_Motorcycle()
        {
            //var date = DateTime.Now.AddHours(-10);

            var entryDate = new DateTime(2018, 7, 9, 20, 0, 0);
            var outputDate = new DateTime(2018, 7, 10, 6, 0, 0);

            decimal dayPrice = 4000;
            decimal hourPrice = 500;
            var priceParking = 4000;

            Record record = new Record
            {
                EntryDate = entryDate,
                CellId = 1,
                Displacement = 700,
                Id = 1,
                Plate = "RTG-905",
                State = true
            };

            var privateTest = new PrivateType(typeof(FacadaRecord));
            var result = privateTest.InvokeStatic("CalculateTimeParking", outputDate, record, dayPrice, hourPrice);
            var resultGetValue = result.GetType().GetProperty("Result").GetValue(result);
            Assert.AreEqual(priceParking, Convert.ToDecimal(resultGetValue));
        } 
        #endregion
    }
}
