using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.dtos
{
    public class toDTO
    {
        public empleadoDTO empToDTO(DlkCatAccEmpleado empleado)
        {

            empleadoDTO empleado1 = new empleadoDTO();
                empleado1.CodEmpleado = empleado.CodEmpleado;
                empleado1.NivelAccEmpleado = empleado.NivelAccEmpleado;
                empleado1.ClaveEmpleado = empleado.ClaveEmpleado;
            return empleado1;
        }
    }
}
