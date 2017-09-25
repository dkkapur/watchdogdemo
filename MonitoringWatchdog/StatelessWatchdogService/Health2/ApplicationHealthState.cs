// ----------------------------------------------------------------------
//  <copyright file="ApplicationHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Health;
    using StatelessWatchdogService;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.ApplicationHealthState to support testability.
    /// </summary>
    internal class ApplicationHealthState : EntityHealthState
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public ApplicationHealthState(Uri appUri, HealthState state) 
            : base(state)
        {
            this.ApplicationName = Guard.IsNotNull(appUri, nameof(appUri));
        }

        public Uri ApplicationName { get; private set; }
    }
}
