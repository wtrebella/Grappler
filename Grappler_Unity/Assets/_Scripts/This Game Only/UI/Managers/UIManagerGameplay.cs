using UnityEngine;
using System.Collections;

public class UIManagerGameplay : UIManager {
	private void Start() {
		GetPanelOfType<PanelGameplay>().gameObject.SetActive(true);
	}
}
