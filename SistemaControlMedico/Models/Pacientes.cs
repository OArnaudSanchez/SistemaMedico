namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pacientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pacientes()
        {
            Citas = new HashSet<Citas>();
            Ingresos = new HashSet<Ingresos>();
        }

        [Key]
        public int idPaciente { get; set; }

        [Required(ErrorMessage ="El campo {0} No puede estar Vacio"), StringLength(13),Display(Name ="Cedula del Paciente"),RegularExpression(@"\d{3}-\d{7}-\d{1}",ErrorMessage ="El campo {0} Debe tener el formato 000-0000000-0")]
        public string cedula { get; set; }

        [Required(ErrorMessage ="El campo {0} No puede estar Vacio"),StringLength(50),Display(Name ="Nombre"), DataType(DataType.Text, ErrorMessage ="Solo se permiten Letras")]
        public string nombre { get; set; }

        [Required, Display(Name ="Paciente Asegurado")]
        public bool? asegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Citas> Citas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingresos> Ingresos { get; set; }
    }
}
