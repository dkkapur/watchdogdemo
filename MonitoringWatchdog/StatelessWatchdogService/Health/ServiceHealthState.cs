// ----------------------------------------------------------------------
//  <copyright file="ServiceHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Health;
    using FabricMonSvc;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.ServiceHealthState to support testability.
    /// </summary>
    internal class ServiceHealthState : EntityHealthState
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public ServiceHealthState(Uri serviceName, HealthState state) 
            : base(state)
        {
            this.ServiceName = Guard.IsNotNull(serviceName, nameof(serviceName));
        }

        public Uri ServiceName { get; private set; }
    }
}
