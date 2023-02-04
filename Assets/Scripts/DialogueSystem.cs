using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
	public bool InProgress { get; private set; }
	
	public List<Dialogue> intro;
	private Dictionary<string, List<Dialogue>> dialogues;

	protected override void Awake()
	{
		base.Awake();
		dialogues = new Dictionary<string, List<Dialogue>>
		{
			{ "intro", intro }
		};
	}

	public void StartDialogue(string key, float delay = 0)
	{
		if (InProgress)
			return;

		key = key.ToLower();
		bool success = dialogues.TryGetValue(key, out var dialogue);
		if (!success)
		{
			Debug.LogError($"{key} doesn't exist");
			return;
		}
		
		InProgress = true;
		StartCoroutine(DoDialogues(dialogue, delay));
	}

	IEnumerator DoDialogues(List<Dialogue> dialogues, float delay = 0)
	{
		yield return new WaitForSeconds(delay);
		foreach (var dialogue in dialogues)
		{
			yield return StartCoroutine(DoDialogue(dialogue));
		}
	}

	IEnumerator DoDialogue(Dialogue dialogue)
	{
		// Update position
		if (dialogue.speaker != null)
			Textbox.Instance.transform.position = dialogue.speaker.position + Vector3.up;

		// Write lines
		string[] lines = dialogue.text.Split('.');
		foreach (var line in lines)
		{
			if (string.IsNullOrEmpty(line))
				continue;

			yield return StartCoroutine(Textbox.Instance.WriteText(line));
		}
	}

	[Serializable]
	public class Dialogue
	{
		public Transform speaker;
		[TextArea(3, 10)]
		public string text;
	}
}
