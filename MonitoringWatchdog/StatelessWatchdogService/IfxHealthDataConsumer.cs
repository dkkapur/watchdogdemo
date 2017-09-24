namespace MdsHealthDataConsumer
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using FabricMonSvc;
    using Microsoft.ServiceFabric.Monitoring.Health;
    using StatelessWatchdogService;

    /// <summary>
    /// Concrete implementation of HealthDataConsumer which uses <see cref="Microsoft.Cloud.InstrumentationFramework"/> to upload health data to MDM and MDS pipelines of Geneva.
    /// </summary>
    internal class IfxHealthDataConsumer : HealthDataConsumer
    {
        private readonly Task completedTask = Task.FromResult(0);
        
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "This rule breaks for nameof() operator.")]
        public IfxHealthDataConsumer()
        {

        }

        public override Task ProcessApplicationHealthAsync(ApplicationEntity application)
        {
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
            cluster.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"cluster: {cluster}, health: {healthEvent}");
                });

            return this.completedTask;
        }

        public override Task ProcessDeployedApplicationHealthAsync(DeployedApplicationEntity deployedApplication)
        {

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
            node.Health.HealthEvents
                .ForEachHealthEvent(healthEvent =>
                {
                    ServiceEventSource.Current.Message($"node: {node}, health: {healthEvent}");
                });

            return this.completedTask;
        }

        public override Task ProcessPartitionHealthAsync(PartitionEntity partition)
        {

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

            if (service.IsHealthEventReportingEnabled(
                service.ApplicationName,
                service.Health.AggregatedHealthState))
            {
                service.Health.HealthEvents
                    .ForEachHealthEvent(healthEvent =>
                    {
                        ServiceEventSource.Current.Message($"service: {service}, health: {healthEvent}");
                        if (service.ServiceName == "SampleStatelessService1")
                        {
                            ServiceEventSource.Current.Message("Change bulb here");
                            // add bulb control from other proj
                        }
                    });
            }

            return this.completedTask;
        }
    }
}