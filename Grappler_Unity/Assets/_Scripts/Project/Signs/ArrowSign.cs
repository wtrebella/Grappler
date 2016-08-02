using UnityEngine;
using System.Collections;

public class ArrowSign : MonoBehaviour {
	[SerializeField] private Transform arrow;
	[SerializeField] private Transform signPost;
	[SerializeField] private Transform signPostAnchorCeiling;
	[SerializeField] private Transform signPostAnchorFloor;
	[SerializeField] private Transform signContents;
	[SerializeField] private Transform signAnchorCeiling;
	[SerializeField] private Transform signAnchorFloor;

	public void SetAsFloorSign() {
		SetSignPostFloor();
		SetSignContentsFloor();
	}

	public void SetAsCeilingSign() {
		SetSignPostCeiling();
		SetSignContentsCeiling();
	}

	public void SetArrowDirection(TerrainSetType terrainSetType) {
		switch (terrainSetType) {
		case TerrainSetType.Flat:
			SetArrowFlat();
			break;
		case TerrainSetType.Uphill:
			SetArrowUp();
			break;
		case TerrainSetType.Downhill:
			SetArrowDown();
			break;
		default:
			Debug.LogWarning("invalid terrain set type: " + terrainSetType.ToString());
			break;
		}
	}

	private void SetSignPostCeiling() {
		signPost.SetParent(signPostAnchorCeiling);
		signPost.localPosition = Vector2.zero;
		signPost.localRotation = Quaternion.identity;
	}

	private void SetSignPostFloor() {
		signPost.SetParent(signPostAnchorFloor);
		signPost.localPosition = Vector2.zero;
		signPost.localRotation = Quaternion.identity;
	}

	private void SetSignContentsCeiling() {
		signContents.SetParent(signAnchorCeiling);
		signContents.localPosition = Vector2.zero;
	}

	private void SetSignContentsFloor() {
		signContents.SetParent(signAnchorFloor);
		signContents.localPosition = Vector2.zero;
	}

	private void SetArrowFlat() {
		arrow.transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	private void SetArrowUp() {
		arrow.transform.localEulerAngles = new Vector3(0, 0, 45);
	}

	private void SetArrowDown() {
		arrow.transform.localEulerAngles = new Vector3(0, 0, -45);
	}
}