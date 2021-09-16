using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiscVisualiser : DiscBase
{
	private List<Vector3> _positions = new List<Vector3>();
	private float _fakeFrameTime = 0.06f;

	private void Reset()
	{
		_currentPower = _power;
		_positions.Clear();
		_positions.Add(transform.position);
	}

	private void OnDrawGizmos()
	{
		while(_currentPower > 0)
		{
			Vector3 lastPos = _positions[_positions.Count - 1];
			Vector3 pos = lastPos + (_currentPower * _speed * transform.forward);
			
			pos += transform.right * GetTurn(_currentPower);
			pos += transform.right * GetFade(_currentPower);

			_positions.Add(pos);
			_currentPower -= _fakeFrameTime;
		}
		{
			if(_positions != null && _positions.Count > 0)
			{
				for (int i = 1; i < _positions.Count; i++)
				{
					Gizmos.DrawLine(_positions[i - 1], _positions[i]);
				}
			}

			Reset();
		}
	}
}
