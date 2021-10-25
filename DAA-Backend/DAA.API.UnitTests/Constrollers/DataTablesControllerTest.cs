using DAA.API.Models.ApiRest;
using DAA.API.Models.Datatables;
using DAA.Utils.Extensions;
using NUnit.Framework;
using System;
using System.Net;

namespace DAA.API.UnitTests.Constrollers
{
    public class DataTablesControllerTest
    {
        private readonly string BaseUrl = "http://localhost:50000";

        [SetUp]
        public void Setup()
        {
        }

        #region Acción: DataHeader
        [Test]
        public void DataHeader_Scores_200()
        {
            string[] headers = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataHeader?datatable=DT_SCORE_GAMES", this.BaseUrl));
                headers = result.FromJson<string[]>();
            }
            catch (Exception ex)
            {
                headers = null;
            }

            Assert.IsNotNull(headers);
        }

        [Test]
        public void DataHeader_VideoGames_200()
        {
            string[] headers = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataHeader?datatable=DT_VIDEOGAMES", this.BaseUrl));
                headers = result.FromJson<string[]>();
            }
            catch (Exception ex)
            {
                headers = null;
            }

            Assert.IsNotNull(headers);
        }

        [Test]
        public void DataHeader_Platforms_200()
        {
            string[] headers = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataHeader?datatable=DT_PLATFORMS", this.BaseUrl));
                headers = result.FromJson<string[]>();
            }
            catch (Exception ex)
            {
                headers = null;
            }

            Assert.IsNotNull(headers);
        }

        [Test]
        public void DataHeader_Empty()
        {
            string[] headers = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataHeader?datatable=CualquierCosa", this.BaseUrl));
                headers = result.FromJson<string[]>();
            }
            catch (Exception ex)
            {
                headers = null;
            }

            Assert.IsEmpty(headers);
        }
        #endregion

        #region Acción DataFilter
        [Test]
        public void DataFilter_Scores_200()
        {
            DataFilter filters = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_SCORE_GAMES", this.BaseUrl));
                filters = result.FromJson<DataFilter>();
            }
            catch (Exception ex)
            {
                filters = null;
            }

            Assert.IsNotNull(filters);
            Assert.IsNotEmpty(filters.Basic);
            Assert.IsNotEmpty(filters.Advanced);
        }

        [Test]
        public void DataFilter_VideoGames_200()
        {
            DataFilter filters = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_VIDEOGAMES", this.BaseUrl));
                filters = result.FromJson<DataFilter>();
            }
            catch (Exception ex)
            {
                filters = null;
            }

            Assert.IsNotNull(filters);
            Assert.IsNotEmpty(filters.Basic);
            Assert.IsNotEmpty(filters.Advanced);
        }

        [Test]
        public void DataFilter_Platforms_200()
        {
            DataFilter filters = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_PLATFORMS", this.BaseUrl));
                filters = result.FromJson<DataFilter>();
            }
            catch (Exception ex)
            {
                filters = null;
            }

            Assert.IsNotNull(filters);
            Assert.IsNotEmpty(filters.Basic);
            Assert.IsNotEmpty(filters.Advanced);
        }

        [Test]
        public void DataFilter_Empty()
        {
            DataFilter filters = null;

            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=CualquierCosa", this.BaseUrl));
                filters = result.FromJson<DataFilter>();
            }
            catch (Exception ex)
            {
                filters = null;
            }

            Assert.IsNull(filters);
        }
        #endregion

        #region Acción: DataView
        [Test]
        public void DataView_Scores_200()
        {
            ApiResponse<DataRecord> dataView = null;

            try
            {
                // Recuperemos los filtros.
                WebClient clientFilter = new WebClient();
                string resultFilter = clientFilter.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_SCORE_GAMES", this.BaseUrl));
                DataFilter filters = resultFilter.FromJson<DataFilter>();

                // Creamos la petición.
                ApiRequest<DataFilter> request = new ApiRequest<DataFilter>()
                {
                    DataKey = "DT_SCORE_GAMES",
                    Data = filters,
                    Offset = 0,
                    Limit = 10,
                    Sort = new DataSort()
                    {
                        Field = "VideoGameName",
                        Asc = true,
                        Desc = false
                    }
                };

                // Recuperamos los datos del DataTable.
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                string result = client.UploadString(string.Format("{0}/api/datatables/dataView", this.BaseUrl), request.ToJson());
                dataView = result.FromJson<ApiResponse<DataRecord>>();
            }
            catch (Exception ex)
            {
                dataView = null;
            }

            Assert.IsNotNull(dataView);
            Assert.IsNotNull(dataView.Data);
            Assert.IsNotEmpty(dataView.Data.Records);
            Assert.GreaterOrEqual(dataView.TotalPages, 1);
            Assert.GreaterOrEqual(dataView.TotalRecords, 1);
        }

        [Test]
        public void DataView_VideoGames_200()
        {
            ApiResponse<DataRecord> dataView = null;

            try
            {
                // Recuperemos los filtros.
                WebClient clientFilter = new WebClient();
                string resultFilter = clientFilter.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_VIDEOGAMES", this.BaseUrl));
                DataFilter filters = resultFilter.FromJson<DataFilter>();

                // Creamos la petición.
                ApiRequest<DataFilter> request = new ApiRequest<DataFilter>()
                {
                    DataKey = "DT_VIDEOGAMES",
                    Data = filters,
                    Offset = 0,
                    Limit = 10,
                    Sort = new DataSort()
                    {
                        Field = "VideoGamePrice",
                        Asc = false,
                        Desc = true
                    }
                };

                // Indicamos que los filtros básicos tienen que buscar por "%el%".
                for (int i = 0; i < filters.Basic.Count; i++)
                {
                    filters.Basic[i].Value = "%el%";
                }

                // Recuperamos los datos del DataTable.
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                string result = client.UploadString(string.Format("{0}/api/datatables/dataView", this.BaseUrl), request.ToJson());
                dataView = result.FromJson<ApiResponse<DataRecord>>();
            }
            catch (Exception ex)
            {
                dataView = null;
            }

            Assert.IsNotNull(dataView);
            Assert.IsNotNull(dataView.Data);
            Assert.IsNotEmpty(dataView.Data.Records);
            Assert.GreaterOrEqual(dataView.TotalPages, 1);
            Assert.GreaterOrEqual(dataView.TotalRecords, 1);
        }

        [Test]
        public void DataView_Platforms_200()
        {
            ApiResponse<DataRecord> dataView = null;

            try
            {
                // Recuperemos los filtros.
                WebClient clientFilter = new WebClient();
                string resultFilter = clientFilter.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_PLATFORMS", this.BaseUrl));
                DataFilter filters = resultFilter.FromJson<DataFilter>();

                // Creamos la petición.
                ApiRequest<DataFilter> request = new ApiRequest<DataFilter>()
                {
                    DataKey = "DT_PLATFORMS",
                    Data = filters,
                    Offset = 0,
                    Limit = 10,
                    Sort = new DataSort()
                    {
                        Field = "Name",
                        Asc = false,
                        Desc = false
                    }
                };

                // Indicamos en el filtro avanzado "Price" que busque las plataformas que valgan desde 100€ hasta los 299,95€.
                for (int i = 0; i < filters.Advanced.Count; i++)
                {
                    if (filters.Advanced[i].Code.Equals("Price", StringComparison.InvariantCultureIgnoreCase))
                    {
                        filters.Advanced[i].From = "100";
                        filters.Advanced[i].To = "299.95";
                    }
                }

                // Recuperamos los datos del DataTable.
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                string result = client.UploadString(string.Format("{0}/api/datatables/dataView", this.BaseUrl), request.ToJson());
                dataView = result.FromJson<ApiResponse<DataRecord>>();
            }
            catch (Exception ex)
            {
                dataView = null;
            }

            Assert.IsNotNull(dataView);
            Assert.IsNotNull(dataView.Data);
            Assert.IsNotEmpty(dataView.Data.Records);
            Assert.AreEqual(dataView.TotalPages, 1);
            Assert.AreEqual(dataView.TotalRecords, 2);
        }

        [Test]
        public void DataView_Empty()
        {
            ApiResponse<DataRecord> dataView = null;

            try
            {
                // Recuperemos los filtros.
                WebClient clientFilter = new WebClient();
                string resultFilter = clientFilter.DownloadString(string.Format("{0}/api/datatables/dataFilter?datatable=DT_VIDEOGAMES", this.BaseUrl));
                DataFilter filters = resultFilter.FromJson<DataFilter>();

                // Creamos la petición.
                ApiRequest<DataFilter> request = new ApiRequest<DataFilter>()
                {
                    DataKey = "MeLoInvento",
                    Data = filters,
                    Offset = 0,
                    Limit = 10,
                    Sort = new DataSort()
                    {
                        Field = "VideoGamePrice",
                        Asc = false,
                        Desc = true
                    }
                };

                // Recuperamos los datos del DataTable.
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                string result = client.UploadString(string.Format("{0}/api/datatables/dataView", this.BaseUrl), request.ToJson());
                dataView = result.FromJson<ApiResponse<DataRecord>>();
            }
            catch (Exception ex)
            {
                dataView = null;
            }

            Assert.IsNull(dataView);
        }
        #endregion
    }
}
