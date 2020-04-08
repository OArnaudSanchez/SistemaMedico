namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Citas
    {
        [Key,Display(Name ="ID Cita")]
        public int idCita { get; set; }

        public int paciente { get; set; }

        public int medico { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true), Display(Name ="Fecha"),DataType(DataType.DateTime)]
        public DateTime fecha { get; set; }

        public virtual Medicos Medicos { get; set; }

        public virtual Pacientes Pacientes { get; set; }
    }
}
