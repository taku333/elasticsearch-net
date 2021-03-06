using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IBucketAggregation : IAggregation
	{
		AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregationBase : AggregationBase, IBucketAggregation
	{
		internal BucketAggregationBase() { }

		protected BucketAggregationBase(string name) : base(name) { }

		public AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
		: IBucketAggregation, IDescriptor
		where TBucketAggregation : BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
		, TBucketAggregationInterface, IBucketAggregation
		where T : class
		where TBucketAggregationInterface : class, IBucketAggregation
	{
		protected TBucketAggregationInterface Self => (TBucketAggregation)this;
		AggregationDictionary IBucketAggregation.Aggregations { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		string IAggregation.Name { get; set; }

		protected TBucketAggregation Assign(Action<TBucketAggregationInterface> assigner) =>
			Fluent.Assign((TBucketAggregation)this, assigner);

		public TBucketAggregation Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);

		public TBucketAggregation Aggregations(AggregationDictionary aggregations) =>
			Assign(a => a.Aggregations = aggregations);

		public TBucketAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
