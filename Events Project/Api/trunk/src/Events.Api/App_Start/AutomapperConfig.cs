using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Dtos.Admin.Registration;
using Aafp.Events.Api.Dtos.EditedRegistration;
using Aafp.Events.Api.Dtos.PendingRegistration;
using Aafp.Events.Api.Dtos.Session;
using Aafp.Events.Api.Dtos.User;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;
using AutoMapper;

namespace Aafp.Events.Api
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Event, EventBaseDto>();
            Mapper.CreateMap<Event, EventDetailDto>();
            Mapper.CreateMap<Event, EventRegistrationTypeInfoDto>();
            Mapper.CreateMap<Event, AdminRegistrationEventDto>();
            Mapper.CreateMap<Event, UserEventDto>();
            Mapper.CreateMap<Event, UserRegistrationEventDto>();

            Mapper.CreateMap<EventBaseDto, EventRegistrationTypeInfoDto>();

            Mapper.CreateMap<EventScheduleItem, EventScheduleItemDto>();

            Mapper.CreateMap<Fee, EventFeeDto>();
            Mapper.CreateMap<Fee, SessionFeeDto>();

            Mapper.CreateMap<Step, StepDto>();
            Mapper.CreateMap<Step, EventStepDto>();
            Mapper.CreateMap<Step, AdminRegistrationStepDto>();
            Mapper.CreateMap<Step, UserRegistrationStepDto>();
            Mapper.CreateMap<Step, UserRegistrationNavigationStepDto>();

            Mapper.CreateMap<Heading, HeadingDto>();
            Mapper.CreateMap<Heading, AdminRegistrationHeadingDto>();
            Mapper.CreateMap<Heading, UserRegistrationHeadingDto>();
            Mapper.CreateMap<Heading, EventHeadingDto>();

            Mapper.CreateMap<Session, SessionDto>();
            Mapper.CreateMap<Session, AdminRegistrationSessionDto>();
            Mapper.CreateMap<Session, UserRegistrationSessionDto>();

            Mapper.CreateMap<RegistrantSession, AdminRegistrantSessionDto>();
            Mapper.CreateMap<RegistrantSession, EditedRegistrationPaymentSessionDto>();

            Mapper.CreateMap<SessionConflict, SessionConflictDto>();
            Mapper.CreateMap<SessionConflict, AdminRegistrationSessionConflictDto>();
            Mapper.CreateMap<SessionConflict, UserRegistrationSessionConflictDto>();

            Mapper.CreateMap<CustomerEvent, CustomerEventDto>();

            Mapper.CreateMap<AdminRegistrationDto, PendingRegistration>();
            Mapper.CreateMap<AdminRegistrationBadgeDto, PendingRegistrationBadgeDto>();
            Mapper.CreateMap<AdminRegistrationSessionDto, PendingRegistrationSessionDto>();

            Mapper.CreateMap<AdminRegistrationDto, BatchRegistrationDto>();

            Mapper.CreateMap<RegistrationInvoiceDetail, AdminRegistrantInvoiceDetailsDto>();

            Mapper.CreateMap<PendingRegistrationBadge, RegistrationBadgeDto>();
            Mapper.CreateMap<EditedRegistrationBadge, RegistrationBadgeDto>();

            Mapper.CreateMap<AdminRegistrationDto, EditedRegistration>();
            Mapper.CreateMap<AdminRegistrationBadgeDto, EditedRegistrationBadgeDto>();
            Mapper.CreateMap<AdminRegistrationSessionDto, EditedRegistrationSessionDto>();
        }
    }
}