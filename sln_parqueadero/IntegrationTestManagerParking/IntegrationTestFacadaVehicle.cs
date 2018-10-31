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
    public class IntegrationTestFacadaVehicle
    {
        IUnityContainer _unityContainer;
        FacadaVehicle _facadaVehicle;

        [TestInitialize]
        public void Initializer()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterInstance(new Context(Effort.DbConnectionFactory.CreateTransient()));
            _unityContainer.RegisterType<IDataAccessVehicle, DataAccessSqlServerVehicle>();

            _facadaVehicle = _unityContainer.Resolve<FacadaVehicle>();
        }

        #region TestInsertVehicleAsync
        [TestMethod]
        public async Task TestInsertVehicleAsync_Valid()
        {
            Vehicle vehicle = new Vehicle
            {
                Description = "Camioneta"
            };

            var result = await _facadaVehicle.InsertVehicleAsync(vehicle);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestInsertVehicleAsync_Not_Valid()
        {
            Vehicle vehicle = new Vehicle
            {

            };

            var result = await _facadaVehicle.InsertVehicleAsync(vehicle);
            Assert.IsFalse(result);
        }
        #endregion
    }
}
