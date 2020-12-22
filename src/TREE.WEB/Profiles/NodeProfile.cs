using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using TREE.DB.Entities;
using TREE.WEB.ViewModels;

namespace TREE.WEB.Profiles
{
    public class NodeProfile : Profile
    {
        public NodeProfile()
        {
            CreateMap<Node, NodeViewModel>();
            CreateMap<Node, NodeEditViewModel>();
            CreateMap<NodeViewModel, NodeEditViewModel>();
            CreateMap<Node, LabelValueViewModel>()
                .ForMember(dest => dest.Label,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
