using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldFollowUpConfiguration
{
    public class CreateFieldFollowUpConfigurationCommandHandler : IRequestHandler<CreateFieldFollowUpConfigurationCommand, Response<ConfigurationFieldDTO>>
    {

        private readonly ICustomFieldsRepository _repository;

        public CreateFieldFollowUpConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ConfigurationFieldDTO>> Handle(CreateFieldFollowUpConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.CreateFieldFollowConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
                OrderNumber = request.OrderNumber,
            });

            return new Response<ConfigurationFieldDTO>(response);
        }
    }
}
