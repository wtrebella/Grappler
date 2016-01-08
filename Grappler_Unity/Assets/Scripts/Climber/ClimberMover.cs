using UnityEngine;
using System.Collections;

public class ClimberMover : MonoBehaviour {
	private float _placeOnMountain = 0;
	public float placeOnMountain {
		get {return _placeOnMountain;}
		set {PlaceOnMountain(value);}
	}

	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfTangentCheck = 0.01f;
	[SerializeField] private float smoothTimeRotation = 0.1f;
	[SerializeField] private Transform body;
	[SerializeField] private Transform feet;

	private float bodyRotationVelocity;
	private float feetRotationVelocity;

	private void Start() {
		StartClimbing();
	}

	public void StartClimbing() {
		StartCoroutine("Climb");
	}

	public void StopClimbing() {
		StopCoroutine("Climb");
		Go.killAllTweensWithTarget(this);
	}

	private void PlaceOnMountain(float place) {
		_placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtDist(_placeOnMountain);
		float placeOnChunk = placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtDist(_placeOnMountain);
		Vector3 position = chunk.GetPositionAlongLine(placeOnChunk);

		body.transform.localEulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(body.transform.localEulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk + 0.01f), ref bodyRotationVelocity, smoothTimeRotation));
		feet.transform.localEulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(feet.transform.localEulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk - 0.01f), ref feetRotationVelocity, smoothTimeRotation));

		transform.position = position;
	}

	private float GetTangentAtPlace(MountainChunk chunk, float place) {
		float minPlace = place - sizeOfTangentCheck / 2f;
		float maxPlace = place + sizeOfTangentCheck / 2f;
		Vector2 minPosition = chunk.GetPositionAlongLine(minPlace);
		Vector2 maxPosition = chunk.GetPositionAlongLine(maxPlace);
		Vector2 vector = maxPosition - minPosition;
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;
		return angle;
	}

	private IEnumerator Climb() {
		while (true) {
			GoTween tween = new GoTween(this, Random.Range(0.2f, 0.3f), new GoTweenConfig().floatProp("placeOnMountain", 0.02f, true).setDelay(Random.Range(0.2f, 0.3f)).setEaseType(GoEaseType.SineInOut));
			Go.addTween(tween);
			tween.play();
			yield return StartCoroutine(tween.waitForCompletion());
		}
	}
}
