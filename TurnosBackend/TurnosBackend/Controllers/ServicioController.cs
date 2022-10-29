using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modelos;
using Negocio;
using System;
using System.Threading.Tasks;

namespace TurnosBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        private readonly AsesoftwareNegocio negocio;
        private readonly ILogger<ComercioController> _logger;
        #endregion

        #region CONSTRUCTOR
        public ServicioController(ILogger<ComercioController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            negocio = new AsesoftwareNegocio(_configuration);
        }
        #endregion

        #region GET
        [HttpGet]
        public async Task<JsonResult> Get(int id_servicio = 0)
        {
            try
            {
                var result = new { OK = true, Data = await negocio.lista_Servicio(id_servicio) };

                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception e)
            {

                var result = new { OK = true, msg = "Ha ocurrido un fallo => " + e.Message };
                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<JsonResult> Post(Servicio servicio)
        {
            try
            {
                await negocio.insertar_servicio(servicio);
                var result = new { OK = true, msg = "Insercion exitosa" };

                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception e)
            {

                var result = new { OK = true, msg = "Ha ocurrido un fallo => " + e.Message };
                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<JsonResult> Delete(string id_servicio)
        {
            try
            {
                await negocio.eliminar_Servicio(id_servicio);
                var result = new { OK = true, msg = "Registro eliminado" };

                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status201Created 
                };
            }
            catch (Exception e)
            {

                var result = new { OK = true, msg = "Ha ocurrido un fallo => " + e.Message };
                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<JsonResult> Put(Servicio servicio)
        {
            try
            {
                await negocio.update_servicio(servicio);
                var result = new { OK = true, msg = "Registro Actualizado" };

                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception e)
            {

                var result = new { OK = true, msg = "Ha ocurrido un fallo => " + e.Message };
                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
           
        }
        #endregion
    }
}
