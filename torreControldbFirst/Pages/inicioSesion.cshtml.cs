using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using torreControldbFirst.dtos;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages.modelo
{
    public class inicioSesionModel : PageModel
    {
        CspharmaInformacionalContext contextodb = new CspharmaInformacionalContext();
        public IActionResult OnGet()
        {
            // Guardamos el rol en la variable nivelAcceso
            var nivelAcceso = HttpContext.Session.GetString("nivelAcceso");

            // Si el nivel de acceso est� vac�o, redirige al inicio de sesi�n
            if (!string.IsNullOrEmpty(nivelAcceso))
            {
                return RedirectToPage("/Privacy");
            }
            return Page();
        }
        private readonly CspharmaInformacionalContext contexto;
        public inicioSesionModel(CspharmaInformacionalContext contextodb)
        {
            contexto = contextodb;
        }


        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> OnPostAsync(string CodEmpleado, string ClaveEmpleado)
        {
            try
            {
                encriptado ecr = new encriptado();
                //Encriptamos la clave para comparar con base de datos
                ClaveEmpleado = ecr.EncriptarContra(ClaveEmpleado);

                // Obtener el empleado de la base de datos
                var empleado = await contexto.DlkCatAccEmpleados.FirstOrDefaultAsync(e => e.CodEmpleado == CodEmpleado && e.ClaveEmpleado == ClaveEmpleado);

                if (empleado != null)
                {
                    // Almacenar el c�digo y nivel de acceso del empleado en la sesi�n
                    HttpContext.Session.SetString("codigoEmpleado", empleado.CodEmpleado);
                    HttpContext.Session.SetString("nivelAcceso", empleado.NivelAccEmpleado.ToString());

                    // Redireccionar a la p�gina de inicio
                    return RedirectToPage("/Privacy");
                }

                // Si el empleado no existe, mostrar un mensaje de error
                ModelState.AddModelError(string.Empty, "Usuario o contrase�a incorrectos.");
                return Page();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR]: Ha ocurrido un error: " + e);
                return Page();
            }
        }
    


    }
}
