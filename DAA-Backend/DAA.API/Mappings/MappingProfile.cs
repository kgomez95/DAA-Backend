using AutoMapper;

namespace DAA.API.Mappings
{
    public class MappingProfile : Profile
    {
        #region Constructores.
        public MappingProfile()
        {
            this.BaseAuditableMapping();
            this.DataTablesMapping();
            this.PlatformsMapping();
            this.VideoGamesMapping();
        }
        #endregion

        #region Métodos y funciones privadas.
        /// <summary>
        /// Crea el mapeo del modelo BaseAuditable.
        /// </summary>
        private void BaseAuditableMapping()
        {
            base.CreateMap<Database.Models.BaseAuditable, Database.ModelsDTO.BaseAuditableDTO>();
            base.CreateMap<Database.ModelsDTO.BaseAuditableDTO, Database.Models.BaseAuditable>();
        }

        /// <summary>
        /// Crea el mapeo de los modelos DataTables.
        /// </summary>
        private void DataTablesMapping()
        {
            base.CreateMap<Database.Models.DataTables.DatatablesTable, Database.ModelsDTO.DataTables.DatatablesTableDTO>();
            base.CreateMap<Database.ModelsDTO.DataTables.DatatablesTableDTO, Database.Models.DataTables.DatatablesTable>();

            base.CreateMap<Database.Models.DataTables.DatatablesRecord, Database.ModelsDTO.DataTables.DatatablesRecordDTO>();
            base.CreateMap<Database.ModelsDTO.DataTables.DatatablesRecordDTO, Database.Models.DataTables.DatatablesRecord>();
        }

        /// <summary>
        /// Crea el mapeo de modelos Platforms.
        /// </summary>
        private void PlatformsMapping()
        {
            base.CreateMap<Database.Models.Platforms.Platform, Database.ModelsDTO.Platforms.PlatformDTO>();
            base.CreateMap<Database.ModelsDTO.Platforms.PlatformDTO, Database.Models.Platforms.Platform>();
        }

        /// <summary>
        /// Crea el mapeo de modelos VideoGames.
        /// </summary>
        private void VideoGamesMapping()
        {
            base.CreateMap<Database.Models.VideoGames.VideoGame, Database.ModelsDTO.VideoGames.VideoGameDTO>();
            base.CreateMap<Database.ModelsDTO.VideoGames.VideoGameDTO, Database.Models.VideoGames.VideoGame>();
        }
        #endregion
    }
}
