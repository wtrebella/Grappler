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
		float speed = rigid.velocity.magnitude;
		text.text = speed.ToString("0.0");
	}
}