using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
	public Sprite normal, hover, click;
	private SpriteRenderer sr;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	private void Update()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(mousePos.x, mousePos.y, 0);

		bool mouseDown = Input.GetMouseButton(0);
		bool hovering  = Physics2D.OverlapPointAll(transform.position)
								  .Where(o => o.GetComponent<Interactable>() != null)
								  .Count() > 0;
		if (mouseDown)
			sr.sprite = click;
		else if (hovering)
			sr.sprite = hover;
		else 
			sr.sprite = normal;
	}
}
