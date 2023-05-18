namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IProjectsService
    {
        Task<T> GetAllProjects<T>();
    }
}
