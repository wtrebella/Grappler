using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour {
	public static void SetQuality(string suffix) {
		var images = FindObjectsOfType<Image>();

		foreach (Image image in images) {
			string name = "UITest/";
			name += image.sprite.name.Split('@')[0];
			name += "@" + suffix;
			var sprite = Resources.Load<Sprite>(name);
			if (sprite) image.sprite = sprite;
		}

		Resources.UnloadUnusedAssets();
	}
}
