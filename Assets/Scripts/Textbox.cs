using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Textbox : Singleton<Textbox>
{
	public float holdTime = 2;
	public float speed = .1f;
	private TextMeshPro text;
	private float waited = 0;

	protected override void Awake()
	{
		base.Awake();
		text = GetComponent<TextMeshPro>();
		Clear();
	}

	public IEnumerator WriteText(string msg)
	{
		Clear();
		for (int i = 0; i < msg.Length; i++)
		{
			text.text += msg[i];
			yield return new WaitForSeconds(speed);
		}
		while (waited < holdTime)
		{
			waited += Time.deltaTime;
			if (!Input.anyKeyDown)
				yield return null;
		}
	}

	public void Clear()
	{
		waited = 0;
		text.text = "";
	}
}
