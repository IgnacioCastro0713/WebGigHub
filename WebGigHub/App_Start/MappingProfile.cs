using AutoMapper;
using WebGigHub.Core.Dtos;
using WebGigHub.Core.Models;

namespace WebGigHub
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var configuration = new MapperConfiguration(cfx =>
            {
                cfx.CreateMap<ApplicationUser, UserDto>();
                cfx.CreateMap<Gig, GigDto>();
                cfx.CreateMap<Notification, NotificationDto>();
                //cfx.CreateMap<UserNotification, UserNotificationDto>();
            });
        }
    }
}