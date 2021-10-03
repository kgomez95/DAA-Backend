using Newtonsoft.Json;

namespace DAA.Utils.Extensions
{
    public static class Json
    {
        /// <summary>
        /// Serializa el objeto a json.
        /// </summary>
        /// <param name="obj">Objeto a serializar.</param>
        /// <returns>Retorna el objeto serializado a una cadena json.</returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        /// <summary>
        /// Deserializa una cadena json para transformarla en el tipo de objeto especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a deserializar.</typeparam>
        /// <param name="obj">Cadena json a deserializar.</param>
        /// <returns>Retorna el objeto json deserializado.</returns>
        public static T FromJson<T>(this object obj)
        {
            return JsonConvert.DeserializeObject<T>(obj as string);
        }
    }
}
