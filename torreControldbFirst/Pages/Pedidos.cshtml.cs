using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class PedidosModel : PageModel
    {
        private readonly CspharmaInformacionalContext contextdb;

        public PedidosModel(CspharmaInformacionalContext context)
        {
            contextdb = context;
        }
        public IList<TdcTchEstadoPedido> Pedidos { get; set; }

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
            Pedidos = await contextdb.TdcTchEstadoPedidos.ToListAsync();
            return Page();

        }
    }
}
