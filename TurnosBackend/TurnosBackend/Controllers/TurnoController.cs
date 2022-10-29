using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnosBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        private readonly AsesoftwareNegocio negocio;
        private readonly ILogger<TurnoController> _logger;
        #endregion

        #region CONSTRUCTOR
        public TurnoController(ILogger<TurnoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            negocio = new AsesoftwareNegocio(_configuration);
        }
        #endregion

        #region GET
        [HttpGet]
        public async Task<JsonResult> Get(int id_turno = 0)
        {
            try
            {
                var result = new { OK = true, Data = await negocio.lista_turnos(id_turno) };

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
        public async Task<JsonResult> Post(int id_servicio, DateTime fecha_inicio, DateTime fecha_fin)
        {
            try
            {
                List<Turno> lstTurnos = await negocio.generar_turnos(id_servicio, fecha_inicio, fecha_fin);
                var result = new { OK = true, msg = "Insercion exitosa", Data = lstTurnos };

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
        public async Task<JsonResult> Delete(string id_turno)
        {
            try
            {
                await negocio.eliminar_turno(id_turno);
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
        public async Task<JsonResult> Put(Turno turno)
        {
            try
            {
                await negocio.update_turno(turno);
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
