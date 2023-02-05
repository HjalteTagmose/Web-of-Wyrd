using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : Singleton<DialogueSystem>
{
	public bool InProgress { get; private set; }

	public List<Dialogue> intro;
	public List<Dialogue> plantTalk;
	public List<Dialogue> spindleRuneTalk;
	public List<Dialogue> finalTalk;
	private Dictionary<string, List<Dialogue>> dialogues;

	protected override void Awake()
	{
		base.Awake();
		dialogues = new Dictionary<string, List<Dialogue>>
		{
			{ "intro", intro },
			{ "planttalk", plantTalk },
			{ "spindlerunetalk", spindleRuneTalk },
			{ "finaltalk", finalTalk },
		};
	}

	public void StartDialogue(string key)
	{
		StartDialogue(key, 0);
	}

	public void StartDialogue(string key, float delay = 0)
	{
		key = key.ToLower();
		bool success = dialogues.TryGetValue(key, out var dialogue);
		if (!success)
		{
			Debug.LogError($"{key} doesn't exist");
			return;
		}
		
		if (InProgress)
			StopAllCoroutines();

		dialogues.Remove(key);
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
		Textbox.Instance.Clear();
	}

	IEnumerator DoDialogue(Dialogue dialogue)
	{
		// Update target
		Textbox.Instance.SetTarget(dialogue.speaker);

		// Do start event
		dialogue.onStartSpeak?.Invoke();

		// Write lines
		string[] lines = dialogue.text.Split('.');
		foreach (var line in lines)
		{
			string trimmedLine = line.Trim();
			if (string.IsNullOrEmpty(trimmedLine))
				continue;

			yield return StartCoroutine(Textbox.Instance.WriteText(trimmedLine));
		}
	}

	[Serializable]
	public class Dialogue
	{
		public Transform speaker;
		[TextArea(3, 10)]
		public string text;
		public UnityEvent onStartSpeak;
	}
}
