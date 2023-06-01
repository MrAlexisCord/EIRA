using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldOnLoadConfiguration
{
    public class CreateFieldOnLoadConfigurationCommandHandler : IRequestHandler<CreateFieldOnLoadConfigurationCommand, Response<ConfigurationFieldDTO>>
    {

        private readonly ICustomFieldsRepository _repository;

        public CreateFieldOnLoadConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ConfigurationFieldDTO>> Handle(CreateFieldOnLoadConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.CreateFieldOnLoadConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
                OrderNumber = request.OrderNumber,
            });

            return new Response<ConfigurationFieldDTO>(response);
        }
    }
}
