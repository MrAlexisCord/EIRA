using EIRA.Application.DTOs;
using MediatR;

namespace EIRA.Application.Features.IssueType.Queries.GetAllIssueTypes
{
    public class GetAllIssueTypesQuery: IRequest<List<IssueTypeDto>>
    {

    }
}
