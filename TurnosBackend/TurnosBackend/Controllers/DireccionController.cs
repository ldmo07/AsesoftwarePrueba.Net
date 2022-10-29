using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnosBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private TurnosAsesoftwareContext _context;
        public DireccionController(TurnosAsesoftwareContext context)
        {
            _context = context;
        }

        #region GET
        [HttpGet]
        public async Task<JsonResult> Get(int id_direccion = 0)
        {
            try
            {
                if (id_direccion == 0)
                {
                    var result = _context.Direcciones.ToList();

                    return new JsonResult(result)
                    {
                        StatusCode = StatusCodes.Status201Created  
                    };

                }
                else
                {
                    var result = _context.Direcciones.Where(x => x.id_direccion.Equals(id_direccion));

                    return new JsonResult(result)
                    {
                        StatusCode = StatusCodes.Status201Created 
                    };
                }
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
        public async Task<JsonResult> Post(Direccion  direccion)
        {
            try
            {
                _context.Direcciones.Add(direccion);
                await _context.SaveChangesAsync();
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
        public async Task<JsonResult> Delete(int id_direccion)
        {
            try
            {
                var direccion_eliminar = _context.Direcciones.SingleOrDefault(x => x.id_direccion == id_direccion);

                if (direccion_eliminar != null)
                {
                    _context.Direcciones.Remove(direccion_eliminar);
                    await _context.SaveChangesAsync();
                }

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
        public async Task<JsonResult> Put(Direccion direccion)
        {
            try
            {
                _context.Direcciones.Update(direccion);
                await _context.SaveChangesAsync();
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
