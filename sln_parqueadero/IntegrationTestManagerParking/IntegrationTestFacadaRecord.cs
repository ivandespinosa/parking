using System;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess.Contracts;
using DataAccess.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Unity;

namespace IntegrationTestManagerParking
{
    [TestClass]
    public class IntegrationTestFacadaRecord
    {
        IUnityContainer _unityContainer;
        FacadaRecord _facadaRecord;
        FacadaCell _facadaCell;
        FacadaParking _facadaParking;
        FacadaVehicle _facadaVehicle;

        [TestInitialize]
        public void Initializer()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterInstance(new Context(Effort.DbConnectionFactory.CreateTransient()));
            _unityContainer.RegisterType<IDataAccessRecord, DataAccessSqlServerRecord>();
            _unityContainer.RegisterType<IDataAccessRate, DataAccessSqlServerRate>();
            _facadaRecord = _unityContainer.Resolve<FacadaRecord>();

            _unityContainer.RegisterType<IDataAccessCell, DataAccessSqlServerCell>();            
            _facadaCell = _unityContainer.Resolve<FacadaCell>();

            _unityContainer.RegisterType<IDataAccessParking, DataAccessSqlServerParking>();
            _facadaParking = _unityContainer.Resolve<FacadaParking>();

            _unityContainer.RegisterType<IDataAccessVehicle, DataAccessSqlServerVehicle>();
            _facadaVehicle = _unityContainer.Resolve<FacadaVehicle>();
        }

        #region TestGetRecordPlateStateAsync
        [TestMethod]
        public async Task TestGetRecordPlateStateAsync_Found()
        {
            //Arrange
            Parking parking = new Parking
            {
                Address = "Calle 22 Carrera 44",
                Id = 1,
                Name = "Parquearse01",
                NitRut = "800900500"
            };

            var parkingSave = _facadaParking.InsertParking(parking);

            Vehicle vehicle = new Vehicle
            {
                Description = "Carro",
                Id = 1                
            };

            var vehicleSave = await _facadaVehicle.InsertVehicleAsync(vehicle);

            Cell cell = new Cell
            {
                Active = true,
                CellNumber = "C01",
                Id = 1,
                ParkingId = 1,
                State = true,
                VehicleId = 1
            };

            var cellSave = _facadaCell.InsertCell(cell);

            Record record = new Record
            {
                CellId = 1,
                Displacement = 700,
                EntryDate = DateTime.Now,
                OutputDate = DateTime.Now,
                Plate = "BAA-009",
                PriceTimeParking = 0,
                State = true,
                TotalDays = 1,
                TotalHours = 1
            };
            var registerSave = await _facadaRecord.RecordVehicleAsync(record);

            //Act
            var Plate = "BAA-009";
            var recordFind = await _facadaRecord.GetRecordPlateStateAsync(Plate);

            //Assert
            Assert.IsTrue(Plate == recordFind.Plate);
        }

        [TestMethod]
        public async Task TestGetRecordPlateStateAsync_Not_Found()
        {
            //Arrange
            Parking parking = new Parking
            {
                Address = "Calle 22 Carrera 44",
                Id = 1,
                Name = "Parquearse01",
                NitRut = "800900500"
            };

            var parkingSave = _facadaParking.InsertParking(parking);

            Vehicle vehicle = new Vehicle
            {
                Description = "Carro",
                Id = 1
            };

            var vehicleSave = await _facadaVehicle.InsertVehicleAsync(vehicle);

            Cell cell = new Cell
            {
                Active = true,
                CellNumber = "C01",
                Id = 1,
                ParkingId = 1,
                State = true,
                VehicleId = 1
            };

            var cellSave = _facadaCell.InsertCell(cell);

            Record record = new Record
            {
                CellId = 1,
                Displacement = 700,
                EntryDate = DateTime.Now,
                OutputDate = DateTime.Now,
                Plate = "BAA-009",
                PriceTimeParking = 0,
                State = true,
                TotalDays = 1,
                TotalHours = 1
            };
            var registerSave = await _facadaRecord.RecordVehicleAsync(record);

            //Act
            var Plate = "XYZ-852";
            var recordFind = _facadaRecord.GetRecordPlateStateAsync(Plate);

            //Assert
            Assert.AreNotEqual(Plate, recordFind.Result);
        }
        #endregion


    }
}
