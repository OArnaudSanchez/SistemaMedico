namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicos()
        {
            Citas = new HashSet<Citas>();
            Ingresos = new HashSet<Ingresos>();
        }

        [Key]
        public int idMedico { get; set; }

        [Required, Display(Name = "Nombre del Medico"), StringLength(50)]
        public string nombre { get; set; }

        [Required,Display(Name = "Exequatur"), StringLength(200)]
        public string exequatur { get; set; }

        [Required, Display(Name = "Especialidad"), StringLength(50)]
        public string especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Citas> Citas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingresos> Ingresos { get; set; }

    }
}
