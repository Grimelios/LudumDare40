using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Animations;
using LudumDare40.Entities.Core;
using LudumDare40.Input.Data;
using LudumDare40.Interfaces;

namespace LudumDare40.Entities
{
	public class Player : LivingEntity, IMessageReceiver
	{
		public Player() : base(EntityTypes.Player)
		{
			LoadAnimations("PlayerAnimations.json");
			PlayAnimation("Idle");

			MessageSystem.Subscribe(MessageTypes.Keyboard, this);
			MessageSystem.Subscribe(MessageTypes.Mouse, this);
		}

		public void Receive(MessageTypes messageType, object data)
		{
			switch (messageType)
			{
				case MessageTypes.Keyboard:
					HandleKeyboard((KeyboardData)data);
					break;

				case MessageTypes.Mouse:
					HandleMouse((MouseData)data);
					break;
			}
		}

		private void HandleKeyboard(KeyboardData data)
		{
		}

		private void HandleMouse(MouseData data)
		{
		}
	}
}
