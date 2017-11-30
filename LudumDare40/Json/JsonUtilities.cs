using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LudumDare40.Json
{
	public static class JsonUtilities
	{
		public static T Deserialize<T>(string filename, bool useTypeHandling = false)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();

			if (useTypeHandling)
			{
				settings.TypeNameHandling = TypeNameHandling.Auto;
			}

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(Paths.Json + filename), settings);
		}
		
		public static T Deserialize<T>(string filename, JsonLoader<T> loader)
		{
			return JsonConvert.DeserializeObject<T>(File.ReadAllText(Paths.Json + filename), loader);
		}
		
		public static void Serialize(object data, string filename)
		{
			StreamWriter writer = File.CreateText(Paths.Json + filename);
			writer.Write(JsonConvert.SerializeObject(data));
			writer.Close();
		}
	}
}
