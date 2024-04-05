﻿using Eclipseworks.Tasks.Application.Exceptions;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipseworks.Tasks.Application.UseCases.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _uow;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork uow)
        {
            _projectRepository = projectRepository;
            _uow = uow;
        }

        public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null) throw new NotFoundException("project not found");

            _projectRepository.Delete(project);
            await _uow.SaveChangesAsync();
        }
    }
}
