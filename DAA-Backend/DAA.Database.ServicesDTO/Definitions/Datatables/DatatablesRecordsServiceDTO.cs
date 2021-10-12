using AutoMapper;
using DAA.Database.ModelsDTO.DataTables;
using DAA.Database.Services.Interfaces.Datatables;
using DAA.Database.ServicesDTO.Interfaces.Datatables;
using System;

namespace DAA.Database.ServicesDTO.Definitions.Datatables
{
    public class DatatablesRecordsServiceDTO : IDatatablesRecordsServiceDTO
    {
        #region Atributos privados.
        private readonly IDatatablesRecordsService _datatablesRecordsService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructores.
        public DatatablesRecordsServiceDTO(IDatatablesRecordsService datatablesRecordsService, IMapper mapper)
        {
            this._datatablesRecordsService = datatablesRecordsService;
            this._mapper = mapper;
        }
        #endregion

        #region Métodos y funciones públicas.
        /// <summary>
        /// Coge las cabeceras de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger las cabeceras.</param>
        /// <returns>Retorna un listado con las cabeceras.</returns>
        public string[] GetDataHeaders(string datatable)
        {
            try
            {
                return this._datatablesRecordsService.GetDataHeaders(datatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Coge todos los registros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los registros.</param>
        /// <returns>Retorna un listado de registros DataTables.</returns>
        public DatatablesRecordDTO[] GetRecords(string datatable)
        {
            try
            {
                return this._mapper.Map<DatatablesRecordDTO[]>(this._datatablesRecordsService.GetRecords(datatable));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
