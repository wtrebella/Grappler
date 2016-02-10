using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboText : MonoBehaviour {
	[SerializeField] private Text text;

	public void OnComboChanged(int combo) {
		text.text = "Combo: " + combo.ToString();
	}
}
