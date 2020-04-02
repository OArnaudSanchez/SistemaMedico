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

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true), Display(Name = "Fecha de Salida"), Required(ErrorMessage ="Debe seleccionar la fecha de salida")]
        public DateTime? fechaSalida { get; set; }

        [Display(Name = "Monto"), Required(ErrorMessage ="Para calcular el Monto debe seleccionar una fecha de salida")]
        public int monto { get; set; }

        [NotMapped]
        public int valor { get; set; }

        [NotMapped,Display(Name ="Fecha de Ingreso")]
        public DateTime fechaI { get; set; }

        [NotMapped, Display(Name ="Tipo de Habitacion")]
        public string tipoH { get; set; }

        [NotMapped, Display(Name ="Nombre Paciente")]
        public string nombreP { get; set; }

        [NotMapped, Display(Name = "Cedula Paciente")]
        public string cedulaP { get; set; }

        [NotMapped, Display(Name = "Ingresado Por")]
        public string nombreM { get; set; }

        public virtual Ingresos Ingresos { get; set; }

        public int CalcularMonto(int tipoHabitacion, int cantidadDias)
        {
            return tipoHabitacion * cantidadDias;
        }
                     
    }
}
