using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunePuzzle : MonoBehaviour
{
	public bool solved = false;
	public List<GameObject> pieces;
	public UnityEvent onSolved;

	[Space, Header("Animation")]
	public float duration = 1;
	public int numJumps = 2;
	public float jumpPower = 5;
	public Vector3 endPos;
	public Vector3 endScale;

	private void Update()
	{
		foreach (var piece in pieces)
		{
			if (!piece.activeInHierarchy)
			{
				return;
			}
		}

		if(!solved) 
			Solve();
	}

	private void Solve()
	{
		solved = true;
		onSolved?.Invoke();
		print("solved");

		transform.DOJump(endPos, jumpPower, numJumps, duration);
		transform.DOScale(endScale, duration);
	}
}
