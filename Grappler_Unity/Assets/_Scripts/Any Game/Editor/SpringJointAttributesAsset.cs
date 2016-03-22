using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpringJointAttributesAsset {
	
	[MenuItem("Assets/Create/SpringJointAttributes")]
	public static void Create() {
		ScriptableObjectUtility.CreateAsset<SpringJointAttributes>();
	}
}
