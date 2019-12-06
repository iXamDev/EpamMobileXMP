using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using XMP.Core.Models;
namespace XMP.Core.Services.Abstract
{
    public interface IVacationRequestsManagerService
    {
        IEnumerable<VacantionRequest> GetVacantionRequests();

        void AddVacation(NewVacationRequest vacantion);

        void UpdateVacation(VacantionRequest newVacantion);

        VacantionRequest GetVacantion(string localId);

        Task<bool> Sync(bool force = false);

        event EventHandler<VacantionRequest> VacationAdded;

        event EventHandler<VacantionRequest> VacationChanged;

        event EventHandler VacationsChanged;
    }
}
