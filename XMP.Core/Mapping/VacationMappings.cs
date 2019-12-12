using System;
using ExpressMapper;
using XMP.API.Models;
using XMP.Core.Database.Implementation.RealmDatabase.VacationRequests;
using XMP.Core.Models;

namespace XMP.Core.Mapping
{
    public static class VacationMappings
    {
        private static EnumMapping<API.Models.VacationType, XMP.Core.Models.VacationType> _vacationTypeMapping
        = new EnumMapping<API.Models.VacationType, Models.VacationType>(
           new System.Collections.Generic.Dictionary<API.Models.VacationType, Models.VacationType>
           {
                { API.Models.VacationType.Exceptional, Models.VacationType.Exceptional },
                { API.Models.VacationType.Sick, Models.VacationType.Sick },
                { API.Models.VacationType.Regular, Models.VacationType.Regular },
                { API.Models.VacationType.Overtime, Models.VacationType.Overtime },
                { API.Models.VacationType.LeaveWithoutPay, Models.VacationType.WithoutPay },
           },
           API.Models.VacationType.Regular,
           Models.VacationType.Regular);

        private static EnumMapping<API.Models.VacationStatus, XMP.Core.Models.VacationState> _vacationStateMapping
        = new EnumMapping<API.Models.VacationStatus, Models.VacationState>(
            new System.Collections.Generic.Dictionary<API.Models.VacationStatus, Models.VacationState>
            {
                { API.Models.VacationStatus.Draft, Models.VacationState.Draft },
                { API.Models.VacationStatus.Submitted, Models.VacationState.Submitted },
                { API.Models.VacationStatus.Approved, Models.VacationState.Approved },
                { API.Models.VacationStatus.InProgress, Models.VacationState.InProgress },
                { API.Models.VacationStatus.Closed, Models.VacationState.Closed }
            },
            API.Models.VacationStatus.Draft,
            Models.VacationState.Draft);

        public static DateTimeOffset VacationDateToDateTimeOffset(this DateTime dateTime)
        => new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));

        public static DateTime VacationDateTimeOffsetToDateTime(this DateTimeOffset dateTimeOffset)
        => dateTimeOffset.DateTime;

        public static void RegisterMappings()
        {
            Mapper.Register<VacationDto, VacantionRequest>()
                .Function(dest => dest.SyncState, (arg) => SynchronizationState.Synced)
                .Function(dest => dest.VacationType, src => _vacationTypeMapping.Get(src.VacationType))
                .Function(dest => dest.State, src => _vacationStateMapping.Get(src.VacationStatus))
                .Function(dest => dest.Start, src => src.Start.VacationDateToDateTimeOffset())
                .Function(dest => dest.End, src => src.End.VacationDateToDateTimeOffset())
                .Function(dest => dest.Created, src => src.Created.VacationDateToDateTimeOffset())
                .Ignore(dest => dest.LocalId);

            Mapper.Register<VacantionRequest, VacationDto>()
                .Function(dest => dest.VacationType, src => _vacationTypeMapping.Get(src.VacationType))
                .Function(dest => dest.VacationStatus, src => _vacationStateMapping.Get(src.State))
                .Function(dest => dest.Start, src => src.Start.VacationDateTimeOffsetToDateTime())
                .Function(dest => dest.End, src => src.End.VacationDateTimeOffsetToDateTime())
                .Function(dest => dest.Created, src => src.Created.VacationDateTimeOffsetToDateTime());

            Mapper.Register<NewVacationRequest, VacantionRequest>()
                .Function(dest => dest.Start, src => src.Start.Date.VacationDateToDateTimeOffset())
                .Function(dest => dest.End, src => src.End.Date.VacationDateToDateTimeOffset());

            Mapper.Register<VacantionRequest, VacationRequestRealmObject>()
                .Function(dest => dest.State, src => (short)src.State)
                .Function(dest => dest.SyncState, src => (short)src.SyncState)
                .Function(dest => dest.VacationType, src => (short)src.VacationType);

            Mapper.Register<VacationRequestRealmObject, VacantionRequest>()
                .Function(dest => dest.State, src => (VacationState)src.State)
                .Function(dest => dest.SyncState, src => (SynchronizationState)src.SyncState)
                .Function(dest => dest.VacationType, src => (Models.VacationType)src.VacationType);
        }
    }
}
