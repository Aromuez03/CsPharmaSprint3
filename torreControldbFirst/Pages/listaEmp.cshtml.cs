using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using torreControldbFirst.dtos;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class listaEmpModel : PageModel
    {
        private readonly CspharmaInformacionalContext contexto;

        public listaEmpModel(CspharmaInformacionalContext context)
        {
            contexto = context;
            Empleados = new List<empleadoDTO>();
        }

        public List<empleadoDTO> Empleados { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Guardamos el rol en la variable nivelAcceso
            var nivelAcceso = HttpContext.Session.GetString("nivelAcceso");

            // Si el nivel de acceso está vacío, redirige al inicio de sesión
            if (string.IsNullOrEmpty(nivelAcceso))
            {
                return RedirectToPage("/inicioSesion");
            }
            // Si el nivel de acceso es diferente de 0, redirige a SinAcceso
            else if (nivelAcceso != "0")
            {
                Console.WriteLine("[INFORMACION]: El usuario no tiene acceso");
                return RedirectToPage("/SinAcceso");
            }

            // Transforma cada objeto empleado del modelo a un objeto empleadoDTO utilizando el método empToDTO de la clase toDTO y los agrega a la lista Empleados
            var toDto = new toDTO();
            Empleados = new List<empleadoDTO>();
            Empleados = await contexto.DlkCatAccEmpleados
                .Select(e => toDto.empToDTO(e))
                .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(empleadoDTO empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Busca el empleado en la base de datos
                    var empleadoActual = await contexto.DlkCatAccEmpleados
                        .FirstOrDefaultAsync(e => e.CodEmpleado == empleado.CodEmpleado);

                    // Si el empleado existe, actualiza su clave y nivel de acceso y guarda los cambios en la base de datos
                    if (empleadoActual != null)
                    {
                        empleadoActual.ClaveEmpleado = empleado.ClaveEmpleado;
                        empleadoActual.NivelAccEmpleado = empleado.NivelAccEmpleado;
                        await contexto.SaveChangesAsync();
                        return RedirectToPage("./listaEmp");
                    }
                }
                return Page();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Si el empleado que se intenta actualizar ya no existe en la base de datos
                // por ejemplo, si otro usuario lo eliminó mientras se estaba editando
                // retornamos NotFound()
                return NotFound();
            }

        }

        //Se intentó
        public async Task<IActionResult> EliminarEmp(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var empleado = await contexto.DlkCatAccEmpleados
                        .FirstOrDefaultAsync(e => e.CodEmpleado == id);
                if(empleado != null)
                { 
                    contexto.DlkCatAccEmpleados.Remove(empleado);
                    await contexto.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("[INFORMACION]: No se pudo eliminar el usuario");
                }
                

            }

            return RedirectToPage("./listaEmp");
        }
    }
}