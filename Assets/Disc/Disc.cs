using System.Collections;
using UnityEngine;
public class Disc : MonoBehaviour
{
	[SerializeField] private float _maxSpeed = 5f;

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
		float speed = _maxSpeed *= power;

		while (speed > 0.05f)
		{
			transform.localPosition += (transform.up * speed);
			speed = speed * 0.95f;
			yield return null;
		}

	}

	private void OnDestroy()
	{
		ThrowControls.ThrowEvent -= OnThrowEvent;
	}
}
