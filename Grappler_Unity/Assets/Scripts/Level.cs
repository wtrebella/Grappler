using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CityPointMaker))]
public class Level : MonoBehaviour {
	[SerializeField] private Anchorable anchorablePrefab;

	private CityPointMaker cityPointMaker;
	private List<Anchorable> anchorables;

	private void Awake() {
		cityPointMaker = GetComponent<CityPointMaker>();
		anchorables = new List<Anchorable>();
		CreateInitialAnchorables();
	}

	private void FixedUpdate() {
		CreateAnchorablesIfNeeded();
		RemoveOffscreenAnchorables();
	}

	private void CreateInitialAnchorables() {
		while (true) {
			if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
			else break;
		}
	}

	private static int anchorableNum = 0;
	private void CreateAnchorableAtNextPoint() {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = transform;
		anchorable.transform.position = GetNextAnchorablePosition();
		anchorables.Add(anchorable);
		anchorable.name = "Anchorable " + anchorableNum.ToString();
		anchorableNum++;
		cityPointMaker.HandleCurrentPointUsed();
	}

	private Vector2 GetNextAnchorablePosition() {
		return cityPointMaker.GetCurrentPoint();
	}

	private bool NextPointIsOnScreenWithMarginX() {
		Vector3 nextPoint = GetNextAnchorablePosition();
		return GameScreen.instance.GetIsOnscreenWithMarginX(nextPoint.x);
	}

	private void CreateAnchorablesIfNeeded() {	
		if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
	}

	private void RemoveOffscreenAnchorables() {
		float minX = GameScreen.instance.lowerLeftWithMargin.x;
		while (true) {
			Anchorable anchorable = anchorables[0];
			if (anchorable.transform.position.x >= minX) break;
			
			anchorables.RemoveAt(0);
			anchorable.Recycle();
		}
	}
}
