using DAA.Constants.Api;

namespace DAA.Utils.Conversions
{
    public static class ConstantsConversion
    {
        /// <summary>
        /// Convierte el tipo de campo proporcionado a su tipo correspondiente en formato cadena.
        /// </summary>
        /// <param name="type">Identificador del tipo.</param>
        /// <returns>Retorna el tipo en formato cadena.</returns>
        public static string ConvertType(int type)
        {
            switch (type)
            {
                case RecordTypesValues.STRING:
                    return "string";
                case RecordTypesValues.INTEGER:
                    return "integer";
                case RecordTypesValues.DECIMAL:
                    return "decimal";
                case RecordTypesValues.CURRENCY:
                    return "currency";
                case RecordTypesValues.PERCENTATGE:
                    return "percentatge";
                case RecordTypesValues.DATETIME:
                    return "datetime";
                default:
                    return "string";
            }
        }
    }
}
