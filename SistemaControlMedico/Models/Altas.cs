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

        public int ingreso { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fechaSalida { get; set; }

        public int monto { get; set; }

        public virtual Ingresos Ingresos { get; set; }
    }
}
