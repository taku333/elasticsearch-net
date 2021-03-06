﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc />
		IAnalyzeResponse Analyze(IAnalyzeRequest request);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector) =>
			Analyze(selector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc />
		public IAnalyzeResponse Analyze(IAnalyzeRequest request) =>
			Dispatcher.Dispatch<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse>(
				request,
				LowLevelDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>
			);

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			AnalyzeAsync(selector?.Invoke(new AnalyzeDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>
			);
	}
}
