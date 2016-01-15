using UnityEngine;
using System.Collections;

public class ClimbingState : MonoBehaviour {
	private float _placeOnMountain = 0;
	public float placeOnMountain {
		get {return _placeOnMountain;}
		set {PlaceOnMountain(value);}
	}

	[SerializeField] private Player player;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfTangentCheck = 0.01f;
	[SerializeField] private float smoothTimeRotation = 0.1f;
	[SerializeField] private float climbStepDuration = 0.2f;
	[SerializeField] private Transform body;
	[SerializeField] private Transform feet;

	private Vector2 initialBodyLocalPosition;
	private float bodyRotationVelocity;
	private float feetRotationVelocity;
	private bool isClimbing = false;

	public void StartClimbing() {
		if (isClimbing) return;
		PlaceOnMountain(0);
		StartCoroutine("Climb");
		isClimbing = true;
	}

	public void StartClimbing(float y) {
		if (isClimbing) return;
		PlaceOnMountainBasedOnY(y);
		StartCoroutine("Climb");
		isClimbing = true;
	}

	public void StopClimbing() {
		if (!isClimbing) return;

		StopCoroutine("Climb");
		Go.killAllTweensWithTarget(this);
		isClimbing = false;
	}

	private void Awake() {
		initialBodyLocalPosition = body.localPosition;
	}

	private void PlaceOnMountain(float place) {
		_placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtPlace(_placeOnMountain);
		float placeOnChunk = _placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtPlace(_placeOnMountain);
		Vector3 position = chunk.GetPositionFromPlace(placeOnChunk);
		SetBodyRotations(chunk, placeOnChunk);
		player.transform.position = position;
	}

	private void PlaceOnMountainBasedOnY(float y) {
		int chunkIndex = mountainChunkGenerator.GetMountainChunkIndexAtY(y);
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunk(chunkIndex);
		float place = chunk.GetPlaceAtY(y);
		_placeOnMountain = place + chunkIndex;
		Vector3 position = chunk.GetPositionFromPlace(place);
		SetBodyRotations(chunk, place);
		player.transform.position = position;
		body.transform.localPosition = initialBodyLocalPosition;
	}

	private void SetBodyRotations(MountainChunk chunk, float placeOnChunk) {
		body.transform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(body.transform.eulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk + 0.01f), ref bodyRotationVelocity, smoothTimeRotation));
		feet.transform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(feet.transform.eulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk - 0.01f), ref feetRotationVelocity, smoothTimeRotation));
	}

	private float GetTangentAtPlace(MountainChunk chunk, float place) {
		float minPlace = place - sizeOfTangentCheck / 2f;
		float maxPlace = place + sizeOfTangentCheck / 2f;
		Vector2 minPosition = chunk.GetPositionFromPlace(minPlace);
		Vector2 maxPosition = chunk.GetPositionFromPlace(maxPlace);
		Vector2 vector = maxPosition - minPosition;
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;
		return angle;
	}

	private IEnumerator Climb() {
		while (true) {
			GoTween tween = new GoTween(this, climbStepDuration, new GoTweenConfig().floatProp("placeOnMountain", 0.02f, true).setDelay(Random.Range(0.2f, 0.3f)).setEaseType(GoEaseType.SineInOut));
			Go.addTween(tween);
			tween.play();
			yield return StartCoroutine(tween.waitForCompletion());
		}
	}
}
