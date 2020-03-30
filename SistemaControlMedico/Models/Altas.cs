namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altas
    {
        [Key]
        public int idAlta { get; set; }

        [Display(Name ="Seleccione el Ingreso"), Required(ErrorMessage ="El campo {0} no puede estar vacio")]
        public int ingreso { get; set; }

        [Column(TypeName = "date"), Display(Name = "Fecha de Salida"), Required(ErrorMessage ="Debe seleccionar la fecha de salida")]
        public DateTime? fechaSalida { get; set; }

        [Display(Name = "Monto"), Required(ErrorMessage ="Para calcular el Monto debe seleccionar una fecha de salida")]
        public int monto { get; set; }

        public virtual Ingresos Ingresos { get; set; }

        public  int CalcularMonto()
        {
            return monto = 22;
        }
    }
}
