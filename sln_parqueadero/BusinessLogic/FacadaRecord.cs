using DataAccess.Contracts;
using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FacadaRecord
    {
        private readonly IDataAccessRecord _dataAccessRecord;
        private readonly IDataAccessRate _dataAccessRate;

        enum Vehicle { Car = 1, Motorcycle = 2, Displacement = 500, overcrowdingCapacity = 2000 };

        #region Constructor
        public FacadaRecord(IDataAccessRecord dataAccessRecord, IDataAccessRate dataAccessRate)
        {
            _dataAccessRecord = dataAccessRecord;
            _dataAccessRate = dataAccessRate;
        } 
        #endregion

        #region GetRecordsAsync - GetRecordAsync
        public async Task<List<RecordDto>> GetRecordsAsync()
        {
            return await _dataAccessRecord.GetRecordsAsync().ConfigureAwait(false);
        }

        public async Task<Record> GetRecordAsync(int Id)
        {
            return await _dataAccessRecord.GetRecordAsync(Id).ConfigureAwait(false);
        } 

        public async Task<RecordDto> GetRecordPlateStateAsync(string Plaque)
        {
            return await _dataAccessRecord.GetRecordPlateStateAsync(Plaque).ConfigureAwait(false);
        }
        #endregion

        #region RecordVehicleAsync
        public async Task<bool> RecordVehicleAsync(Record record)
        {
            var recordFind = await _dataAccessRecord.GetRecordPlateStateAsync(record.Plate).ConfigureAwait(false);
            
            if (recordFind != null)
            {
                return false;
            }

            var Plate = record.Plate;
            var Day = (int)DateTime.Now.DayOfWeek;
            if (!ValidationLetterAndDay(Plate, Day))
            {
                return false;
            }

            record.EntryDate = DateTime.Now;
            record.OutputDate = null;
            record.State = true;
            return await _dataAccessRecord.RecordVehicleAsync(record).ConfigureAwait(false) == 2;
        }

        #region ValidatingLetterDay
        private static bool ValidationLetterAndDay(string plate, int day)
        {
            var letter = plate.Substring(0, 1);

            if (letter != "A")
            {
                // Register
                return true;

            }
            else if (letter == "A" && day < 2)
            {
                // Register
                return true;
            }
            else
            {
                // No register
                return false;
            }
        } 
        #endregion

        #endregion

        #region OutputVehicleAsync
        public async Task<bool> OutputVehicleAsync(Record record)
        {
            var vehicleId = record.Cell.VehicleId;
            var StartTimeDay = 9;
            var StartTimeHour = 0;

            if (!String.IsNullOrEmpty(record.Id.ToString()))
            {
                var daysPrice = await _dataAccessRate.GetRateVehicleIdAsync(vehicleId, StartTimeDay).ConfigureAwait(false);
                var hourPrice = await _dataAccessRate.GetRateVehicleIdAsync(vehicleId, StartTimeHour).ConfigureAwait(false);
                
                record.PriceTimeParking += await CalculateTimeParking(DateTime.Now, record, daysPrice.Price, hourPrice.Price).ConfigureAwait(false);
                record.PriceTimeParking += await ValidationMotorcycleDisplacement(record.Displacement, vehicleId).ConfigureAwait(false);

                record.State = !record.State;
                record.OutputDate = DateTime.Now;
                record.State = false;
                bool response = await _dataAccessRecord.OutputVehicleAsync(record).ConfigureAwait(false) == 2;
                return response;
            }
            else
            {
                return false;
            }
        }

        #region Calculating Price
        private static async Task<decimal> CalculateTimeParking(DateTime outputDate, Record record, decimal dayPrice, decimal hourPrice)
        {
            var days = 0;
            var hours = 0;
            decimal priceParking = 0;

            var timeParking = Convert.ToInt32(Math.Ceiling(outputDate.Subtract(record.EntryDate).TotalHours));
            if (timeParking < 9)
            {
                hours = timeParking;
            }
            else if (timeParking <= 24)
            {
                days = 1;
            }
            else
            {
                days = Math.DivRem(timeParking, 25, out hours);
                if (hours >= 9)
                {
                    days++;
                    hours -= 9;
                }
            }
            
            priceParking = days * dayPrice + hours * hourPrice;
            record.PriceTimeParking = priceParking;

            var result = await Task.Run(() => {
                return priceParking;
            }).ConfigureAwait(false);

            return result;

        }
        #endregion

        #region ValidatingMotorcycleDisplacement
        private static async Task<decimal> ValidationMotorcycleDisplacement(int Displacement, int VehicleId)
        {
            decimal overcrowdingCapacity = 0;

            if ((int)Vehicle.Motorcycle == VehicleId && Displacement > (int)Vehicle.Displacement)
            {
                overcrowdingCapacity += (int)Vehicle.overcrowdingCapacity;
            }
                        
            var result = await Task.Run(() => {
                return overcrowdingCapacity;
            }).ConfigureAwait(false);

            return result;
        }
        #endregion

        #endregion

    }
}
