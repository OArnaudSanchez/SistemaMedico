namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ingresos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingresos()
        {
            Altas = new HashSet<Altas>();
        }

        [Key]
        public int idIngreso { get; set; }

        public int paciente { get; set; }

        public int habitacion { get; set; }

        public int medico { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaIngreso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Altas> Altas { get; set; }

        public virtual Habitaciones Habitaciones { get; set; }

        public virtual Medicos Medicos { get; set; }

        public virtual Pacientes Pacientes { get; set; }
    }
}
