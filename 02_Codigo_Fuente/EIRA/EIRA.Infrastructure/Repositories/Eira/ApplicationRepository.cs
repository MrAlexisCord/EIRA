using EIRA.Application.Attributes.Helpers;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.Enumerations.Errors;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.EntityModels;
using EIRA.Application.Specifications;
using EIRA.Domain.Common;
using EIRA.Infrastructure.DbContexts.SqlServerContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace EIRA.Infrastructure.Repositories.Eira
{
    public class ApplicationRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly EiraContext _dbContext;
        //protected readonly BALAPPDapperContext _dbContextDapper;

        private IQueryable<T> queriable;

        public ApplicationRepository(EiraContext dbContext
            //, BALAPPDapperContext dbContextDapper
            )
        {
            _dbContext = dbContext;
            //_dbContextDapper = dbContextDapper;
        }

        public virtual async Task<IReadOnlyList<T>> GetExecuteAsync(Enum @enum, ISpecification<T> spec, bool executeOnDapper = true)
        {
            try
            {
                //if (!executeOnDapper)
                //    return await GetExecuteOnEFAsync(@enum, spec);

                //return await GetExecuteOnDapperAsync(@enum, spec);
                return null;
            }
            catch (SqlException) { throw; }
            //catch (LogErrorException) { throw; }
            catch (ValidationException) { throw; }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Execute store procedures through EF Repository
        /// </summary>
        /// <param name="enum">Store procedure Enum</param>
        /// <param name="spec">Specification with store procedure parameters</param>
        /// <returns></returns>
        private async Task<IReadOnlyList<T>> GetExecuteOnEFAsync(Enum @enum, ISpecification<T> spec)
        {
            try
            {
                //var dato = await _dbContext.BalBasConfiguracionProcedimientoAlmacenado.Where(x => x.ProcedimientoAlmacenadoId.Equals(Conversion.GetMensaje(@enum))).FirstOrDefaultAsync();
                //return (await ApplySpecification(dato?.Rutina, spec))?.ToList();
                return null;
            }
            catch (SqlException e)
            {
                if (e.Number == (int)ExceptionSQLErrorsEnum.ExecutionTimeOutExpired)
                {
                    throw new ValidationException("El tiempo de espera soportado por la aplicación ha expirado, consulte con el administrador");
                }
                throw e;
            }
            catch (Exception e)
            {
                if (!spec.SqlParameters[0].SqlValue.ToString().Equals("0"))
                {
                    var logErrorExceptionSp = new ApiException(spec.SqlParameters[1].SqlValue.ToString(), e);

                    throw logErrorExceptionSp;
                }
                throw e;
            }
        }

        /// <summary>
        /// Execute store procedures through EF Repository
        /// </summary>
        /// <param name="enum">Store procedure Enum</param>
        /// <param name="spec">Specification with store procedure parameters</param>
        /// <returns></returns>
        //private async Task<IReadOnlyList<T>> GetExecuteOnDapperAsync(Enum @enum, ISpecification<T> spec)
        //{
        //    var parameters = new DynamicParameters();
        //    try
        //    {
        //        using var connection = _dbContextDapper.CreateConnection();
        //        DefaultTypeMap.MatchNamesWithUnderscores = true;
        //        var dato = await _dbContext.BalBasConfiguracionProcedimientoAlmacenado.Where(x => x.ProcedimientoAlmacenadoId.Equals(Conversion.GetMensaje(@enum))).FirstOrDefaultAsync();
        //        parameters = spec?.SqlParameters?.SqlParametersToDynamicParameters();

        //        return (IReadOnlyList<T>)await connection.QueryAsync<T>(sql: dato.ProcedimientoAlmacenadoId, param: parameters, commandTimeout: _dbContextDapper.GetTimeOutConnection(), commandType: CommandType.StoredProcedure);
        //    }
        //    catch (SqlException e)
        //    {
        //        if (e.Number == (int)ExceptionSQLErrorsEnum.ExecutionTimeOutExpired)
        //        {
        //            throw new ValidationException("El tiempo de espera soportado por la aplicación ha expirado, consulte con el administrador");
        //        }
        //        throw e;
        //    }
        //    catch (Exception e)
        //    {
        //        if ((parameters?.Get<long>("onu_Codigo_Respuesta") ?? 0) != 0)
        //        {
        //            spec.SqlParameters.FirstOrDefault(x => x.ParameterName == "onu_Codigo_Respuesta").Value = parameters.Get<long>("onu_Codigo_Respuesta");
        //            spec.SqlParameters.FirstOrDefault(x => x.ParameterName == "ova_Mensaje").Value = parameters?.Get<string>("ova_Mensaje") ?? string.Empty;

        //            var logErrorExceptionSp = new LogErrorException(parameters?.Get<string>("ova_Mensaje") ?? string.Empty, e);

        //            throw logErrorExceptionSp;
        //        }
        //        throw e;
        //    }
        //}

        public void SetIsValidate(bool validate)
        {
            _dbContext.IsValidate = validate;
        }

        public virtual void SetIQueryable(Func<IQueryable<T>, IQueryable<T>> func)
        {
            queriable = func.Invoke(_dbContext.Set<T>().AsQueryable());
        }

        public virtual async Task<T> GetByIdAsync<Z>(Z id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(params object[] id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllNoTrackingAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListNotTrackingAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<long> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<long> CountNoTrackingAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException e)
            {
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.TruncatedStrings)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de caracteres permitidos");
                }

                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.ArithmeticOverflow)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de dígitos permitidos");
                }

                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity, bool masivo = false)
        {
            if (masivo)
            {
                await _dbContext.InsercionMasivaAsync(entity.ToList());
            }
            else
            {
                await _dbContext.Set<T>().AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {

            var responseContract = typeof(T).GetInterfaces()?.Where(x => x.GetGenericTypeDefinition() == typeof(IHistoricable<>))?.FirstOrDefault();
            bool isHistoriable = !(responseContract is null);

            try
            {
                if (isHistoriable)
                {
                    await UpdateWithHistoricalAsync(entity).ConfigureAwait(false);
                }
                else
                {
                    _dbContext.Update(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.TruncatedStrings)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de caracteres permitidos");
                }

                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.ArithmeticOverflow)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de dígitos permitidos");
                }

                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task UpdateWithHistoricalAsync(T entity)
        {
            var historicableType = PropertiesExtension.GetTypeParameterHistoricable<T>();

            var oldEntity = GetRecordInDb(entity);

            var columnsInfo = GetColumnsNameByEntity(entity);

            var respuesta = HistoricalTransform.FromSourceObjectToHistoricalObjectUpdate<T, object>(oldEntity, entity, historicableType, columnsInfo);

            var transaction = _dbContext.Database.CurrentTransaction ?? await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();

                if (!PropertiesExtension.HaveEqualValues(entity, oldEntity, columnsInfo))
                {
                    await _dbContext.AddAsync(respuesta);
                    await _dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw e;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw e;
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entity, bool masivo = false)
        {
            var responseContract = typeof(T).GetInterfaces()?.Where(x => x.GetGenericTypeDefinition() == typeof(IHistoricable<>))?.FirstOrDefault();
            bool isHistoriable = !(responseContract is null);
            if (isHistoriable)
            {
                await UpdateWithHistoricaRangeAsync(entity, masivo).ConfigureAwait(false);
            }
            else
            {
                if (masivo)
                {
                    await _dbContext.ActualizacionMasivaAsync(entity.ToList());
                }
                else
                {
                    _dbContext.UpdateRange(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task UpdateWithHistoricaRangeAsync(IEnumerable<T> entity, bool masivo = false)
        {
            List<T> oldEntity = new List<T>();
            var historicableType = PropertiesExtension.GetTypeParameterHistoricable<T>();

            var columnsInfo = GetColumnsNameByEntity(entity.FirstOrDefault());

            foreach (var item in entity)
            {
                oldEntity.Add(GetRecordInDb(item));
            }

            if (entity.LongCount() != oldEntity.LongCount())
            {
                throw new Exception(message: "La cantidad de registros por actualizar no corresponde con la cantidad de registros existentes en la base de datos");
            }

            var respuesta = HistoricalTransform.FromSourceListToHistoricalListUpdate<T, object>(oldEntity, entity, historicableType, columnsInfo);

            var transaction = _dbContext.Database.CurrentTransaction ?? await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                /*Actualiza Registros*/
                if (masivo)
                {
                    await _dbContext.ActualizacionMasivaAsync(entity.ToList());
                }
                else
                {
                    _dbContext.UpdateRange(entity);
                    await _dbContext.SaveChangesAsync();
                }

                /*Genera históricos si los registros han cambiado*/
                if (!(respuesta is null) && respuesta.Any())
                {
                    _dbContext.UpdateRange(respuesta);
                    await _dbContext.SaveChangesAsync();

                }

                await transaction.CommitAsync().ConfigureAwait(false);

            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw e;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw e;
            }
        }

        public async Task AttachAsync(T entity, Func<T, T> func)
        {
            _dbContext.Attach(entity);
            _ = func.Invoke(entity);
            _dbContext.ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Modified && t.Properties.Where(y => y.IsModified).Any())
                    .ToList();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<RegistroModificado>> AttachCatchFieldAsync(T entity, Func<T, T> func, Func<T, string, T> func2 = null)
        {
            _dbContext.Attach(entity);
            _ = func.Invoke(entity);
            List<RegistroModificado> listaCambios = null;
            var changeEntry = _dbContext.ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Modified && t.Properties.Where(y => y.IsModified).Any())
                    .ToList();
            foreach (var entry in changeEntry)
            {
                if (listaCambios is null)
                    listaCambios = new List<RegistroModificado>();

                var key = entry.Properties.Where(k => k.Metadata.IsPrimaryKey()).FirstOrDefault();
                var key2 = entry.Properties.Where(k => k.Metadata.IsPrimaryKey()).LastOrDefault();
                foreach (var propertyEntry in entry.Properties.Where(k => k.IsModified))
                {
                    listaCambios.Add(new RegistroModificado
                    {
                        Origen = key.CurrentValue.ToString(),
                        Origen2 = key2?.CurrentValue.ToString(),
                        CampoModificado = new CampoModificado
                        {
                            Nombre = propertyEntry.Metadata.Name,
                            Valor = propertyEntry.CurrentValue?.ToString()
                        }
                    });
                }
            }
            _ = func2?.DynamicInvoke(entity, (listaCambios is null) ? null : string.Join(";", listaCambios.Select(x => x.CampoModificado.Nombre).ToArray()));
            await _dbContext.SaveChangesAsync();
            return listaCambios;
        }

        public async Task AttachRangeAsync(IEnumerable<T> entity, Func<IEnumerable<T>, IEnumerable<T>> func)
        {
            _dbContext.AttachRange(entity);
            _ = func.Invoke(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                var responseContract = typeof(T).GetInterfaces()?.Where(x => x.GetGenericTypeDefinition() == typeof(IHistoricable<>))?.FirstOrDefault();
                bool isHistoriable = !(responseContract is null);

                if (isHistoriable)
                {
                    var keyParams = typeof(T).GetProperties()
                    .Where(y => _dbContext.Entry(entity).CurrentValues.EntityType.FindPrimaryKey().Properties.Select(x => x.Name).ToArray().Contains(y.Name))
                    .Select(x => x.GetValue(entity)).ToArray();

                    var entityToDelete = _dbContext.Set<T>().Find(keyParams);

                    _dbContext.Entry(entityToDelete).State = EntityState.Detached;

                    await DeleteWithHistoricaAsync(entityToDelete).ConfigureAwait(false);
                }
                else
                {
                    _dbContext.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.TruncatedStrings)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de caracteres permitidos");
                }
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.DeleteReferenceConstrain)
                {
                    throw new ValidationException("Acción no permitida: No se pueden eliminar registros que estén asociados a otras funcionalidades");
                }
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.ArithmeticOverflow)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de dígitos permitidos");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task DeleteWithHistoricaAsync(T entity)
        {
            var historicableType = PropertiesExtension.GetTypeParameterHistoricable<T>();
            var respuesta = HistoricalTransform.FromSourceObjectToHistoricalObject<T, object>(entity, historicableType);

            var transaction = _dbContext.Database.CurrentTransaction ?? await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

                await _dbContext.AddAsync(respuesta);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                List<T> entityToDelete = new List<T>();
                var responseContract = typeof(T).GetInterfaces()?.Where(x => x.GetGenericTypeDefinition() == typeof(IHistoricable<>))?.FirstOrDefault();
                bool isHistoriable = !(responseContract is null);

                if (isHistoriable)
                {
                    foreach (var item in entity)
                    {
                        var keyParams = typeof(T).GetProperties()
                        .Where(y => _dbContext.Entry(item).CurrentValues.EntityType.FindPrimaryKey().Properties.Select(x => x.Name).ToArray().Contains(y.Name))
                        .Select(x => x.GetValue(item)).ToArray();

                        var toDelete = _dbContext.Set<T>().Find(keyParams);
                        _dbContext.Entry(toDelete).State = EntityState.Detached;

                        entityToDelete.Add(toDelete);
                    }
                    if (!(entityToDelete is null) && entityToDelete.Any())
                        await DeleteWithHistoricaRangeAsync(entityToDelete).ConfigureAwait(false);
                }
                else
                {
                    _dbContext.RemoveRange(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.TruncatedStrings)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de caracteres permitidos");
                }
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.DeleteReferenceConstrain)
                {
                    throw new ValidationException("Acción no permitida: No se pueden eliminar registros que estén asociados a otras funcionalidades");
                }
                if (((SqlException)e?.InnerException).Number == (int)ExceptionSQLErrorsEnum.ArithmeticOverflow)
                {
                    throw new ValidationException("Algunos valores superan la cantidad de dígitos permitidos");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task DeleteWithHistoricaRangeAsync(IEnumerable<T> entity)
        {
            var historicableType = PropertiesExtension.GetTypeParameterHistoricable<T>();

            var respuesta = HistoricalTransform.FromSourceListToHistoricalList<T, object>(entity, historicableType);

            var transaction = _dbContext.Database.CurrentTransaction ?? await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                _dbContext.RemoveRange(entity);
                await _dbContext.SaveChangesAsync();

                await _dbContext.AddRangeAsync(respuesta);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(queriable ?? _dbContext.Set<T>().AsQueryable(), spec);
        }

        private async Task<IQueryable<T>> ApplySpecification(string query, ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(query, _dbContext.Set<T>(), spec);
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction is null)
            {
                return _dbContext.Database.BeginTransactionAsync();
            }
            return Task.FromResult(_dbContext.Database.CurrentTransaction);
        }

        public IQueryable<T> GetIQueryable()
        {
            return queriable ?? _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetIQueryable(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(queriable ?? _dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> GetByIdAsync<Z>(params Enum[] @enum)
        {
            var dato = @enum.Select(x => Conversion.GetMensaje(x)).ToArray();
            return await _dbContext.Set<T>().FindAsync(dato);
        }

        public EntityColumnName[] GetColumnsNameByEntity(T entity)
        {
            return _dbContext.Entry(entity)?.CurrentValues?.EntityType?.GetProperties()?.Select(x => new EntityColumnName { ClassPropertyName = x.Name, TablePropertyName = x.GetColumnName() ?? string.Empty })?.ToArray();
        }

        private T GetRecordInDb(T entity)
        {
            var keyParams = GetPrimaryKeyValues(entity);

            var oldEntity = _dbContext.Set<T>().Find(keyParams);
            _dbContext.Entry(oldEntity).State = EntityState.Detached;

            return oldEntity;
        }

        public object[] GetPrimaryKeyValues(T entity)
        {
            return typeof(T).GetProperties()
                .Where(y => _dbContext.Entry(entity).CurrentValues.EntityType.FindPrimaryKey().Properties.Select(x => x.Name).ToArray().Contains(y.Name))
                .Select(x => x.GetValue(entity)).ToArray();
        }

        public bool TransactionIsClosed()
        {
            return _dbContext.Database.CurrentTransaction is null || _dbContext.Database.CurrentTransaction.GetDbTransaction().Connection.State == ConnectionState.Closed;
        }

        public bool TransactionIsOpen()
        {
            return !(_dbContext.Database.CurrentTransaction is null) && _dbContext.Database.CurrentTransaction.GetDbTransaction().Connection.State == ConnectionState.Open;
        }
    }
}
