using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Core.Models;

namespace XMP.Core.Services.Abstract
{
    public interface IVacationRequestsManagerService
    {
        event EventHandler<EventArgs<VacantionRequest>> VacationAdded;

        event EventHandler<EventArgs<VacantionRequest>> VacationChanged;

        event EventHandler VacationsChanged;

        IEnumerable<VacantionRequest> GetVacantionRequests();

        void AddVacation(NewVacationRequest vacantion);

        void UpdateVacation(VacantionRequest newVacantion);

        VacantionRequest GetVacantion(string localId);

        Task<bool> Sync(bool force = false);
    }
}
