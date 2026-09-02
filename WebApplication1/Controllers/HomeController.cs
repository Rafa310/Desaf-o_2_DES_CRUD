using Microsoft.AspNetCore.Mvc;
//Esta es TOTALMENTE PARA SU USO, nomás la cree asi chiquita para ver que se viera algo en la web sin más, pueden modificar o borrar.
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                mensaje = "API de Eventos funcionando correctamente",
                base_datos = "bd_des104_d2",
                estado = "Base de datos creada exitosamente"
            });
        }
    }
}