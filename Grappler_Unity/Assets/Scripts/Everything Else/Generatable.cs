using UnityEngine;
using System.Collections;
using System;

public class Generatable : MonoBehaviour {
	public Action<Generator, int> SignalSpawnComplete;
	public Action<Generator> SignalNeedsDelete;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void GenerationComplete(Generator generator, int side) {
		if (SignalSpawnComplete != null) SignalSpawnComplete(generator, side);
	}
	
	public void NeedsDelete(Generator spawner) {
		if (SignalNeedsDelete != null) SignalNeedsDelete(spawner);
		else Destroy(this.gameObject);
	}
}
