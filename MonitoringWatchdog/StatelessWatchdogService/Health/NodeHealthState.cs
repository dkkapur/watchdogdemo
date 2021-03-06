﻿// ----------------------------------------------------------------------
//  <copyright file="NodeHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Health;
    using FabricMonSvc;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.NodeHealthState to support testability.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
    internal class NodeHealthState : EntityHealthState
    {
        public NodeHealthState(string nodeName, HealthState state) 
            : base(state)
        {
            this.NodeName = Guard.IsNotNullOrEmpty(nodeName, nameof(nodeName));
        }

        public string NodeName { get; private set; }
    }
}
