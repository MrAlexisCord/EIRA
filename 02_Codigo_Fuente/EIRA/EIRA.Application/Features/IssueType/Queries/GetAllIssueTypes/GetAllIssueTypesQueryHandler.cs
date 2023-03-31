using AutoMapper;
using EIRA.Application.Contracts.Persistence.IssueType;
using EIRA.Application.DTOs;
using MediatR;

namespace EIRA.Application.Features.IssueType.Queries.GetAllIssueTypes
{
    public class GetAllIssueTypesQueryHandler : IRequestHandler<GetAllIssueTypesQuery, List<IssueTypeDto>>
    {
        private readonly IIssueTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllIssueTypesQueryHandler(IIssueTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<IssueTypeDto>> Handle(GetAllIssueTypesQuery request, CancellationToken cancellationToken)
        {
            var jiraResponse = await _repository.GetAll();
            return _mapper.Map<List<IssueTypeDto>>(jiraResponse);

        }
    }
}
