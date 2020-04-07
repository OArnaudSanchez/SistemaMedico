namespace SistemaControlMedico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altas
    {
        [Key,Display(Name ="ID Alta")]
        public int idAlta { get; set; }

        [Display(Name ="Seleccione el Ingreso"), Required(ErrorMessage ="El campo {0} no puede estar vacio")]
        public int ingreso { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true), Display(Name = "Fecha de Salida"), Required(ErrorMessage ="Debe seleccionar la fecha de salida")]
        public DateTime fechaSalida { get; set; }

        [Display(Name = "Monto"), Required(ErrorMessage ="Para calcular el Monto debe seleccionar una fecha de salida")]
        public int monto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true),NotMapped,Display(Name ="Fecha de Ingreso")]
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

        public Altas()
        {
            
        }

        public int TrabajoFechas(string tipoH, DateTime inicio, DateTime fin)
        {
            TimeSpan ts = fin - inicio;
            int parametro = ts.Days;

            if (parametro > 0 && parametro <= 31)
            {
                //años
                if (tipoH == "DOBLE")
                {
                    return monto = parametro * 500;
                }
                else if (tipoH == "PRIVADA")
                {
                    return monto = parametro * 1000;
                }
                else
                {
                    return monto = parametro * 2000;
                }
                
            }

           else if (parametro > 31 && parametro < 365)
            {
                //Meses
                if (tipoH == "DOBLE")
                {
                    return monto = parametro * 500;
                }
                else if (tipoH == "PRIVADA")
                {
                    return monto = parametro * 1000;
                }
                else
                {
                    return monto = parametro * 2000;
                }
            }

           else if(parametro > 365)
            {
                //Años
                if (tipoH == "DOBLE")
                {
                    return monto = parametro * 500;
                }
                else if (tipoH == "PRIVADA")
                {
                    return monto = parametro * 1000;
                }
                else
                {
                    return monto = parametro * 2000;
                }
            }
            else
            {
                return 00;
            }
           
        }
    }
}
