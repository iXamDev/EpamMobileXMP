using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using XMP.API.Models;
using System.Threading;

namespace XMP.API.Services.Abstract
{
    public interface IVacationRequestsApiService
    {
        Task<IEnumerable<VacationDto>> GetVacations(CancellationToken? cancellationToken = null);

        Task<VacationDto> CreateVacation(VacationDto vacation, CancellationToken? cancellationToken = null);

        Task UpdateVacation(VacationDto vacation, CancellationToken? cancellationToken = null);
    }
}
