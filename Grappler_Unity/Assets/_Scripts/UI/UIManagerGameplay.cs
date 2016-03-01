using UnityEngine;
using System.Collections;

public class UIManagerGameplay : UIManager {
	private void Awake() {
		BaseAwake();
		GetPanelOfType<PanelGameplay>().gameObject.SetActive(true);
	}
}
