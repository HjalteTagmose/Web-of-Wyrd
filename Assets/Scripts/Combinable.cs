using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Combinable : Interactable
{
	private SpriteRenderer sr;
	private int initSortOrder;
	private int initSortLayer;
	private Vector3 originalPos;

	private void Awake()
	{
		originalPos = transform.localPosition;
		sr = GetComponent<SpriteRenderer>();
		if (sr) initSortOrder = sr.sortingOrder;
		if (sr) initSortLayer = sr.sortingLayerID;
	}

	public void OnMouseDrag()
	{
		if (sr) sr.sortingLayerID = SortingLayer.NameToID("UI");
		Debug.Log("Mouse dragging " + gameObject.name);
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		transform.position = mousePos;
	}

	public override void OnMouseUp()
	{
		if (sr) sr.sortingLayerID = initSortLayer;
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

		transform.DOLocalMove(originalPos, 1);
	}
}
