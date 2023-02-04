using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
	public Vector2 highestPoint;
    public float duration = 5;

	void Start()
    {
		transform.DOLocalMove(highestPoint, duration)
				 .SetEase(Ease.InOutSine)
				 .SetLoops(-1, LoopType.Yoyo);
	}
}
