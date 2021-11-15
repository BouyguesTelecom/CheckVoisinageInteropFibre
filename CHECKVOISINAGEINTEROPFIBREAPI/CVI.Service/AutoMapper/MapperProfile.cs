using AutoMapper;
using CVI.Domain.Model;
using CVI.Domain.Model.Photo;
using CVI.Service.CVI.DTO;
using CVI.Service.CVI.DTO.Photo;

namespace CVI.Service.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // TODO: ajouter les fonctions de mapping une fois que les entités et les DTO seront créer
            CreateMap<Photo, PhotoDTO>().ReverseMap();

        }
    }
}
