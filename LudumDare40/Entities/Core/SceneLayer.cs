using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;

namespace LudumDare40.Entities.Core
{
	public class SceneLayer : IDynamic, IRenderable
	{
		private List<Entity>[] entities;

		private int[] updateOrder;
		private int[] drawOrder;
		
		public SceneLayer(string[] updateOrder, string[] drawOrder)
		{
			this.updateOrder = new int[updateOrder.Length];
			this.drawOrder = new int[drawOrder.Length];

			entities = new List<Entity>[CoreFunctions.EnumCount<EntityTypes>()];

			InitializeArray(this.updateOrder, updateOrder);
			InitializeArray(this.drawOrder, drawOrder);
		}
		
		private void InitializeArray(int[] array, string[] rawArray)
		{
			Type enumType = typeof(EntityTypes);

			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)Enum.Parse(enumType, rawArray[i]);
			}
		}
		
		public void Add(Entity entity)
		{
			VerifyList(entity.EntityType).Add(entity);
		}
		
		public void Add(List<Entity> entities)
		{
			VerifyList(entities[0].EntityType).AddRange(entities);
		}
		
		public void Remove(Entity entity)
		{
			entity.Dispose();
			entities[(int)entity.EntityType].Remove(entity);
		}
		
		public List<Entity> GetEntities(EntityTypes type)
		{
			return VerifyList(type);
		}
		
		private List<Entity> VerifyList(EntityTypes type)
		{
			int index = (int)type;

			return entities[index] ?? (entities[index] = new List<Entity>());
		}
		
		public void Update(float dt)
		{
			foreach (int group in updateOrder)
			{
				entities[group]?.ForEach(e => e.Update(dt));
			}
		}
		
		public void Draw(SuperBatch sb)
		{
			foreach (int group in drawOrder)
			{
				entities[group]?.ForEach(e => e.Draw(sb));
			}
		}
	}
}
