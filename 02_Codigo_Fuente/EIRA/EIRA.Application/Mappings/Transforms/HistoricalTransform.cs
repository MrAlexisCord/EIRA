using EIRA.Application.Extensions;
using EIRA.Application.Models.EntityModels;
using EIRA.Domain.Common;

namespace EIRA.Application.Mappings.Transforms
{
    public static class HistoricalTransform
    {
        public static object FromSourceObjectToHistoricalObject<T, TH>(T entity, Type type) where T : BaseEntity
        {
            var outerObject = Activator.CreateInstance(type);
            var response = entity.MapProperties<T, object>(outerObject, type);
            return response;
        }

        public static IEnumerable<object> FromSourceListToHistoricalList<T, TH>(IEnumerable<T> entity, Type type) where T : BaseEntity
        {
            var response = new List<object>();
            foreach (var item in entity)
            {
                var outerObject = Activator.CreateInstance(type);
                var objectOut = item.MapProperties<T, object>(outerObject, type);
                response.Add(objectOut);
            }
            return response;
        }

        public static object FromSourceObjectToHistoricalObjectUpdate<T, TH>(T oldEntity, T entity, Type type, EntityColumnName[] columnInfo) where T : BaseEntity
        {
            var outerObject = Activator.CreateInstance(type);
            var response = oldEntity.MapPropertiesUpdate<T, object>(entity, outerObject, type, columnInfo);
            return response;
        }

        public static IEnumerable<object> FromSourceListToHistoricalListUpdate<T, TH>(IEnumerable<T> oldEntity, IEnumerable<T> entity, Type type, EntityColumnName[] columnInfo) where T : BaseEntity
        {
            var pairZipEntities = oldEntity.Zip(entity, (o, u) => new { Old = o, Updated = u });
            var response = new List<object>();

            foreach (var tupla in pairZipEntities)
            {
                var outerObject = Activator.CreateInstance(type);
                if (!PropertiesExtension.HaveEqualValues(tupla.Old, tupla.Updated, columnInfo))
                {
                    var objectOut = (tupla.Old).MapPropertiesUpdate<T, object>(tupla.Updated, outerObject, type, columnInfo);
                    response.Add(objectOut);
                }
            }
            return response;
        }
    }
}
