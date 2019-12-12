using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XMP.API.Models;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class VacationRequestsApiService : CommonApiService, IVacationRequestsApiService
    {
        private const string ControllerUrl = "workflow";

        public VacationRequestsApiService(IWebConnectionService webConnectionService, IApiSettingsService apiSettingService)
            : base(webConnectionService, apiSettingService)
        {
        }

        public async Task<VacationDto> CreateVacation(VacationDto vacation, CancellationToken? cancellationToken = null)
        {
            var responce = await Post<ApiResponceDto<VacationDto>>(ControllerUrl, ToStringContent(vacation), cancellationToken);

            ThrowApiExceptionIfNeeded(responce);

            return responce.Result;
        }

        public async Task<IEnumerable<VacationDto>> GetVacations(CancellationToken? cancellationToken = null)
        {
            var responce = await Get<ApiResponceDto<IEnumerable<VacationDto>>>(ControllerUrl, cancellationToken);

            ThrowApiExceptionIfNeeded(responce);

            return responce.Result;
        }

        public async Task UpdateVacation(VacationDto vacation, CancellationToken? cancellationToken = null)
        {
            var responce = await Post<ApiResponceDto<VacationDto>>(ControllerUrl, ToStringContent(vacation), cancellationToken);

            ThrowApiExceptionIfNeeded(responce);
        }
    }
}
