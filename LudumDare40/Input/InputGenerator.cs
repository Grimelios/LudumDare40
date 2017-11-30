using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumDare40.Input.Data;
using LudumDare40.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LudumDare40.Input
{
	public class InputGenerator
	{
		private KeyboardState oldKS;
		private KeyboardState newKS;
		private MouseState oldMS;
		private MouseState newMS;
		private Camera camera;

		// Computing world position requires using the camera's view transform. As such, it's easier to store the previous position here
		// rather than recomputing the value each frame.
		private Vector2 previousWorldPosition;
		
		public InputGenerator(Camera camera)
		{
			this.camera = camera;
		}
		
		public void GenerateEvents()
		{
			MessageSystem.Send(MessageTypes.Keyboard, GenerateKeyboardData());
			MessageSystem.Send(MessageTypes.Mouse, GenerateMouseData());
		}
		
		private KeyboardData GenerateKeyboardData()
		{
			oldKS = newKS;
			newKS = Keyboard.GetState();

			List<Keys> oldKeysDown = new List<Keys>(oldKS.GetPressedKeys());
			List<Keys> newKeysDown = new List<Keys>(newKS.GetPressedKeys());
			List<Keys> keysPressedThisFrame = newKeysDown.Except(oldKeysDown).ToList();
			List<Keys> keysReleasedThisFrame = oldKeysDown.Except(newKeysDown).ToList();

			return new KeyboardData(newKeysDown, keysPressedThisFrame, keysReleasedThisFrame);
		}
		
		private MouseData GenerateMouseData()
		{
			oldMS = newMS;
			newMS = Mouse.GetState();

			Vector2 screenPosition = new Vector2(newMS.X, newMS.Y);
			Vector2 previousScreenPosition = new Vector2(oldMS.X, oldMS.Y);
			Vector2 worldPosition = Vector2.Transform(screenPosition, Matrix.Invert(camera.Transform));

			ClickStates leftClick = GetClickState(oldMS.LeftButton, newMS.LeftButton);
			ClickStates rightClick = GetClickState(oldMS.RightButton, newMS.RightButton);
			ClickStates middleClick = GetClickState(oldMS.MiddleButton, newMS.MiddleButton);

			float scrollDelta = newMS.ScrollWheelValue - oldMS.ScrollWheelValue;

			MouseData data = new MouseData(screenPosition, previousScreenPosition, worldPosition, previousWorldPosition, leftClick,
				rightClick, middleClick, scrollDelta);

			previousWorldPosition = worldPosition;

			return data;
		}
		
		private ClickStates GetClickState(ButtonState oldState, ButtonState newState)
		{
			if (oldState == newState)
			{
				return newState == ButtonState.Pressed ? ClickStates.Held : ClickStates.Released;
			}

			return newState == ButtonState.Pressed ? ClickStates.PressedThisFrame : ClickStates.ReleasedThisFrame;
		}
	}
}
