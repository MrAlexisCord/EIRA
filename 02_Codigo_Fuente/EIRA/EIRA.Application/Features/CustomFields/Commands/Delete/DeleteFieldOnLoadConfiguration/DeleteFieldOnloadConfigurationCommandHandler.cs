using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldGlobalConfiguration
{
    public class DeleteFieldGlobalConfigurationCommandHandler : IRequestHandler<DeleteFieldGlobalConfigurationCommand, Response<bool>>
    {
        private readonly ICustomFieldsRepository _repository;

        public DeleteFieldGlobalConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }


        public async Task<Response<bool>> Handle(DeleteFieldGlobalConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.DeleteFieldGlobalConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
            });

            return new Response<bool>(response);
        }
    }
}
