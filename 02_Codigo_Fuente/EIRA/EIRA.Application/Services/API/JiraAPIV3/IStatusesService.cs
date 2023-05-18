namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IStatusesService
    {
        Task<T> GetAllStatusesByProject<T>(string projectId);
    }
}
