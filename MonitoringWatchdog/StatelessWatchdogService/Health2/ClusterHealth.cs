// ----------------------------------------------------------------------
//  <copyright file="ClusterHealth.cs" company="Microsoft">
//       Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------

namespace Microsoft.ServiceFabric.Monitoring.Health
{
    using System.Collections.Generic;
    using System.Fabric.Health;
    using System.Linq;

    /// <summary>
    /// Extensible wrapper for System.Fabric.Health.ClusterHealth to support testability.
    /// </summary>
    internal class ClusterHealth : EntityHealth
    {
        internal ClusterHealth(HealthState aggregatedHealthState)
            : base(aggregatedHealthState)
        {
        }

        internal ClusterHealth(
            HealthState aggregatedHealthState,
            IEnumerable<EntityHealthEvent> healthEvents,
            IList<System.Fabric.Health.HealthEvaluation> unhealthyEvaluations)
            : base(aggregatedHealthState, healthEvents, unhealthyEvaluations)
        {
        }

        internal ClusterHealth(System.Fabric.Health.ClusterHealth health)
            : base(health)
        {
            this.ApplicationHealthStates = health.ApplicationHealthStates
                .Select(state => new ApplicationHealthState(
                    state.ApplicationName,
                    state.AggregatedHealthState));

            this.NodeHealthStates = health.NodeHealthStates
                .Select(state => new NodeHealthState(
                    state.NodeName,
                    state.AggregatedHealthState));
        }

        internal virtual IEnumerable<ApplicationHealthState> ApplicationHealthStates { get; private set; }

        internal virtual IEnumerable<NodeHealthState> NodeHealthStates { get; private set; }
    }
}