using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ClimberPlacer))]
public class ClimberStepper : MonoBehaviour {
	[SerializeField] private float stepDistance = 0.05f;
	[SerializeField] private float moveTime = 0.3f;
	[SerializeField] private GoEaseType easeType = GoEaseType.SineInOut;

	private ClimberPlacer climberPlacer;

	public void TakeClimbStep() {
		Go.to(climberPlacer, moveTime, new GoTweenConfig().floatProp("placeOnMountain", stepDistance, true).setEaseType(easeType));
	}

	private void Awake() {
		climberPlacer = GetComponent<ClimberPlacer>();
	}
}
