using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldFollowUpConfiguration
{
    public class DeleteFieldFollowUpConfigurationCommandHandler : IRequestHandler<DeleteFieldFollowUpConfigurationCommand, Response<bool>>
    {
        private readonly ICustomFieldsRepository _repository;

        public DeleteFieldFollowUpConfigurationCommandHandler(ICustomFieldsRepository repository)
        {
            _repository = repository;
        }


        public async Task<Response<bool>> Handle(DeleteFieldFollowUpConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.DeleteFieldFollowConfiguration(new ConfigurationFieldDTO
            {
                FieldId = request.FieldId,
                ProjectId = request.ProjectId,
            });

            return new Response<bool>(response);
        }
    }
}
