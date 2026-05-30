using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentacionesAspnetcoreBarberia.Models;
using PresentacionesAspnetcoreBarberia.Services;

namespace PresentacionesAspnetcoreBarberia.Pages.Historicos
{
    public class IndexModel : PageModel
    {
        private readonly HistoricosService _historicosService;

        public List<HistoricosClase> ListaHistoricos { get; set; } = new();

        public IndexModel(HistoricosService historicosService)
        {
            _historicosService = historicosService;
        }

        public async Task OnGetAsync()
        {
            // Traemos los registros de la bitácora
            ListaHistoricos = await _historicosService.ConsultarAsync();
        }
    }
}