using AutoMapper;
using TagManage.API.Requests;
using TagManage.Data.Entities;
using TagManage.Domain.ExternalApp;

namespace TagManage.API.Infrastructure

{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateTagRequest, TagEntity>()
                .ForMember(opt => opt.Id, opt => opt.Ignore());
            CreateMap<StackTagModel, TagEntity>()
                .ForMember(opt => opt.Id, opt => opt.Ignore());
        }
        
    }
}
