using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class ResponsibleJiraRepository : IResponsibleJiraRepository
    {
        private readonly IResponsibleService _responsibleService;

        public ResponsibleJiraRepository(IResponsibleService responsibleService)
        {
            _responsibleService = responsibleService;
        }

        public async Task<string> GetDefaultValue()
        {
            var response = await _responsibleService.GetDefaultValue<BaseJiraResult<List<CustomFieldContext>>>();
            if (response is null || response.Values is null || !response.Values.Any() || response.Values.FirstOrDefault() is null)
                return null;
            return response.Values.FirstOrDefault().OptionId;
        }

        public async Task<List<KeyValueList>> GetResponsibleList()
        {
            var response = await _responsibleService.GetResponsibleList<BaseJiraResult<List<KeyValueList>>>();
            return response.Values;
        }
    }
}
