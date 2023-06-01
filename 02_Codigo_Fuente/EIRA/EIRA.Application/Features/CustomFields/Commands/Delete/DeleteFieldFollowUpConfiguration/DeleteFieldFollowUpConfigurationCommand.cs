using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldFollowUpConfiguration
{
    public class DeleteFieldFollowUpConfigurationCommand : IRequest<Response<bool>>
    {
        public string ProjectId { get; set; }
        public string FieldId { get; set; }
    }
}
