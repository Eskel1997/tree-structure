using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TREE.WEB.ViewModels;

namespace TREE.WEB.Validation
{
    public class NodeEditViewModelValidator : AbstractValidator<NodeEditViewModel>
    {
        public NodeEditViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
