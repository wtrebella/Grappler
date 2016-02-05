using UnityEngine;
using System.Collections;
using System;

public class GeneratableItem : MonoBehaviour {
	public virtual void GenerationComplete(Generator generator) {
		
	}

	public T To<T>() {
		return (T)(object)this;
	}
}
