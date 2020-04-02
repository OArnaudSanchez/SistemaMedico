namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Habitaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Habitaciones()
        {
            Ingresos = new HashSet<Ingresos>();
        }

        [Key]
        public int idHabitacion { get; set; }

        [Required(ErrorMessage ="Debe ingresar el Numero de Habitacion"),Display(Name ="Numero de Habitacion")]
        public int numero { get; set; }

        [StringLength(25), Display(Name ="Tipo de Habitacion")]
        public string tipo { get; set; }

        [Required(ErrorMessage ="Debe ingresar el Precio Por Dia"), Display(Name ="Precio Por Dia")]
        public int precioDia { get; set; }


        //[Required,Display(Name ="Fecha de Ingreso"),DataType(DataType.DateTime)]
        //public DateTime fechaIngreso { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingresos> Ingresos { get; set; }
    }

    public enum PrecioHabitacion
    {
        Doble = 5000, Privada = 1000 , Suite = 15000
    }
}
