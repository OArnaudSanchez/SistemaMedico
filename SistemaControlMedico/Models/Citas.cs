namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Citas
    {
        [Key]
        public int idCita { get; set; }

        public int paciente { get; set; }

        public int medico { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public virtual Medicos Medicos { get; set; }

        public virtual Pacientes Pacientes { get; set; }
    }
}
