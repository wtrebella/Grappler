using UnityEngine;
using System.Collections;

public class Initializations : MonoBehaviour {
	void Awake() {
		Application.targetFrameRate = 60;
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.0333f;
		Go.defaultEaseType = GoEaseType.SineInOut;
		Go.duplicatePropertyRule = GoDuplicatePropertyRuleType.RemoveRunningProperty;
		Go.logLevel = GoLogLevel.Error;
		ChangeQuality.SetQuality("HD");
	}
}
