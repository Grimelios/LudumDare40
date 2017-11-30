using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Interfaces;

namespace LudumDare40
{
	public static class MessageSystem
	{
		private static List<IMessageReceiver>[] receivers;
		private static List<Tuple<MessageTypes, IMessageReceiver>> addList;
		private static List<Tuple<MessageTypes, IMessageReceiver>> removeList;
		
		static MessageSystem()
		{
			receivers = new List<IMessageReceiver>[CoreFunctions.EnumCount<MessageTypes>()];
			addList = new List<Tuple<MessageTypes, IMessageReceiver>>();
			removeList = new List<Tuple<MessageTypes, IMessageReceiver>>();
		}
		
		public static void Subscribe(MessageTypes messageType, IMessageReceiver receiver)
		{
			addList.Add(new Tuple<MessageTypes, IMessageReceiver>(messageType, receiver));
		}
		
		public static void Unsubscribe(MessageTypes messageType, IMessageReceiver receiver)
		{
			removeList.Add(new Tuple<MessageTypes, IMessageReceiver>(messageType, receiver));
		}
		
		public static void Send(MessageTypes messageType, object data)
		{
			VerifyList(messageType).ForEach(r => r.Receive(messageType, data));
		}
		
		public static void ProcessChanges()
		{
			addList.ForEach(tuple => VerifyList(tuple.Item1).Add(tuple.Item2));
			addList.Clear();

			removeList.ForEach(tuple => receivers[(int)tuple.Item1].Remove(tuple.Item2));
			removeList.Clear();
		}
		
		private static List<IMessageReceiver> VerifyList(MessageTypes messageType)
		{
			int index = (int)messageType;

			return receivers[index] ?? (receivers[index] = new List<IMessageReceiver>());
		}
	}
}
