using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
	public List<Dialogue> dialogues;

	public void StartDialogue(string key)
	{
		var root = dialogues.Find(x => x.key == key);
		print(root.text);

		StartCoroutine(DoDialogue(root));
	}

	IEnumerator DoDialogue(Dialogue dialogue)
	{
		// Start writing
		StopAllCoroutines();
		var write = Textbox.Instance.WriteText(dialogue.text);
		yield return StartCoroutine(write);

		// Start next dialogue
		string key = dialogue.next;
		StartDialogue(key);
	}

	[Serializable]
	public class Dialogue
	{
		public string key;
		public string next;
		[Space, TextArea]
		public string text;
		public Transform speaker;
	}
}
