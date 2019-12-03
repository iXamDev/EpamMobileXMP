using System;
using System.Collections.Generic;
namespace XMP.Core.Operations
{
    public static class PreventRepetitiveExecutionsHelper
    {
        private const string OperationRunningKey = nameof(OperationRunningKey);

        public static bool IsRunning(IDictionary<string, object> customData)
        => customData.ContainsKey(OperationRunningKey);

        public static void SetIsRunning(IDictionary<string, object> customData, bool running)
        {
            if (running)
            {
                customData[OperationRunningKey] = null;
            }
            else
            {
                customData.Remove(OperationRunningKey);
            }
        }
    }
}
