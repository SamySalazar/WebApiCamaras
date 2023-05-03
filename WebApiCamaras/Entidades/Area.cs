using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiCamaras.Validaciones;

namespace WebApiCamaras.Entidades
{
    public class Area : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} non debe tenenr más de {1} carácteres")]
        // [FirstCharCapital]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Coordenadas { get; set; }
        public string Dimensiones { get; set; }
        public string NivielRiesgo { get; set; }
        public List<Camara> Camaras { get; set; }

        //[Range(18, 120)]
        //[NotMapped]
        //public int MetrosCuadrados { get; set; }

        //[CreditCard]
        //[NotMapped]
        //public string TarjetaDeCredito { get; set; }

        //[Url]
        //[NotMapped]
        //public string Url { get; set; }

        //[NotMapped]
        //public int Menor { get; set; }
        //[NotMapped]
        //public int Mayor { get; set; }

        // Validación a nivel de modelo
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Para que se ejecuten debe de primero cumplirse con las reglas a nibel de Atributo Ejemplo: Range
            // Tomar a consideración que primero se ejecutaran las validaciones mappeadas en los atributos
            // y posteriormente las declaradas en la entidad
            if (!string.IsNullOrEmpty(Nombre))
            {
                var firstChar = Nombre[0].ToString();
                if (firstChar != firstChar.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new string[] { nameof(Nombre) });
                }
            }
            //if (Menor > Mayor)
            //{
            //    yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
            //        new string[] { nameof(Menor) });
            //}

        }
    }
}
