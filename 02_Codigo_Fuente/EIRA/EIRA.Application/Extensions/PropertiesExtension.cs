using EIRA.Application.Models.EntityModels;
using EIRA.Application.Statics.Misc;
using EIRA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIRA.Application.Extensions
{
    public static class PropertiesExtension
    {
        public static string GetDisplayName<T>(this T model, string nameProperty) where T : class
        {
            var type = typeof(T);

            object[] attrs = type.GetProperty(nameProperty).GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs != null && attrs.Length > 0)
            {
                return ((DisplayAttribute)attrs[0]).Name;
            }

            return null;
        }

        //public static List<HeaderInfo> GetHeadersByDisplayName<T>() where T : class
        //{
        //    List<HeaderInfo> headers = new();
        //    var type = typeof(T);

        //    type.GetProperties().ToList().ForEach(x =>
        //    {
        //        var propertyName = x.Name;
        //        var isNullable = Nullable.GetUnderlyingType(x.PropertyType) != null;
        //        var typeOfProperty = !isNullable ? x.PropertyType.Name : x.PropertyType.GenericTypeArguments[0].Name;
        //        var attributes = x.GetCustomAttributes(typeof(DisplayAttribute), false);

        //        if (attributes != null && attributes.Length > 0)
        //        {
        //            var headerText = ((DisplayAttribute)attributes[0]).Name;
        //            headers.Add(new HeaderInfo { HeaderText = headerText, PropertyName = propertyName, PropertyType = typeOfProperty });
        //        }
        //    });

        //    return headers;
        //}

        //public static object[] GetPropertyValueByComparison<T>(this T model, string partNameProperty, TipoComparacion tipoComparacion) where T : class
        //{
        //    var type = typeof(T);
        //    object[] listProperties = null;
        //    listProperties = tipoComparacion switch
        //    {
        //        TipoComparacion.Equals => type.GetProperties()?.Where(x => x.Name.ToUpper().Equals(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        TipoComparacion.Contains => type.GetProperties()?.Where(x => x.Name.ToUpper().Contains(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        TipoComparacion.StartsWith => type.GetProperties()?.Where(x => x.Name.ToUpper().StartsWith(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        TipoComparacion.EndsWith => type.GetProperties()?.Where(x => x.Name.ToUpper().EndsWith(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        TipoComparacion.NotContains => type.GetProperties()?.Where(x => !x.Name.ToUpper().Contains(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        TipoComparacion.NotEquals => type.GetProperties()?.Where(x => !x.Name.ToUpper().Equals(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //        _ => type.GetProperties()?.Where(x => x.Name.ToUpper().Equals(partNameProperty.ToUpper()))?.Select(x => x.GetValue(model))?.ToArray(),
        //    };

        //    return listProperties;
        //}

        public static TH MapProperties<T, TH>(this T model, TH objectout, Type typeout) where T : BaseEntity
        {
            var typeIn = typeof(T);

            foreach (var propIn in typeIn.GetProperties()?.Where(x => !x.GetGetMethod().IsVirtual))
            {
                var valueIn = propIn.GetValue(model);
                var propName = propIn.Name;
                if (!(valueIn is null))
                    typeout.GetProperty(propName).SetValue(objectout, valueIn);
            }

            var fechaUltimaActualizacionIn = typeIn.GetProperty($"{nameof(BaseHisEntity.UpdatedAt)}").GetValue(model);
            var horaActual = DateTime.Now;
            typeout.GetProperty($"{nameof(BaseHisEntity.FechaVigenciaInicio)}").SetValue(objectout, fechaUltimaActualizacionIn);
            typeout.GetProperty($"{nameof(BaseHisEntity.FechaVigenciaFin)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.AccionAuditoriaId)}").SetValue(objectout, "DELETE");
            typeout.GetProperty($"{nameof(BaseHisEntity.CamposModificados)}").SetValue(objectout, null);
            typeout.GetProperty($"{nameof(BaseHisEntity.CreationAt)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.UpdatedAt)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.UserAt)}").SetValue(objectout, null);
            typeout.GetProperty($"{nameof(BaseHisEntity.UpdateUser)}").SetValue(objectout, null);

            return objectout;
        }

        public static TH MapPropertiesUpdate<T, TH>(this T oldModel, T model, TH objectout, Type typeout, EntityColumnName[] columnInfo) where T : BaseEntity
        {
            List<string> listaCamposModificados = new List<string>();
            string _camposModificados = null;
            columnInfo = columnInfo.ExclusionCamposAuditoria();

            var typeIn = typeof(T);

            foreach (var propIn in typeIn.GetProperties()?.Where(x => !x.GetGetMethod().IsVirtual))
            {
                var valueOld = propIn.GetValue(oldModel);
                var valueNew = propIn.GetValue(model);
                var propName = propIn.Name;

                if (columnInfo.Select(x => x.ClassPropertyName).Contains(propName) && ((valueNew ?? string.Empty).GetHashCode() != (valueOld ?? string.Empty).GetHashCode()))
                {
                    var changedName = $"{columnInfo.First(x => x.ClassPropertyName == propName).TablePropertyName}{Separator.COLON}{valueNew ?? "Sin Asignar"}";
                    listaCamposModificados.Add(changedName);
                }

                typeout.GetProperty(propName).SetValue(objectout, valueOld);
            }

            if (!(listaCamposModificados is null) && listaCamposModificados.Any())
            {
                _camposModificados = string.Join($"{Separator.PIPE}{Separator.PIPE}", listaCamposModificados);
            }

            var fechaUltimaActualizacionIn = typeIn.GetProperty($"{nameof(BaseHisEntity.UpdatedAt)}").GetValue(oldModel);
            var horaActual = DateTime.Now;
            typeout.GetProperty($"{nameof(BaseHisEntity.FechaVigenciaInicio)}").SetValue(objectout, fechaUltimaActualizacionIn);
            typeout.GetProperty($"{nameof(BaseHisEntity.FechaVigenciaFin)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.AccionAuditoriaId)}").SetValue(objectout, "UPDATE");
            typeout.GetProperty($"{nameof(BaseHisEntity.CamposModificados)}").SetValue(objectout, _camposModificados);
            typeout.GetProperty($"{nameof(BaseHisEntity.IsActive)}").SetValue(objectout, true);
            typeout.GetProperty($"{nameof(BaseHisEntity.CreationAt)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.UpdatedAt)}").SetValue(objectout, horaActual);
            typeout.GetProperty($"{nameof(BaseHisEntity.UserAt)}").SetValue(objectout, null);
            typeout.GetProperty($"{nameof(BaseHisEntity.UpdateUser)}").SetValue(objectout, null);

            return objectout;
        }

        public static EntityColumnName[] ExclusionCamposAuditoria(this EntityColumnName[] columnInfo)
        {
            string[] baseFields = new string[] { $"{nameof(BaseHisEntity.CreationAt)}"
                ,$"{nameof(BaseHisEntity.UserAt)}"
                ,$"{nameof(BaseHisEntity.UpdatedAt)}"
                ,$"{nameof(BaseHisEntity.UpdateUser)}"
                ,$"{nameof(BaseHisEntity.IsActive)}"
                };
            return columnInfo.Where(x => !baseFields.Contains(x.ClassPropertyName)).ToArray();
        }

        public static Type GetTypeParameterHistoricable<T>() where T : BaseEntity
        {
            return (from iType in typeof(T).GetInterfaces()
                    where iType.IsGenericType
                    && iType.GetGenericTypeDefinition() == typeof(IHistoricable<>)
                    select iType.GetGenericArguments()[0])?.FirstOrDefault();
        }

        public static bool HaveEqualValues<T>(T oldEntity, T newEntity, EntityColumnName[] columnInfo) where T : BaseEntity
        {
            var columnsInfo = columnInfo.ExclusionCamposAuditoria().Select(x => x.ClassPropertyName);
            var allEquals = typeof(T)?.GetProperties()?.Where(x => columnsInfo.Contains(x.Name))?
                .All(x => (x.GetValue(oldEntity) ?? string.Empty).GetHashCode() == (x.GetValue(newEntity) ?? string.Empty).GetHashCode());
            return allEquals.HasValue && allEquals.Value;
        }

        //public static string GetReporteHeader<T>(string propertyName)
        //{
        //    var columnName = typeof(T)?.GetMember(propertyName)?.FirstOrDefault()?.GetCustomAttributesData()?
        //                    .Where(x => x?.AttributeType?.Name == nameof(ReporteNombreEncabezadoAttribute))?.FirstOrDefault()?
        //                    .ConstructorArguments?[0].Value?.ToString() ??
        //                    (typeof(T)?.GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayAttribute), false)?.FirstOrDefault() as DisplayAttribute)?.Name ??
        //                    string.Empty;
        //    return columnName;
        //}

        //public static string GetColumnNameByPropName<T>(string propertyName)
        //{
        //    var columnName = GetReporteHeader<T>(propertyName);
        //    if (string.IsNullOrEmpty(columnName))
        //        return propertyName;
        //    return columnName;
        //}
    }
}
