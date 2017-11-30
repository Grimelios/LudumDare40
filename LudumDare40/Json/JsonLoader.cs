using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LudumDare40.Json
{
	/// <summary>
	/// Abstract class used to load Json files using custom creation logic. See
	/// https://stackoverflow.com/questions/8030538/how-to-implement-custom-jsonconverter-in-json-net-to-deserialize-a-list-of-base.
	/// </summary>
	public abstract class JsonLoader<T> : JsonConverter
	{
		protected abstract T Create(JObject jObject);
		
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}
		
		public override bool CanWrite => false;
		
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);

			T target = Create(jObject);
			serializer.Populate(jObject.CreateReader(), target);

			return target;
		}
		
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
