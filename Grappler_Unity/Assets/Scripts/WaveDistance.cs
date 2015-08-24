using UnityEngine;
using System.Collections;

public class WaveDistance : MonoBehaviour {
	[SerializeField] private DeltaDistanceMeasure deltaDistanceMeasure;
	[SerializeField] private UILabel deltaDistanceLabel;
	
	public void HandleDeltaDistanceChanged(float deltaDistance) {
		SetLabel(deltaDistance);
	}
	
	private void Awake() {
		deltaDistanceMeasure.SignalDeltaDistanceChanged += HandleDeltaDistanceChanged;
	}
	
	private void SetLabel(float deltaDistance) {
		float gameUnitDistance = WhitTools.UnityUnitsToGameUnits * deltaDistance;
		gameUnitDistance = Mathf.Max(0, gameUnitDistance);
		deltaDistanceLabel.text = gameUnitDistance.ToString("0.0") + "m";
	}
}
