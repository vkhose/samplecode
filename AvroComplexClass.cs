// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.7.7.5
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace au.com.example.@event
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	public partial class Parent : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"Parent\",\"namespace\":\"au.com.example.event\",\"fields\":[{\"n" +
				"ame\":\"children\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"Child\",\"" +
				"namespace\":\"au.com.example.event\",\"fields\":[{\"name\":\"name\",\"type\":\"string\"}]}}}]" +
				"}");
		private IList<au.com.example.@event.Child> _children;
		public virtual Schema Schema
		{
			get
			{
				return Parent._SCHEMA;
			}
		}
		public IList<au.com.example.@event.Child> children
		{
			get
			{
				return this._children;
			}
			set
			{
				this._children = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.children;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.children = (IList<au.com.example.@event.Child>)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
