using System.Collections;
using UnityEngine;
public class Disc : MonoBehaviour
{
	private const float MIN_SPEED = 0.1f;
	[SerializeField] private float _maxSpeed = 5f;
	[SerializeField] private float _maxFlightTime = 3f;
	[SerializeField] private AnimationCurve _flightDecelerationCurve;

	private void Awake()
	{
		ThrowControls.ThrowEvent += OnThrowEvent;
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

		while (currentTime > 0)
		{
			transform.localPosition += (transform.up * speed);
			currentTime -= Time.deltaTime;
			float delta = _flightDecelerationCurve.Evaluate(1 - (currentTime / flightTime));
			speed *= delta;
			Debug.Log("speed = " + speed);
			if (speed < MIN_SPEED)
			{
				yield break;
			}
			yield return null;
		}

	}

	private void OnDestroy()
	{
		ThrowControls.ThrowEvent -= OnThrowEvent;
	}
}
