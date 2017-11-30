using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LudumDare40.Input.Data
{
	public enum ClickStates
	{
		Held,
		Released,
		PressedThisFrame,
		ReleasedThisFrame
	}
	
	public class MouseData
	{
		public MouseData(Vector2 screenPosition, Vector2 previousScreenPosition, Vector2 worldPosition, Vector2 previousWorldPosition,
			ClickStates leftClick, ClickStates rightClick, ClickStates middleClick, float scrollDelta)
		{
			ScreenPosition = screenPosition;
			PreviousScreenPosition = previousScreenPosition;
			WorldPosition = worldPosition;
			PreviousWorldPosition = previousWorldPosition;
			LeftClick = leftClick;
			RightClick = rightClick;
			MiddleClick = middleClick;
			ScrollDelta = scrollDelta;
		}
		
		public Vector2 ScreenPosition { get; }
		public Vector2 PreviousScreenPosition { get; }
		public Vector2 WorldPosition { get; }
		public Vector2 PreviousWorldPosition { get; }
		
		public ClickStates LeftClick { get; }
		public ClickStates RightClick { get; }
		public ClickStates MiddleClick { get; }

		public float ScrollDelta { get; }
	}
}
