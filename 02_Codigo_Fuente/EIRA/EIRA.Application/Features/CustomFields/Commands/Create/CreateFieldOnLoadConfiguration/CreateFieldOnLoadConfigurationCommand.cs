using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldOnLoadConfiguration
{
    public class CreateFieldOnLoadConfigurationCommand : IRequest<Response<ConfigurationFieldDTO>>
    {
        public string ProjectId { get; set; }
        public string FieldId { get; set; }
        public int OrderNumber { get; set; } = 1;
    }
}
