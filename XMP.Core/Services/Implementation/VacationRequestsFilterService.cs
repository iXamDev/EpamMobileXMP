using System;
using System.Collections.Generic;
using System.Linq;
using XMP.Core.Models;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Services.Implementation
{
    public class VacationRequestsFilterService : IVacationRequestsFilterService
    {
        private Dictionary<VacantionRequestFilterType, VacationState[]> filterMap;

        public VacationRequestsFilterService()
        {
            filterMap = new Dictionary<VacantionRequestFilterType, VacationState[]>
            {
                {VacantionRequestFilterType.All, new VacationState[] { VacationState.Approved, VacationState.Closed } },
                {VacantionRequestFilterType.Open, new VacationState[] { VacationState.Approved } },
                {VacantionRequestFilterType.Closed, new VacationState[] { VacationState.Closed } }
            };
        }

        public IEnumerable<VacantionRequest> Filter(IEnumerable<VacantionRequest> models, VacantionRequestFilterType filterType)
        => models?.Where(t => Filter(t, filterType))?.AsEnumerable();

        public bool Filter(VacantionRequest model, VacantionRequestFilterType filterType)
        => model != null && Filter(model.State, filterType);

        public bool Filter(VacationState state, VacantionRequestFilterType filterType)
        => filterMap[filterType].Contains(state);
    }
}
