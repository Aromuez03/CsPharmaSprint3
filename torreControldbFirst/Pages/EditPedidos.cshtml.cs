using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using torreControldbFristDAL.Modelo;

namespace torreControldbFirst.Pages
{
    public class EditPedidosModel : PageModel
    {
        // Se crea una instancia de la base de datos para interactuar con ella.
        private readonly CspharmaInformacionalContext contextdb;

        public EditPedidosModel(CspharmaInformacionalContext context)
        {
            contextdb = context;
        }

        // La propiedad BindProperty se utiliza para enlazar automáticamente los valores de las propiedades del modelo con la página.
        [BindProperty]
        public TdcTchEstadoPedido editador { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(long? id)
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

            if (id == null || contextdb.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdctchestadopedido = await contextdb.TdcTchEstadoPedidos.FirstOrDefaultAsync(m => m.Id == id);
            if (tdctchestadopedido == null)
            {
                return NotFound();
            }
            editador = tdctchestadopedido;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var pedido = await contextdb.TdcTchEstadoPedidos.FindAsync(editador.Id);

                if (pedido == null)
                {
                    return NotFound();
                }

                pedido.CodEstadoEnvio = editador.CodEstadoEnvio;
                pedido.CodEstadoPago = editador.CodEstadoPago;
                pedido.CodEstadoDevolucion = editador.CodEstadoDevolucion;
                pedido.CodPedido = editador.CodPedido;
                pedido.CodLinea = editador.CodLinea;
                pedido.MdUuid = editador.MdUuid;
                pedido.MdDate = editador.MdDate;

                contextdb.Attach(pedido).State = EntityState.Modified;

                try
                {
                    await contextdb.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcTchEstadoPedidoExists(editador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Page();
            }


            return RedirectToPage("/Pedidos");
        }

        private bool TdcTchEstadoPedidoExists(int id)
        {
            return contextdb.TdcTchEstadoPedidos.Any(e => e.Id == id);
        }
    }
}
