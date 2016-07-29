using UnityEngine;
using System.Collections;

public class SignIcon : MonoBehaviour {
	public string flatTerrainSpriteName = "signIconFlat";
	public string uphillTerrainSpriteName = "signIconUphill";
	public string downhillTerrainSpriteName = "signIconDownhill";

	[SerializeField] private tk2dSprite sprite;

	public void SetTerrainFlat() {
		sprite.SetSprite(flatTerrainSpriteName);
	}

	public void SetTerrainUphill() {
		sprite.SetSprite(uphillTerrainSpriteName);
	}

	public void SetTerrainDownhill() {
		sprite.SetSprite(downhillTerrainSpriteName);
	}
}