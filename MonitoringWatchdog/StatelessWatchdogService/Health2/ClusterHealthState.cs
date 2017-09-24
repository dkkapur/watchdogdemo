// ----------------------------------------------------------------------
//  <copyright file="ClusterHealthState.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System.Fabric.Health;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.ClusterHealthState to support testability.
    /// </summary>
    internal class ClusterHealthState : EntityHealthState
    {
        public ClusterHealthState(HealthState state) 
            : base(state)
        {
        }
    }
}
