using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldOnLoadConfiguration
{
    public class DeleteFieldOnloadConfigurationCommandHandler : IRequestHandler<DeleteFieldOnloadConfigurationCommand, Response<bool>>
    {
        private readonly ICustomFieldsRepository _repository;

        public DeleteFieldOnloadConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }


        public async Task<Response<bool>> Handle(DeleteFieldOnloadConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.DeleteFieldOnLoadConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
            });

            return new Response<bool>(response);
        }
    }
}
