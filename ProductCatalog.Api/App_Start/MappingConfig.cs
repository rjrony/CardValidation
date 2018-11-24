using AutoMapper;

namespace CardValidation.Api
{
    public class MappingConfig
    {
        public static void RegisterMapping()
        {
            Mapper.AddProfile(new Map());
        }
    }
    public class Map : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<Model, DtoView>();

        }
    }
}