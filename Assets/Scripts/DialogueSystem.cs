using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
	public List<Dialogue> dialogues;
	public bool InProgress { get; private set; }

	public void StartDialogue(string key)
	{
		if (InProgress)
			return;

		InProgress = true;
		var root = dialogues.Find(x => x.key == key);
		if (root == null)
		{
			Debug.LogError($"{root} doesn't exist");
			return;
		}
		StartCoroutine(DoDialogue(root));
	}

	IEnumerator DoDialogue(Dialogue dialogue)
	{
		// Check if reached end
		if (dialogue.key == "stop")
		{
			Textbox.Instance.Clear();
			InProgress = false;
			yield break;
		}

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
			print(line);
		}

		// Start next dialogue
		string key = dialogue.next;
			InProgress = false;
		StartDialogue(key);
	}

	[Serializable]
	public class Dialogue
	{
		public string key;
		public string next;
		[Space, TextArea(3, 10)]
		public string text;
		public Transform speaker;
	}
}
