using UnityEngine;
using System.Collections;

public class IntVector2 {
	public int x = 0;
	public int y = 0;

	public static IntVector2 zero {
		get {return new IntVector2(0, 0);}
	}
	
	public IntVector2(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public IntVector2(float x, float y) {
		this.x = (int)x;
		this.y = (int)y;
	}

	public IntVector2() {

	}
}
