using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExclamationText : MonoBehaviour {
	[SerializeField] private Text text;

	public void OnDoneShowing() {
		Destroy(gameObject);
	}

	public void SetText(string textString) {
		text.text = textString;
	}
}
