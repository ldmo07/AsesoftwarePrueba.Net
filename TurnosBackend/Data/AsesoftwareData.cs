using Dapper;
using Microsoft.Extensions.Configuration;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class AsesoftwareData : IDisposable
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        private readonly string _conexionString;
        #endregion

        #region CONSTRUCTOR
        public AsesoftwareData(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionString = _configuration.GetConnectionString("dbConexionString");
        }
        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #region COMERCIO
        public async Task<List<Comercio>> lista_comercios(int id_comercio = 0)
        {
            List<Comercio> lstComercios = new List<Comercio>();
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new { id_comercio = id_comercio };
                var lstDapper = await connection.QueryAsync<Comercio>("SP_LISTAR_COMERCIO", param, commandType: CommandType.StoredProcedure);
                lstComercios = lstDapper.ToList();
            }
            return lstComercios;
        }

        public async Task insertar_comercio(Comercio comercio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    nom_comercio = comercio.nom_comercio,
                    aforo_maximo = comercio.aforo_maximo
                };

                await connection.ExecuteAsync("SP_INSERTAR_COMERCIO", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task update_comercio(Comercio comercio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_comercio = comercio.id_comercio,
                    nom_comercio = comercio.nom_comercio,
                    aforo_maximo = comercio.aforo_maximo,
                };

                await connection.ExecuteAsync("SP_ACTUALIZAR_COMERCIO", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task eliminar_comercio(string id_comercio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_comercio = id_comercio
                };

                await connection.ExecuteAsync("SP_ELIMINAR_COMERCIO", param, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region SERVICIOS
        public async Task<List<Servicio>> lista_Servicio(int id_servicio = 0)
        {
            List<Servicio> lstServicios = new List<Servicio>();
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new { id_servicio = id_servicio };
                var lstDapper = await connection.QueryAsync<Servicio>("SP_LISTAR_SERVICIO", param, commandType: CommandType.StoredProcedure);
                lstServicios = lstDapper.ToList();
            }
            return lstServicios;
        }

        public async Task insertar_servicio(Servicio servicio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_comercio = servicio.id_comercio,
                    nom_servicio = servicio.nom_servicio,
                    hora_apertura = servicio.hora_apertura,
                    hora_cierre = servicio.hora_cierre,
                    duracion = servicio.duracion
                };

                await connection.ExecuteAsync("SP_INSERTAR_SERVICIO", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task update_servicio(Servicio servicio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_servicio = servicio.id_servicio,
                    id_comercio = servicio.id_comercio,
                    nom_servicio = servicio.nom_servicio,
                    hora_apertura = servicio.hora_apertura,
                    hora_cierre = servicio.hora_cierre,
                    duracion = servicio.duracion
                };

                await connection.ExecuteAsync("SP_ACTUALIZAR_SERVICIO", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task eliminar_Servicio(string id_servicio)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_servicio = id_servicio
                };

                await connection.ExecuteAsync("SP_ELIMINAR_SERVICIO", param, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region TURNO
        public async Task<List<Turno>> lista_turnos(int id_turno = 0)
        {
            List<Turno> lstTurnos = new List<Turno>();
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new { id_turno = id_turno };
                var lstDapper = await connection.QueryAsync<Turno>("SP_LISTAR_TURNO", param, commandType: CommandType.StoredProcedure);
                lstTurnos = lstDapper.ToList();
            }
            return lstTurnos;
        }

        public async Task update_turno(Turno turno)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_turno = turno.id_turno,
                    id_servicio = turno.id_servicio,
                    fecha_turno = turno.fecha_turno,
                    hora_inicio = turno.hora_inicio,
                    hora_fin = turno.hora_fin,
                    estado = turno.estado
                };

                await connection.ExecuteAsync("SP_ACTUALIZAR_TUNO", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task eliminar_turno(string id_turno)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                var param = new
                {
                    id_turno = id_turno
                };

                await connection.ExecuteAsync("SP_ELIMINAR_TURNO", param, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<List<Turno>> generar_turnos(int id_servicio, DateTime fecha_inicio, DateTime fecha_fin)
        {
            using (SqlConnection connection = new SqlConnection(_conexionString))
            {
                /*var param = new
                {
                    id_servicio = id_servicio,
                    fecha_inicio = fecha_inicio,
                    fecha_fin = fecha_fin,
                };*/

                var param = new DynamicParameters();
                param.Add("id_servicio", id_servicio);
                param.Add("fecha_inicio", fecha_inicio);
                param.Add("fecha_fin", fecha_fin);
                param.Add("tabla_turnos", dbType: DbType.String, direction: ParameterDirection.Output,size: 214748367);
                await connection.ExecuteAsync("SP_GENERAR_TURNO", param, commandType: CommandType.StoredProcedure);
        
                var table_turnos = param.Get<string>("tabla_turnos");
                var listProductos = JsonConvert.DeserializeObject<List<Turno>>(table_turnos);
                return listProductos;
            }
        }

        #endregion
    }
}
