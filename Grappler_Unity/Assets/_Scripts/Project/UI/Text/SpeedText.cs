using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SpeedText : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;

	private Text text;

	private void Awake() {
		text = GetComponent<Text>();
	}

	private void FixedUpdate() {
		int speed = (int)rigid.velocity.magnitude;
		text.text = "Speed: " + speed.ToString();
	}
}