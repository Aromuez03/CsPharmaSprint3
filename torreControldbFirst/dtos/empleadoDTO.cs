using System.ComponentModel.DataAnnotations;

namespace torreControldbFirst.dtos
{
    public class empleadoDTO
    {
        /// <summary>
        /// codigo inequívoco que identifica al empleado
        /// </summary>
        public string CodEmpleado { get; set; } = null!;

        /// <summary>
        /// Entidad que identifica la contraseña del empleado
        /// </summary>
        public string ClaveEmpleado { get; set; } = null!;

        /// <summary>
        /// Entidad que identifica el grupo de acceso del empleado
        /// </summary>
        public int NivelAccEmpleado { get; set; }

        public empleadoDTO(string codEmpleado, string claveEmpleado, int nivelAccEmpleado)
        {
            CodEmpleado = codEmpleado;
            ClaveEmpleado = claveEmpleado;
            NivelAccEmpleado = nivelAccEmpleado;
        }
        public empleadoDTO()
        {
            
        }
    }
}
