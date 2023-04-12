namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IResponsibleService
    {
        Task<T> GetResponsibleList<T>();
        Task<T> GetDefaultValue<T>();
    }
}
