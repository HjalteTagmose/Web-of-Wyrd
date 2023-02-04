using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
	[Range(0f, 1f)] public float parallaxSpeedX = 0.5f;
	[Range(0f, 1f)] public float parallaxSpeedY = 0.5f;

	private Transform cam;
	private Vector3 prevCamPos;

	void Start()
	{
		cam = Camera.main.transform;
		prevCamPos = cam.position;
	}

	void Update()
	{
		float parallaxX = (prevCamPos.x - cam.position.x) * parallaxSpeedX;
		float parallaxY = (prevCamPos.y - cam.position.y) * parallaxSpeedY;
		transform.position = new Vector3(transform.position.x + parallaxX, transform.position.y + parallaxY, transform.position.z);
		prevCamPos = cam.position;
	}
}
