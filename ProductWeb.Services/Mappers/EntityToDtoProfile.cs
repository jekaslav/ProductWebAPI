using AutoMapper;
using ProductWeb.Domain.Entities;
using ProductWeb.Domain.Models;

namespace ProductWeb.Services.Mappers;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<ProductEntity, ProductDto>();
    }
}