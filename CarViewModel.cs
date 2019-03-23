using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core
{
    public class CarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<CarViewModel> CarModel { get; set; }
        public IEnumerable<CarViewModel> CarEngineType { get; set; }
        public IEnumerable<CarViewModel> CarEngineSize { get; set; }
        public IEnumerable<CarViewModel> CarTransmission { get; set; }

        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        public decimal CarMaxPrice { get; set; }
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        public decimal CarMinPrice { get; set; }
        public int CarEngineMax { get; set; }
        public int CarEngineMin { get; set; }
        public string id { get; set; }
        public string value { get; set; }
    }
}
