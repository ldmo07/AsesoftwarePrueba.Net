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
    public class ClienteController : ControllerBase
    {
        private TurnosAsesoftwareContext _context;
        public ClienteController(TurnosAsesoftwareContext context)
        {
            _context = context;
        }

        #region GET
        [HttpGet]
        public async Task<JsonResult> Get(int id_cliente = 0)
        {
            try
            {
                if (id_cliente == 0)
                {
                    var result = _context.Clientes.ToList();

                    return new JsonResult(result)
                    {
                        StatusCode = StatusCodes.Status201Created 
                    };

                }
                else
                {
                    var result = _context.Clientes.Where(x => x.id_cliente.Equals(id_cliente));

                    return new JsonResult(result)
                    {
                        StatusCode = StatusCodes.Status201Created
                    };
                }
            }
            catch (Exception e)
            {
                var result = new { OK = true, msg = "Ha ocurrido un fallo => "+e.Message };
                return new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            

        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<JsonResult> Post(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
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
        public async Task<JsonResult> Delete(int id_cliente)
        {
            try
            {
                var cliente_eliminar = _context.Clientes.SingleOrDefault(x => x.id_cliente == id_cliente);

                if (cliente_eliminar != null)
                {
                    _context.Clientes.Remove(cliente_eliminar);
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
        public async Task<JsonResult> Put(Cliente cliente)
        {
            try
            {
                _context.Clientes.Update(cliente);
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
