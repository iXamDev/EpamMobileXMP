using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XMP.API.Models;

namespace XMP.API.Services.Abstract
{
    public interface IVacationRequestsApiService
    {
        Task<IEnumerable<VacationDto>> GetVacations(CancellationToken? cancellationToken = null);

        Task<VacationDto> CreateVacation(VacationDto vacation, CancellationToken? cancellationToken = null);

        Task UpdateVacation(VacationDto vacation, CancellationToken? cancellationToken = null);
    }
}
