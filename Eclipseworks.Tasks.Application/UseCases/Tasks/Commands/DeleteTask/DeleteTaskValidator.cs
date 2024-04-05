﻿using FluentValidation;

namespace Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.DeleteTask
{
    public class DeleteTaskValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}