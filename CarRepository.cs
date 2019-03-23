using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CarApp.Core;
using CarApp.Core.Interfaces;

namespace CarApp.Infrastructure
{
    public class CarRepository : ICarRepository
    {
        private CarContext _context;
        public CarRepository(CarContext carContext)
        {
            this._context = carContext;
        }
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        public Car GetCarByID(int carId)
        {
            return _context.Cars.Find(carId);
        }

        public void InsertCar(Car car)
        {
            _context.Cars.Add(car);
        }

        public void DeleteCar(int carID)
        {
            Car car = _context.Cars.Find(carID);

            _context.Cars.Remove(car);
        }

        public void UpdateCar(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
        }

        public IEnumerable<Car> SortOrder(string sortOrder)
        {
            IEnumerable<Car> cars = GetCars();

            switch (sortOrder)
            {
                case "model_desc":
                    cars = cars.OrderByDescending(c => c.Model);
                    break;
                case "model_asc":
                    cars = cars.OrderBy(c => c.Model);
                    break;
                case "engineSize_desc":
                    cars = cars.OrderByDescending(c => c.EngineSize);
                    break;
                case "engineSize_asc":
                    cars = cars.OrderBy(c => c.EngineSize);
                    break;
                case "engineType_desc":
                    cars = cars.OrderByDescending(c => c.EngineType);
                    break;
                case "engineType_asc":
                    cars = cars.OrderBy(c => c.EngineType);
                    break;
                case "transmission_desc":
                    cars = cars.OrderByDescending(c => c.Transmission);
                    break;
                case "transmission_asc":
                    cars = cars.OrderBy(c => c.Transmission);
                    break;
                case "basePrice_desc":
                    cars = cars.OrderByDescending(c => c.BasePrice);
                    break;
                case "basePrice_asc":
                    cars = cars.OrderBy(c => c.BasePrice);
                    break;
                default:
                    cars = cars.OrderBy(c => c.Id);
                    break;
            }
            return cars;
        }
        public IEnumerable<Car> FilterResults(Dictionary<string, string> formCollection)
        {
            IEnumerable<Car> cars = GetCars();

            foreach (var item in formCollection)
            {
                if (item.Key == "CarModel" && item.Value != "SelectModel")
                {
                    cars = cars.Where(c => c.Model == item.Value);
                }
                if (item.Key == "CarEngineType" && item.Value != "SelectEngineType")
                {
                    cars = cars.Where(c => c.EngineType.ToString() == item.Value);
                }
                if (item.Key == "CarTransmission" && item.Value != "SelectTransmission")
                {
                    cars = cars.Where(c => c.Transmission.ToString() == item.Value);
                }
                if (item.Key == "PriceMax" && !string.IsNullOrEmpty(item.Value))
                {
                    cars = cars.Where(c => c.BasePrice <= Convert.ToDecimal(item.Value));
                }
                if (item.Key == "PriceMin" && !string.IsNullOrEmpty(item.Value))
                {
                    cars = cars.Where(c => c.BasePrice >= Convert.ToDecimal(item.Value));
                }
                if (item.Key == "EngineSizeMin" && !string.IsNullOrEmpty(item.Value))
                {
                    cars = cars.Where(c => c.EngineSize <= Convert.ToInt32(item.Value));
                }
                if (item.Key == "EngineSizeMax" && !string.IsNullOrEmpty(item.Value))
                {
                    cars = cars.Where(c => c.EngineSize <= Convert.ToInt32(item.Value));
                }
            }

            return cars;
        }

        public CarViewModel PopulateDropDownsForFiltering()
        {
            IEnumerable<Car> cars = GetCars();

            CarViewModel model = new CarViewModel();

            List<CarViewModel> ListOfCarModels = new List<CarViewModel> { };
            List<CarViewModel> ListOfCarEngineTypes = new List<CarViewModel> { };
            List<CarViewModel> ListOfCarTransmissions= new List<CarViewModel> { };

            ListOfCarModels.Add(new CarViewModel { id = "SelectModel", value = "Select.." });
            ListOfCarEngineTypes.Add(new CarViewModel { id = "SelectEngineType", value = "Select.." });
            ListOfCarTransmissions.Add(new CarViewModel { id = "SelectTransmission", value = "Select.." });

            foreach (var item in cars.Select(x => x.Model).Distinct())
            {
                ListOfCarModels.Add(new CarViewModel { id = item, value = item });
            }
            foreach (var item in cars.Select(x => x.EngineType.ToString()).Distinct())
            {
                ListOfCarEngineTypes.Add(new CarViewModel { id = item, value = item });
            }
            foreach (var item in cars.Select(x => x.Transmission.ToString()).Distinct())
            {
                ListOfCarTransmissions.Add(new CarViewModel { id = item, value = item });
            }

            model.CarModel = ListOfCarModels;
            model.CarEngineType = ListOfCarEngineTypes;
            model.CarTransmission = ListOfCarTransmissions;

            return model;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool dispose)
        {
            if (!this.disposed)
            {
                if (dispose)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
