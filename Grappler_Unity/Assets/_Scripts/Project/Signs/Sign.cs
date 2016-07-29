using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {
	[SerializeField] private SignIcon icon;
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

	public void SetIcon(TerrainSetType terrainSetType) {
		switch (terrainSetType) {
		case TerrainSetType.Flat:
			icon.SetTerrainFlat();
			break;
		case TerrainSetType.Uphill:
			icon.SetTerrainUphill();
			break;
		case TerrainSetType.Downhill:
			icon.SetTerrainDownhill();
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
}