using UnityEngine;
using System.Collections;
using System;

public class Generatable : MonoBehaviour {
	public Action<AreaGenerator, int> SignalSpawnComplete;
	public Action<AreaGenerator> SignalNeedsDelete;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void GenerationComplete(AreaGenerator generator, int side) {
		if (SignalSpawnComplete != null) SignalSpawnComplete(generator, side);
	}
	
	public void NeedsDelete(AreaGenerator spawner) {
		if (SignalNeedsDelete != null) SignalNeedsDelete(spawner);
		else Destroy(this.gameObject);
	}
}
