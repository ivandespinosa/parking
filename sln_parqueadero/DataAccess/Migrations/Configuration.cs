namespace DataAccess.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.SqlServer.ContextSqlServer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DataAccess.SqlServer.ContextSqlServer";
        }

        protected override void Seed(DataAccess.SqlServer.ContextSqlServer context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
            context.Parkings.AddOrUpdate(
                p => p.NitRut,
                new Models.Parking { Id = 1, Address = "Calle 55 Carrera 21", Name = "Parquearse01", NitRut = "900800700" }
                );

            context.Vehicles.AddOrUpdate(
                v => v.Description,
                new Models.Vehicle { Description = "Carros",
                    
                    Rates = new List<Rate> {
                        new Rate { Description = "Dia Carro", StartTime = 9, EndTime = 24, Price = 8000 },
                        new Rate { Description = "Hora Carro", StartTime = 0, EndTime = 9, Price = 1000 } },
                    Cells = new List<Cell> {
                        new Cell { Active = true, CellNumber = "C01", State = true, ParkingId = 1,
                        Records = new List<Record>{
                            new Record{ Displacement = 0, EntryDate = DateTime.Now, OutputDate = null, Plate = "AAA-009", PriceTimeParking = 0, State = true, TotalDays = 1 } } },
                        new Cell { Active = true, CellNumber = "C02", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C03", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C04", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C05", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C06", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C07", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C08", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C09", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C10", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C11", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C12", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C13", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C14", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C15", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C16", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C17", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C18", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C19", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "C20", State = false, ParkingId = 1 } } },
                new Models.Vehicle { Description = "Motos",
                
                    Rates = new List<Rate> {
                        new Rate { Description = "Día Moto", StartTime = 9, EndTime = 24, Price = 4000 },
                        new Rate { Description = "Hora Moto", StartTime = 0, EndTime = 9, Price = 500 } },
                    Cells = new List<Cell> {
                        new Cell { Active = true, CellNumber = "M01", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M02", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M03", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M04", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M05", State = true, ParkingId = 1,
                            Records = new List<Record>{
                                new Record { Displacement = 700, EntryDate = DateTime.Now, OutputDate = null, Plate = "ZZZ-98A", PriceTimeParking = 0, State = true, TotalDays = 1 } } },
                        new Cell { Active = true, CellNumber = "M06", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M07", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M08", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M09", State = false, ParkingId = 1 },
                        new Cell { Active = true, CellNumber = "M10", State = false, ParkingId = 1 } } }
                );            
        }
    }
}
