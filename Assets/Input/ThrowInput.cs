using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowInput : MonoBehaviour
{
	private Mouse _currentMouse;
	private Vector2 _startPos;
	[SerializeField] private float _maxDistance = 4000f;

	public static event Action<float> ThrowEvent;
	public static event Action<Vector3> TargetSetEvent;
	public static event Action<Vector3> ReleaseAngleSetEvent;

	private void Update()
	{
		ListenForMouseInput();
	}

	private void ListenForMouseInput()
	{
		if (Mouse.current == null)
		{
			return;
		}

		_currentMouse = Mouse.current;

		if (_currentMouse.leftButton.wasPressedThisFrame)
		{
			Debug.Log("Left Button Pressed");
			_startPos = _currentMouse.position.ReadValue();
			TargetSetEvent?.Invoke(GetMouseWorldPos(_startPos));
		}
		else if(_currentMouse.leftButton.isPressed)
		{
			ReleaseAngleSetEvent?.Invoke(GetMouseWorldPos(_currentMouse.position.ReadValue()));
		}
		else if (_currentMouse.leftButton.wasReleasedThisFrame && _startPos != Vector2.zero)
		{
			float distance = Mathf.Clamp((_currentMouse.position.ReadValue() - _startPos).sqrMagnitude, 0, _maxDistance);
			_startPos = Vector2.zero;
			if (distance > 0)
			{
				ThrowEvent?.Invoke(distance / _maxDistance);
			}
			Debug.Log("Released with distance of " + (distance / _maxDistance));
		}
	}

	private Vector3 GetMouseWorldPos(Vector2 mousePos)
	{
		Camera cam = Camera.main;
		return cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -10)); //todo - set this from the Plane Distance in the Canvas
	}
}
