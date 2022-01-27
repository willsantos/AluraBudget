using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AluraBudget.Helpers
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OutgoingCategory
    {
        Outras, 
        Alimentação,
        Saúde,
        Moradia,
        Transporte,
        Educação,
        Lazer,
        Imprevistos,
    }
}
