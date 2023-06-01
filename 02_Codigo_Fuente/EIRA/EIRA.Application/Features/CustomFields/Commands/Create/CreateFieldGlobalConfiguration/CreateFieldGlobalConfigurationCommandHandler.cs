using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldGlobalConfiguration
{
    public class CreateFieldGlobalConfigurationCommandHandler : IRequestHandler<CreateFieldGlobalConfigurationCommand, Response<ConfigurationFieldDTO>>
    {

        private readonly ICustomFieldsRepository _repository;

        public CreateFieldGlobalConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ConfigurationFieldDTO>> Handle(CreateFieldGlobalConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.CreateFieldGlobalConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
                OrderNumber = request.OrderNumber,
            });

            return new Response<ConfigurationFieldDTO>(response);
        }
    }
}
