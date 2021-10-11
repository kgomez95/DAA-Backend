using Microsoft.AspNetCore.Mvc;

namespace DAA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataTablesController : ControllerBase
    {
        public DataTablesController()
        {

        }

        [HttpGet("DataHeader")]
        public IActionResult DataHeader(string datatable)
        {
            return Ok(string.Format("Buscas las cabeceras de la tabla '{0}'.", datatable));
        }
    }
}
