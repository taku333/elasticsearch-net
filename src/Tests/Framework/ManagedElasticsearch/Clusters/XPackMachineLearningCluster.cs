﻿using System;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	// TODO! inherit from XPackCluster
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackMachineLearningCluster : ClusterBase
	{
		protected string[] XPackSettings => TestClient.VersionUnderTestSatisfiedBy(">=5.5.0")
			? new[] {"xpack.security.authc.token.enabled=true"}
			: new string[] {} ;

		protected override string[] AdditionalServerSettings => base.AdditionalServerSettings.Concat(this.XPackSettings).ToArray();

		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");

		protected override void SeedNode()
		{
			Console.WriteLine("Running SeedNode");
			new DefaultSeeder(this.Node).SeedNode();
			new MachineLearningSeeder(this.Node).SeedNode();
		}

		protected override InstallationTaskBase[] AdditionalInstallationTasks => new []
		{
			new DownloadMachineLearningSampleDataDistribution()
		};
	}
}