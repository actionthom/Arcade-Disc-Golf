using System;
using System.Collections;
using UnityEngine;
public class Disc : MonoBehaviour
{
	private const float MIN_SPEED = 0.1f;
	[SerializeField] private float _maxSpeed = 5f;
	[SerializeField] private float _maxFlightTime = 3f;
	[SerializeField] private AnimationCurve _flightDecelerationCurve;

	private Vector3 _targetPoint = Vector3.zero;

	private void Awake()
	{
		ThrowInput.ThrowEvent += OnThrowEvent;
		ThrowInput.TargetSetEvent += OnTargetSet;
		ThrowInput.ReleaseAngleSetEvent += OnReleaseAngleSet;
	}

	private void OnReleaseAngleSet(Vector3 mousePos)
	{
		mousePos.z = transform.position.z;
		transform.up = (mousePos - transform.position).normalized;
	}

	private void OnTargetSet(Vector3 targetPosition)
	{
		targetPosition.z = transform.position.z;
		_targetPoint = targetPosition;
	}

	private void OnThrowEvent(float power)
	{
		StartCoroutine(Throw(power));
	}

	private IEnumerator Throw(float power)
	{
		float speed = _maxSpeed * power;
		float flightTime = _maxFlightTime * power;
		float currentTime = flightTime;
		Vector3 startDirection = transform.up;
		Vector3 reflectedPosition = Vector3.Reflect(startDirection, (transform.position - _targetPoint).normalized);
		Vector3 endDirection = (reflectedPosition - transform.position).normalized;

		while (currentTime > 0)
		{
			float timeDelta = 1 - (currentTime / flightTime);
			transform.up = Vector3.Lerp(startDirection, endDirection, timeDelta);
			transform.localPosition += (transform.up * speed);
			currentTime -= Time.deltaTime;
	
			speed *= _flightDecelerationCurve.Evaluate(timeDelta);
			if (speed < MIN_SPEED)
			{
				yield break;
			}
			yield return null;
		}

	}

	private void OnDestroy()
	{
		ThrowInput.ThrowEvent -= OnThrowEvent;
		ThrowInput.TargetSetEvent -= OnTargetSet;
		ThrowInput.ReleaseAngleSetEvent -= OnReleaseAngleSet;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawSphere(_targetPoint, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, _targetPoint);
		Gizmos.color = Color.blue;
		Vector3 lineTarget = transform.position + (transform.up * 50);
		Gizmos.DrawLine(transform.position, lineTarget);
	}
}
