using System;
using System.Collections.Generic;

namespace torreControldbFristDAL.Modelo;

public partial class DlkCatAccEmpleado
{
    /// <summary>
    /// Código de metadato
    /// que indica el grupo
    /// de inserción al que
    /// pertenece el registro
    /// </summary>
    public string MdUuid { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se
    /// define el grupo de
    /// inserción
    /// </summary>
    public DateTime MdDate { get; set; }

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

    public DlkCatAccEmpleado(string mdUuid, DateTime mdDate, string codEmpleado, string claveEmpleado, int nivelAccEmpleado)
    {
        MdUuid = mdUuid;
        MdDate = mdDate;
        CodEmpleado = codEmpleado;
        ClaveEmpleado = claveEmpleado;
        NivelAccEmpleado = nivelAccEmpleado;
    }

    public DlkCatAccEmpleado()
    {
    }
}
