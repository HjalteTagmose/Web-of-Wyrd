using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	public UnityEvent onInteract;

	public void OnMouseEnter()
	{
		Debug.Log("Mouse entered " + gameObject.name);
	}

	public void OnMouseDown()
	{
		Debug.Log("Mouse down on " + gameObject.name);
	}

	public virtual void OnMouseUp()
	{
		Debug.Log("Mouse up on " + gameObject.name);
		Interact();
	}

	private void Interact()
	{
		onInteract?.Invoke();
	}
}
