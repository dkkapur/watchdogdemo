namespace StatelessWatchdogService
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.ServiceFabric.Monitoring.Health;

    /// <summary>
    /// Concrete implementation of HealthDataConsumer.
    /// </summary>
    internal class SampleHealthDataConsumer : HealthDataConsumer
    {
        private readonly Task completedTask = Task.FromResult(0);
        
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public SampleHealthDataConsumer()
        {
        }

        public override Task ProcessApplicationHealthAsync(ApplicationEntity application)
        {
            ServiceEventSource.Current.Message($"process application: {application}, aggregatedHealth: {application.Health.AggregatedHealthState}");

            if (application.IsHealthEventReportingEnabled(
                application.ApplicationName,
                application.Health.AggregatedHealthState))
            {
                application.Health.HealthEvents
                    .ForEachHealthEvent(healthEvent =>
                    {
                        ServiceEventSource.Current.Message($"process application: {application}, health: {healthEvent}");
                        //Message(application, healthEvent);
                    });
            }

            return this.completedTask;
        }

        public override Task ProcessClusterHealthAsync(ClusterEntity cluster)
        {
            ServiceEventSource.Current.Message($"cluster: {cluster}, aggregatedHealth: {cluster.Health.AggregatedHealthState}");

            cluster.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"cluster: {cluster}, health: {healthEvent}");
                });

            return this.completedTask;
        }

        public override Task ProcessDeployedApplicationHealthAsync(DeployedApplicationEntity deployedApplication)
        {
            ServiceEventSource.Current.Message($"deployed application: {deployedApplication}, aggregatedHealth: {deployedApplication.Health.AggregatedHealthState}");

            if (deployedApplication.IsHealthEventReportingEnabled(
                deployedApplication.ApplicationName,
                deployedApplication.Health.AggregatedHealthState))
            {
                deployedApplication.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"deployed application: {deployedApplication}, health: {healthEvent}");
                });
            }

            return this.completedTask;
        }

        public override Task ProcessDeployedServicePackageHealthAsync(DeployedServicePackageEntity deployedServicePackage)
        {
            ServiceEventSource.Current.Message($"deployed service package: {deployedServicePackage}, aggregatedHealth: {deployedServicePackage.Health.AggregatedHealthState}");

            if (deployedServicePackage.IsHealthEventReportingEnabled(
                deployedServicePackage.ApplicationName,
                deployedServicePackage.Health.AggregatedHealthState))
            {
                deployedServicePackage.Health.HealthEvents
                    .ForEachHealthEvent(healthEvent =>
                    {
                        ServiceEventSource.Current.Message($"deployed service package: {deployedServicePackage}, health: {healthEvent}");
                    });
            }

            return this.completedTask;
        }

        public override Task ProcessNodeHealthAsync(NodeEntity node)
        {
            ServiceEventSource.Current.Message($"node: {node}, aggregatedHealth: {node.Health.AggregatedHealthState}");

            node.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"node: {node}, health: {healthEvent}");
                });

            return this.completedTask;
        }

        public override Task ProcessPartitionHealthAsync(PartitionEntity partition)
        {
            ServiceEventSource.Current.Message($"partition: {partition}, aggregatedHealth: {partition.Health.AggregatedHealthState}");

            if (partition.IsHealthEventReportingEnabled(
                partition.ApplicationName,
                partition.Health.AggregatedHealthState))
            {
                partition.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"partition: {partition}, health: {healthEvent}");
                });
            }

            return this.completedTask;
        }

        public override Task ProcessReplicaHealthAsync(ReplicaEntity replica)
        {
            ServiceEventSource.Current.Message($"replica: {replica}, aggregatedHealth: {replica.Health.AggregatedHealthState}");

            if (replica.IsHealthEventReportingEnabled(
                replica.ApplicationName,
                replica.Health.AggregatedHealthState))
            {
                replica.Health.HealthEvents
                    .ForEachHealthEvent(healthEvent =>
                    {
                        ServiceEventSource.Current.Message($"replica: {replica}, health: {healthEvent}");
                    });
            }

            return this.completedTask;
        }

        public override Task ProcessServiceHealthAsync(ServiceEntity service)
        {
            ServiceEventSource.Current.Message($"service: {service}, aggregatedHealth: {service.Health.AggregatedHealthState}");
            if (service.ServiceName == "SampleStatelessService1")
            {
                ServiceEventSource.Current.Message("Change bulb here");
                // add bulb control from other proj
            }

            if (service.IsHealthEventReportingEnabled(
                service.ApplicationName,
                service.Health.AggregatedHealthState))
            {
                service.Health.HealthEvents
                    .ForEachHealthEvent(healthEvent =>
                    {
                        ServiceEventSource.Current.Message($"service: {service}, health: {healthEvent}");
                    });
            }

            return this.completedTask;
        }
    }
}