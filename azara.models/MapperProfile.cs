namespace azara.models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserEntity, UserResponse>();

        CreateMap<AdminEntity, AdminResponse>();

        CreateMap<EventInsertUpdateRequest, EventsEntity>();

        CreateMap<EventsEntity, InsertUpdateEventResponse>();

        CreateMap<DealsEntity, DealsUpdateResponse>();

        CreateMap<UserProductRequest, ProductRequestEntity>();
    }
}