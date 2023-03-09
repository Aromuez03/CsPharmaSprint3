using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class DeletePedidosModel : PageModel
    {
        private readonly CspharmaInformacionalContext contextdb;

        public DeletePedidosModel(CspharmaInformacionalContext context)
        {
            contextdb = context;
        }

        [BindProperty]
        public TdcTchEstadoPedido Pedido { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null)
            {
                return NotFound();
            }

            Pedido = contextdb.TdcTchEstadoPedidos.FirstOrDefault(m => m.Id == id);

            if (Pedido == null)
            {
                return NotFound();
            }

            contextdb.TdcTchEstadoPedidos.Remove(Pedido);
            contextdb.SaveChanges();

            return RedirectToPage("/Pedidos");

        }

    }
}
