using DAA.API.Models.Datatables;
using DAA.Database.ServicesDTO.Interfaces.Datatables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace DAA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataTablesController : ControllerBase
    {
        #region Atributos privados.
        private readonly IDatatablesRecordsServiceDTO _datatablesRecordsServiceDTO;
        #endregion

        #region Constructores.
        public DataTablesController(IDatatablesRecordsServiceDTO datatablesRecordsServiceDTO)
        {
            this._datatablesRecordsServiceDTO = datatablesRecordsServiceDTO;
        }
        #endregion

        #region Métodos y funciones públicas.
        [HttpGet("DataHeader")]
        public IActionResult DataHeader(string datatable)
        {
            try
            {
                return Ok(this._datatablesRecordsServiceDTO.GetDataHeaders(datatable));
            }
            catch (Exception ex)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("DataFilter")]
        public IActionResult DataFilter(string datatable)
        {
            try
            {
                return Ok(this._datatablesRecordsServiceDTO.GetDataFilters(datatable));
            }
            catch (Exception ex)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("DataView")]
        public IActionResult DataView(string datatable)
        {
            // TODO: Realizar correctamente esta función, porque ahora mismo solamente es para probar que funcione.

            try
            {
                // TODO: BORRAR PRUEBA ///////////////////////////////////////////
                DataSort dataSort = new DataSort();
                dataSort.Field = "VideoGameName";
                dataSort.Desc = true;
                DataFilter dataFilter = this._datatablesRecordsServiceDTO.GetDataFilters(datatable);
                return Ok(this._datatablesRecordsServiceDTO.GetDataView(datatable, dataFilter, 0, 10, dataSort));
                //////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetRecords")]
        public IActionResult GetRecords(string datatable)
        {
            try
            {
                return Ok(this._datatablesRecordsServiceDTO.GetRecords(datatable));
            }
            catch (Exception ex)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
