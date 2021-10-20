using DAA.API.Models.ApiRest;
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

        [HttpPost("DataView")]
        public IActionResult DataView([FromBody] ApiRequest<DataFilter> apiRequest)
        {
            try
            {
                return Ok(this._datatablesRecordsServiceDTO.GetDataView(apiRequest.DataKey, apiRequest.Data, apiRequest.Offset, apiRequest.Limit, apiRequest.Sort));
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
