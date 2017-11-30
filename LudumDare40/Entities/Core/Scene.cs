using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;
using LudumDare40.Json;

namespace LudumDare40.Entities.Core
{
	public class Scene : IDynamic, IRenderable
	{
		private SceneLayer[] layers;
		private List<Entity>[] addLists;
		private List<Entity>[] removeLists;
		
		public Scene()
		{
			layers = JsonUtilities.Deserialize<SceneLayer[]>("Layers.json");

			// Lists are instantiated within the array as needed.
			addLists = new List<Entity>[layers.Length];
			removeLists = new List<Entity>[layers.Length];
		}
		
		public void Add(int layer, Entity entity)
		{
			layers[layer].Add(entity);

			List<Entity> list = addLists[layer] ?? (addLists[layer] = new List<Entity>());
			list.Add(entity);
		}
		
		public void Remove(int layer, Entity entity)
		{
			List<Entity> list = removeLists[layer] ?? (removeLists[layer] = new List<Entity>());
			list.Add(entity);
		}
		
		public List<Entity> GetEntities(int layer, EntityTypes type)
		{
			return layers[layer].GetEntities(type);
		}
		
		public void Update(float dt)
		{
			foreach (SceneLayer layer in layers)
			{
				layer.Update(dt);
			}

			for (int i = 0; i < layers.Length; i++)
			{
				SceneLayer layer = layers[i];
				List<Entity> removeList = removeLists[i];
				List<Entity> addList = addLists[i];

				if (removeList != null)
				{
					removeList.ForEach(e => layer.Remove(e));
					removeList.Clear();
				}

				if (addList != null)
				{
					addList.ForEach(e => layer.Add(e));
					addList.Clear();
				}
			}
		}

		public void Draw(SuperBatch sb)
		{
			foreach (SceneLayer layer in layers)
			{
				layer.Draw(sb);
			}
		}
	}
}
