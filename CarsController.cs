using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CarApp.Core;
using CarApp.Infrastructure;
using CarApp.Core.Interfaces;

namespace CarApp.Web.Controllers
{
    public class CarsController : Controller
    {
        private ICarRepository _carRepository;
        public CarsController(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public ActionResult Index(string sortOrder)
        {
            ViewBag.ModelSortParm = sortOrder == "model_asc" ? "model_desc" : "model_asc";
            ViewBag.EngineTypeSortParm = sortOrder == "engineType_asc" ? "engineType_desc" : "engineType_asc";
            ViewBag.EngineSizeSortParm = sortOrder == "engineSize_asc" ? "engineSize_desc" : "engineSize_asc";
            ViewBag.TransmissionSortParm = sortOrder == "transmission_asc" ? "transmission_desc" : "transmission_asc";
            ViewBag.BasePriceSortParm = sortOrder == "basePrice_asc" ? "basePrice_desc" : "basePrice_asc";

            var model = _carRepository.PopulateDropDownsForFiltering();

            if (string.IsNullOrEmpty(sortOrder))
            {
                model.Cars = _carRepository.GetCars();
            }
            else
            {
                model.Cars = _carRepository.SortOrder(sortOrder);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult FilterResults(FormCollection formCollection)
        {
            var form = formCollection.AllKeys.ToDictionary(k => k, k => formCollection[k]);
            CarViewModel model = new CarViewModel();
           
            model = _carRepository.PopulateDropDownsForFiltering();
            model.Cars = _carRepository.FilterResults(form);

            return View("Index",model);
        }

        public ActionResult Details(int id)
        {
            Car car = _carRepository.GetCarByID(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,EngineType,EngineSize,Transmission,BasePrice")] Car car)
        {
            if (ModelState.IsValid)
            {
                _carRepository.InsertCar(car);
                _carRepository.Save();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        public ActionResult Edit(int id)
        {
            Car car = _carRepository.GetCarByID(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,EngineType,EngineSize,Transmission,BasePrice")] Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _carRepository.UpdateCar(car);
                        _carRepository.Save();
                        return RedirectToAction("Index");
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }
            return View(car);
        }

        public ActionResult Delete(int id)
        {
            Car car = _carRepository.GetCarByID(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = _carRepository.GetCarByID(id);
            _carRepository.DeleteCar(id);
            _carRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _carRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
