using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combiner : Singleton<Combiner>
{
	public List<Combination> combinations;
	
	public bool TryGetCombination(Combinable self, Combinable other, out Combination combination)
	{
		Func<Combination, bool> canCombine = (c => c.a == self && c.b == other || c.a == other && c.b == self);
		combination = combinations.FirstOrDefault(canCombine);
		return combination != null;
	}

	[Serializable]
	public class Combination
	{
		public Combinable a;
		public Combinable b;
		public GameObject result;
		[Space]
		public Transform spawnUnder;

		public void Combine()
		{
			Debug.Log("Combining " + a.name + " and " + b.name);
			Instantiate(result, spawnUnder);
			Destroy(a.gameObject);
			Destroy(b.gameObject);
		}
	}
}
