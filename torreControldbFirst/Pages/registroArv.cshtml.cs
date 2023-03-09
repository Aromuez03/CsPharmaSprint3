using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using torreControldbFirst.dtos;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class registroArvModel : PageModel
    {
        private readonly CspharmaInformacionalContext contexto;

        public registroArvModel(CspharmaInformacionalContext context)
        {
            contexto = context;
        }

        public IActionResult OnGet()
        {
            //Guardamos el rol en la variable nivelAcceso
            var nivelAcceso = HttpContext.Session.GetString("nivelAcceso");
            //Si esta vacio te manda a inicioSesion
            if (string.IsNullOrEmpty(nivelAcceso))
            {
                return RedirectToPage("/inicioSesion");
            }//Si es distinto de 0 no es admin por lo que lo envia a Privacy
            else if (nivelAcceso != "0")
            {
                Console.WriteLine("[INFORMACION]: El usuario no tiene acceso");
                return RedirectToPage("/SinAcceso");
            }
            return Page();
        }
        [HttpPost]
        [ActionName("RegistroArv")]
        public void OnPost(string codEmpleado, string claveEmpleado, int nivelAccEmpleado)
        {
            encriptado encr = new encriptado();
            // Creamos un nuevo objeto de tipo empleadoDTO
            var contra = claveEmpleado.Length;
            empleadoDTO nuevoEmpleado = new empleadoDTO(codEmpleado, encr.EncriptarContra(claveEmpleado), nivelAccEmpleado); ;
            
            if (contra <= 7)
            {

                // Insertamos la entidad en la base de datos
                try
                {
                    contexto.DlkCatAccEmpleados.Add(new toDAO().empToDAO(nuevoEmpleado));
                    contexto.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR]: Ha ocurrido un error: " +e);
                }
            }
            else
            {
                TempData["alerta"] = "La clave contiene mas caracteres de lo esperado, el maximo son 7 (No inspecciones tanto)";

            }

            
        }
    }
}
