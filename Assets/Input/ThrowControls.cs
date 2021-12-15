using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowControls : MonoBehaviour
{
	private Mouse _currentMouse;
	private Vector2 _startPos;
	[SerializeField] private float _maxDistance = 4000f;
	public static event Action<float> ThrowEvent;

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
}
