using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Textbox : Singleton<Textbox>
{
	public Vector2 offset;
	public float holdTime = 2;
	public float speed = .1f;
	private TextMeshPro text;
	private float waited = 0;
	private Transform target;
	private bool requestSkip;

	protected override void Awake()
	{
		base.Awake();
		text = GetComponent<TextMeshPro>();
		Clear();
	}

	private void Update()
	{
		if (target != null)
		{
			transform.position = target.position + (Vector3)offset;
		}
		if (Input.anyKeyDown)
		{
			requestSkip = true;
		}
	}

	public IEnumerator WriteText(string msg)
	{
		Clear();
		for (int i = 0; i < msg.Length; i++)
		{
			text.text += msg[i];
			yield return new WaitForSeconds(speed);

			if (requestSkip)
			{
				Skip(msg);
				break;
			}
		}
		while (waited < holdTime)
		{
			waited += Time.deltaTime;
			yield return null;

			if (requestSkip)
			{
				Skip();
				break;
			}
		}
	}
	
	private void Skip(string msg = "")
	{
		text.text = msg;
		requestSkip = false;
		waited = -2;
	}

	public void Clear()
	{
		waited = 0;
		text.text = "";
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
