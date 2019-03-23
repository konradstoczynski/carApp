using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Core
{
    public enum EngineType { Petrol, Diesel, Hybrid, Electric }
    public enum Transmission { Automatic, Manual }
    public class Car
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("Model")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Column("EngineType")]
        [Display(Name = "Engine Type")]
        public EngineType? EngineType { get; set; }

        [Required]
        [Column("EngineSize")]
        [Display(Name = "Engine Size")]
        public int EngineSize { get; set; }

        [Required]
        [Column("Transmission")]
        [Display(Name = "Transmission")]
        public Transmission? Transmission { get; set; }

        [Required]
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        [Column("BasePrice")]
        [Display(Name = "Base Price")]
        public decimal BasePrice { get; set; }

    }
}
