using IADatabase.Data;
using IADatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IADatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IaConsultaController : ControllerBase
    {

        private readonly AppDbContext _context;

        public IaConsultaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<String>> ConsultarDatos(String consulta)
        {
            Utils.IA iaConsutal = new Utils.IA();

            var respuesta = await iaConsutal.GeneraConsultaAsync(consulta, _context);


            return respuesta;
        }
    }
}
