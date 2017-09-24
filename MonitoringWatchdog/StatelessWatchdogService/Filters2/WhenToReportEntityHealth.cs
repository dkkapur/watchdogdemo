// ----------------------------------------------------------------------
//  <copyright file="WhenToReportEntityHealth.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Filters
{
    internal enum WhenToReportEntityHealth
    {
        Always = 0,
        OnWarningOrError,
        Never
    }
}
