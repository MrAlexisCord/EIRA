using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Infrastructure.Repositories.Eira
{
    public class IssueTypeRepository : IIssueTypeRepository
    {

        private readonly IAsyncRepository<AppConfigurationIssueType> _repository;
        private readonly IMapper _mapper;

        public IssueTypeRepository(IAsyncRepository<AppConfigurationIssueType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<IssueTypeConfigurationDTO>> GetIssueTypeConfiguration()
        {
            var response = await _repository.ListAllNoTrackingAsync();
            var dtoResult = _mapper.Map<List<IssueTypeConfigurationDTO>>(response);
            return dtoResult;
        }
    }
}
