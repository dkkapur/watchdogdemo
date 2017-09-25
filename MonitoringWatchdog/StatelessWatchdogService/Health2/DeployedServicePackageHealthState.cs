﻿// ----------------------------------------------------------------------
//  <copyright file="DeployedServicePackageHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Health;
    using StatelessWatchdogService;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.DeployedServicePackageHealthState to support testability.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
    internal class DeployedServicePackageHealthState : EntityHealthState
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public DeployedServicePackageHealthState(string appName, string serviceName, string nodeName, HealthState state) 
            : base(state)
        {
            this.ApplicationName = Guard.IsNotNullOrEmpty(appName, nameof(appName));
            this.ServiceManifestName = Guard.IsNotNullOrEmpty(serviceName, nameof(serviceName));
            this.NodeName = Guard.IsNotNullOrEmpty(nodeName, nameof(nodeName));
        }

        public string ApplicationName { get; private set; }

        public string ServiceManifestName { get; private set; }

        public string NodeName { get; private set; }
    }
}
