namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IProjectsService
    {
        Task<T> GetAllProjects<T>();
        Task<T> AssignableUsersByProjectId<T>(string projectKeyOrId);
    }
}
