using System;
using System.Collections.Generic;

namespace CarApp.Core.Interfaces
{
    public interface ICarRepository : IDisposable
    {
        IEnumerable<Car> GetCars();
        Car GetCarByID(int carId);
        void InsertCar(Car car);
        void DeleteCar(int carID);
        void UpdateCar(Car car);
        IEnumerable<Car> SortOrder(string sortOrder);
        IEnumerable<Car> FilterResults(Dictionary<string, string>formCollection);
        CarViewModel PopulateDropDownsForFiltering();
        void Save();
    }
}
