using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare40.Interfaces
{
	public enum MessageTypes
	{
		Keyboard,
		Mouse,
		Exit
	}
	
	public interface IMessageReceiver
	{
		void Receive(MessageTypes messageType, object data);
	}
}
