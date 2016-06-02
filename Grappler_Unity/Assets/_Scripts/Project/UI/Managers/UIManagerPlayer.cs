using UnityEngine;
using System.Collections;

public class UIManagerPlayer : UIManager {
	private void Start() {
		GetPanelOfType<PanelPlayer>().gameObject.SetActive(true);
	}
}
