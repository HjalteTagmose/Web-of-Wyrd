using System.Collections;
using TMPro;
using UnityEngine;

public class Textbox : Singleton<Textbox>
{
	public float speed = .01f;
	private TextMeshPro text;

	protected override void Awake()
	{
		base.Awake();
		text = GetComponent<TextMeshPro>();
	}

	public IEnumerator WriteText(string msg)
	{
		// Clear
		text.text = "";

		// Write
		for (int i = 0; i < msg.Length; i++)
		{
			text.text += msg[i];
			yield return new WaitForSeconds(speed);
		}
	}
}
