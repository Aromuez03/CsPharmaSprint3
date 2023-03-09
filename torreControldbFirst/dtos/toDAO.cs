using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.dtos
{
    public class toDAO
    {
        public DlkCatAccEmpleado empToDAO(empleadoDTO empleado)
        {
            Guid uuid = Guid.NewGuid();
            DlkCatAccEmpleado empleado1 = new DlkCatAccEmpleado();
            empleado1.MdUuid = uuid.ToString();
            empleado1.MdDate = DateTime.Now;
            empleado1.CodEmpleado = empleado.CodEmpleado;
            empleado1.NivelAccEmpleado = empleado.NivelAccEmpleado;
            empleado1.ClaveEmpleado = empleado.ClaveEmpleado;
            return empleado1;
        }
    }
}
