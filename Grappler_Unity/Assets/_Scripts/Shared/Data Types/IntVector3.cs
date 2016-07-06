using UnityEngine;
using System.Collections;

namespace WhitDataTypes {
	public class IntVector3 {
		public int x = 0;
		public int y = 0;
		public int z = 0;

		public static IntVector3 zero {
			get {return new IntVector3(0, 0, 0);}
		}

		public IntVector3(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public IntVector3(float x, float y, float z) {
			this.x = (int)x;
			this.y = (int)y;
			this.z = (int)z;
		}

		public IntVector3() {

		}
	}
}