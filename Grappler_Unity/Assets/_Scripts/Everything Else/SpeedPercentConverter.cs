using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FloatToPercentConverter))]
public class SpeedPercentConverter : MonoBehaviour {
	[SerializeField] private FloatToPercentConverter converter;
	[SerializeField] private Rigidbody2D rigid;
	
	public float GetPercent() {
		return converter.ConvertToPercent(GetSpeed());
	}

	private float GetSpeed() {
		return rigid.velocity.magnitude;
	}
}
