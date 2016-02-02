using UnityEngine;
using System.Collections;

public class Initializations : MonoBehaviour {
	void Awake() {
		Application.targetFrameRate = 60;
		Go.defaultEaseType = GoEaseType.SineInOut;
		Go.duplicatePropertyRule = GoDuplicatePropertyRuleType.RemoveRunningProperty;
		Go.logLevel = GoLogLevel.Error;
		ChangeQuality.SetQuality("HD");
	}
}
