using Data;
using Microsoft.Extensions.Configuration;
using Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio
{
    public class AsesoftwareNegocio
    {
        private readonly IConfiguration _configuration;
        public AsesoftwareNegocio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region COMERCIOS
        public async Task<List<Comercio>> lista_comercios(int id_comercio = 0)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                return await data.lista_comercios(id_comercio);
            }
        }
        public async Task insertar_comercio(Comercio comercio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.insertar_comercio(comercio);
            }
        }

        public async Task update_comercio(Comercio comercio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.update_comercio(comercio);
            }
        }

        public async Task eliminar_comercio(string id_comercio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.eliminar_comercio(id_comercio);
            }
        }

        #endregion

        #region SERVICIO
        public async Task<List<Servicio>> lista_Servicio(int id_servicio = 0)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                return await data.lista_Servicio(id_servicio);
            }
        }
        public async Task insertar_servicio(Servicio servicio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.insertar_servicio(servicio);
            }
        }

        public async Task update_servicio(Servicio servicio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.update_servicio(servicio);
            }
        }

        public async Task eliminar_Servicio(string id_servicio)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.eliminar_Servicio(id_servicio);
            }
        }

        #endregion

        #region TURNOS
        public async Task<List<Turno>> lista_turnos(int id_turno = 0)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                return await data.lista_turnos(id_turno);
            }
        }
        public async Task update_turno(Turno turno)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.update_turno(turno);
            }
        }

        public async Task eliminar_turno(string id_turno)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                await data.eliminar_turno(id_turno);
            }
        }

        public async Task<List<Turno>> generar_turnos(int id_servicio,DateTime fecha_inicio,DateTime fecha_fin)
        {
            using (AsesoftwareData data = new AsesoftwareData(_configuration))
            {
                return await data.generar_turnos(id_servicio, fecha_inicio, fecha_fin);
            }
        }

        #endregion
    }
}

