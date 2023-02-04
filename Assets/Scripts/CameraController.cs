using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 division;
	private float offsetZ = -10;

	private void Awake()
	{
		offsetZ = transform.position.z;
	}

	void Update()
	{
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 pos = mousePos / division;
		pos.z = offsetZ;
		transform.position = pos;
    }
}
