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

        //[NotMapped]
        //public string nombre { get; set; }

        [Required(ErrorMessage ="Debe seleccionar el Paciente para Ingresarlo")]
        public int paciente { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la Habitacion para Ingresar")]
        public int habitacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Medico responsable del Ingreso")]
        public int medico { get; set; }

        [Column(TypeName = "date"), DataType(DataType.Date),Display(Name ="Fecha de Ingreso"),Required(ErrorMessage ="El campo {0} No puede estar Vacio")]
        public DateTime fechaIngreso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Altas> Altas { get; set; }

        public virtual Habitaciones Habitaciones { get; set; }

        public virtual Medicos Medicos { get; set; }

        public virtual Pacientes Pacientes { get; set; }
    }
}
