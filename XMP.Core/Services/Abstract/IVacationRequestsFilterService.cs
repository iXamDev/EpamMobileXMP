using System;
using System.Collections;
using System.Collections.Generic;
using XMP.Core.Models;

namespace XMP.Core.Services.Abstract
{
    public interface IVacationRequestsFilterService
    {
        IEnumerable<VacantionRequest> Filter(IEnumerable<VacantionRequest> models, VacantionRequestFilterType filterType);

        bool Filter(VacantionRequest model, VacantionRequestFilterType filterType);

        bool Filter(VacationState state, VacantionRequestFilterType filterType);
    }
}
