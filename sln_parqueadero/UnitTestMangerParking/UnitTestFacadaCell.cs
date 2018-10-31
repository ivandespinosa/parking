using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Dtos;
using Moq;

namespace UnitTestMangerParking
{
    [TestClass]
    public class UnitTestFacadaCell
    {
        private Mock<IDataAccessCell> _mockDataAccessCell;
        private FacadaCell _facadaCell;

        [TestInitialize]
        public void Initializer()
        {
            List<CellDto> CellCarList = new List<CellDto>
            {
                new CellDto
                {
                    CellNumber = "C01",
                    Id = 1
                },
                new CellDto
                {
                    CellNumber = "C02",
                    Id = 2
                },
                new CellDto
                {
                    CellNumber = "C03",
                    Id = 3
                },
                new CellDto
                {
                    CellNumber = "C04",
                    Id = 4
                }
            };

            List<CellDto> CellMotorcycleList = new List<CellDto>
            {
                new CellDto
                {
                    CellNumber = "M01",
                    Id = 21
                },
                new CellDto
                {
                    CellNumber = "M02",
                    Id = 22
                },
                new CellDto
                {
                    CellNumber = "M03",
                    Id = 23
                },
                new CellDto
                {
                    CellNumber = "M04",
                    Id = 24
                },
                new CellDto
                {
                    CellNumber = "M05",
                    Id = 25
                }
            };

            _mockDataAccessCell = new Mock<IDataAccessCell>();
            _mockDataAccessCell.Setup(c => c.GetCellsAsync(It.IsAny<int>())).ReturnsAsync((int vehicleId) => vehicleId == 1 ? CellCarList : (vehicleId == 2 ? CellMotorcycleList : new List<CellDto>()));
            _mockDataAccessCell.Setup(c => c.CellState(It.IsAny<Cell>())).Returns(1);
            _facadaCell = new FacadaCell(_mockDataAccessCell.Object);
        }

        #region TestGetCellsAsync
        [TestMethod]
        public async Task TestGetCellsAsync_Vehicle_As_Car()
        {
            var vehicleId = 1;
            var records = await _facadaCell.GetCellsAsync(vehicleId);
            Assert.IsTrue(records.Count == 4);
        }

        [TestMethod]
        public async Task TestGetCellsAsync_Vehicle_As_Motorcycle()
        {
            var vehicleId = 2;
            var records = await _facadaCell.GetCellsAsync(vehicleId);
            Assert.IsTrue(records.Count == 5);
        }

        [TestMethod]
        public async Task TestGetCellsAsync_Not_Found()
        {
            var vehicleId = 3;
            var records = await _facadaCell.GetCellsAsync(vehicleId);
            Assert.IsTrue(records.Count == 0);
        }
        #endregion

        #region TestCellState
        [TestMethod]
        public void TestCellState_Valid()
        {
            Cell cell = new Cell
            {
                Active = true,
                CellNumber = "C01",
                Id = 1,
                ParkingId = 1,
                State = false,
                VehicleId = 1
            };

            var result = _facadaCell.CellState(cell);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCellState_No_Valid()
        {
            Cell cell = new Cell();

            var result = _facadaCell.CellState(cell);
            Assert.IsFalse(result);
        } 
        #endregion
    }
}
