using UnityEngine;
using System.Collections;

public class UIManagerTitle : UIManager {
	private void Start() {
		GetPanelOfType<PanelTitle>().gameObject.SetActive(true);
	}
}
