using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using torreControldbFirst.dtos;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class EliminarEmpModel : PageModel
    {
        private readonly CspharmaInformacionalContext contextdb;

        public EliminarEmpModel(CspharmaInformacionalContext context)
        {
            contextdb = context;
        }

        [BindProperty]
        public DlkCatAccEmpleado Empleado { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string? codEmpleado)
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
            if (codEmpleado == null)
            {
                return NotFound();
            }

            Empleado = contextdb.DlkCatAccEmpleados.FirstOrDefault(m => m.CodEmpleado == codEmpleado);

            contextdb.DlkCatAccEmpleados.Remove(Empleado);
            contextdb.SaveChanges();

            return RedirectToPage("/listaEmp");

           
        }
    }
}
