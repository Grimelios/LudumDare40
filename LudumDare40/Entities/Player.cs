using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using LudumDare40.Animations;
using LudumDare40.Entities.Core;
using LudumDare40.Input.Data;
using LudumDare40.Interfaces;
using LudumDare40.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LudumDare40.Entities
{
	public class Player : LivingEntity, IMessageReceiver
	{
		private const int PlayerWidth = 48;
		private const int PlayerHeight = 96;

		private PlayerData playerData;

		public Player() : base(EntityTypes.Player)
		{
			LoadAnimations("PlayerAnimations.json");
			PlayAnimation("Idle");

			playerData = new PlayerData();

			Body = PhysicsFactory.CreateRectangle(PlayerWidth, PlayerHeight, Units.Pixels, BodyType.Dynamic, this);
			Body.Friction = 0;
			Body.ManuallyControlled = true;
			Body.MaximumSpeed = new Vector2(PhysicsConvert.ToMeters(playerData.MaxSpeed), float.PositiveInfinity);

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
			bool aDown = false;
			bool dDown = false;

			foreach (Keys key in data.KeysDown)
			{
				switch (key)
				{
					case Keys.A:
						aDown = true;
						break;

					case Keys.D:
						dDown = true;
						break;
				}
			}

			if (aDown ^ dDown)
			{
				int multiplier = aDown ? -1 : 1;

				Body.Accelerating = true;
				Body.Decelerating = false;
				Body.ApplyRawForce(new Vector2(PhysicsConvert.ToMeters(playerData.Acceleration * multiplier), 0));

				FlipHorizontally = aDown;
			}
			else if (Math.Abs(Body.LinearVelocity.X) > 0)
			{
				int multiplier = Body.LinearVelocity.X > 0 ? -1 : 1;

				Body.Accelerating = false;
				Body.Decelerating = true;
				Body.ApplyRawForce(new Vector2(PhysicsConvert.ToMeters(playerData.Deceleration * multiplier), 0));
			}
			else
			{
				Body.Accelerating = false;
				Body.Decelerating = false;
			}
		}

		private void HandleMouse(MouseData data)
		{
		}
	}
}
