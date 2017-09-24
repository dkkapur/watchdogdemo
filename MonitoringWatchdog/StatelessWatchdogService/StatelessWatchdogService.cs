using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using FabricMonSvc;
using Microsoft.ServiceFabric.Monitoring.Interfaces;
using Microsoft.ServiceFabric.Monitoring.Filters;
using MdsHealthDataConsumer;

namespace StatelessWatchdogService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessWatchdogService : StatelessService
    {

        private static readonly TraceWriterWrapper TraceWriter = new TraceWriterWrapper();
        private HealthDataService healthDataService;

        public StatelessWatchdogService(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            if (this.healthDataService == null)
            {
                // compose the HealthDataService instance with all the dependencies.
                IServiceConfiguration config = null;
                //var config = new ServiceConfiguration(this.serviceContext.CodePackageActivationContext, TraceWriter);
                var filterRepository = new EntityFilterRepository(config);

                var healthClient = new FabricHealthClientWrapper(
                    TraceWriter,
                    this.Context.ServiceTypeName,
                    this.Context.PartitionId,
                    this.Context.ReplicaOrInstanceId);

                //var eventWriter = new MonitoringEventWriter();
                //var metricsEmitter = new MetricsEmitter(config, TraceWriter);
                //var consumer = new IfxHealthDataConsumer(eventWriter, metricsEmitter);
                var consumer = new IfxHealthDataConsumer();
                var producer = new HealthDataProducer(healthClient, consumer, TraceWriter, config, filterRepository);
                this.healthDataService = new HealthDataService(producer, TraceWriter, config, filterRepository);

                //this.TraceInfo("Service.RunAsync: Composed new HealthDataService instance");
            }

            //this.TraceInfo("Service.RunAsync: Invoking HealthDataService.RunAsync");
            await this.healthDataService.RunAsync(cancellationToken).ConfigureAwait(false);

        }
    }
}
