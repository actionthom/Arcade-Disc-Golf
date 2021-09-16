using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public abstract class DiscBase : MonoBehaviour
{
	[Range(0f, 1.5f)]
	[SerializeField] protected float _power;
	
	[Range(0, 13)]
	[SerializeField] protected int _speed;
	
	[Range(0, 7)]
	[SerializeField] protected int _glide;
	
	[Range(-4, 0)]
	[SerializeField] protected int _turn;
	
	[Range(0, 4)]
	[SerializeField] protected int _fade;
	
	[Range(1, 10)]
	[SerializeField] protected float _fadeModifier = 2f;
	
	protected float _currentPower;

	protected virtual float GetTurn(float delta)
	{
		return -_turn * Easing.OutPower(delta, 10);
	}

	protected virtual float GetFade(float delta)
	{
		return -_fade * _fadeModifier * (1 -delta);
	}
}
