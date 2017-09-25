// ----------------------------------------------------------------------
//  <copyright file="DeployedApplicationHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Health;
    using StatelessWatchdogService;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.DeployedApplicationHealthState to support testability.
    /// </summary>
    internal class DeployedApplicationHealthState : EntityHealthState
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public DeployedApplicationHealthState(string appName, string nodeName, HealthState state) 
            : base(state)
        {
            this.ApplicationName = Guard.IsNotNullOrEmpty(appName, nameof(appName));
            this.NodeName = Guard.IsNotNullOrEmpty(nodeName, nameof(nodeName));
        }

        public string ApplicationName { get; private set; }

        public string NodeName { get; private set; }
    }
}
