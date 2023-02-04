using System.Linq;
using UnityEngine;

public class Combinable : Interactable
{
	private SpriteRenderer sr;
	private int initSortOrder;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		initSortOrder = sr.sortingOrder;
	}

	public void OnMouseDrag()
	{
		Debug.Log("Mouse dragging " + gameObject.name);
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		transform.position = mousePos;
		sr.sortingOrder = 1000;
	}

	public override void OnMouseUp()
	{
		sr.sortingOrder = initSortOrder;
		Debug.Log("Mouse up on " + gameObject.name);
		var others = Physics2D.OverlapCircleAll(transform.position, 0.5f)
							  .Where( o => o.GetComponent<Combinable>() != null)
							  .Select(o => o.GetComponent<Combinable>())
							  .ToList();
		
		foreach (var other in others)
		{
			if (other == this) 
				continue;

			bool canCombine = Combiner.Instance.TryGetCombination(this, other, out var combination);
			if (canCombine)
			{
				combination.Combine();
				return;
			}
		}
	}
}
