﻿// ----------------------------------------------------------------------
//  <copyright file="EntityFilter.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Filters
{
    using System.Collections.Generic;
    using System.Fabric.Health;
    using StatelessWatchdogService;
    using Health;

    internal class EntityFilter
    {
        public EntityFilter(WhenToReportEntityHealth whenToReport, bool reportAllApps, ISet<string> appWhitelist)
        {
            this.WhenToReport = whenToReport;
            this.ReportAllApps = reportAllApps;

            this.AppWhitelist = whenToReport == WhenToReportEntityHealth.Never || reportAllApps 
                ? null 
                : Guard.IsNotNull(appWhitelist, nameof(appWhitelist));
        }

        internal WhenToReportEntityHealth WhenToReport { get; private set; }

        internal bool ReportAllApps { get; private set; }

        internal ISet<string> AppWhitelist { get; private set; }

        public bool IsEntityEnabled(string applicationName)
        {
            if (this.WhenToReport == WhenToReportEntityHealth.Never)
            {
                return false;
            }

            return this.ReportAllApps
                ? true
                : this.AppWhitelist != null && this.AppWhitelist.Contains(applicationName);
        }

        public bool IsEntityEnabled(HealthState aggregatedHealthState)
        {
            if (this.WhenToReport == WhenToReportEntityHealth.Never)
            {
                return false;
            }

            if (this.WhenToReport == WhenToReportEntityHealth.OnWarningOrError)
            {
                return aggregatedHealthState.IsWarningOrError();
            }

            return true;
        }
    }
}