using UnityEngine;
using System.Collections;

public class ClimberPlacer : MonoBehaviour {
	private float _placeOnMountain = 0;
	public float placeOnMountain {
		get {return _placeOnMountain;}
		set {PlaceOnMountain(value);}
	}

	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfRotationCheck = 0.05f;
	[SerializeField] private float smoothTime = 0.1f;
	[SerializeField] private float speed = 0.1f;

	private Vector3 positionVelocity;
	private float rotationVelocity;

	private void Start() {
		StartCoroutine(Climb());
	}

	private void PlaceOnMountain(float place) {
		_placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtDist(_placeOnMountain);
		float placeOnChunk = placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtDist(_placeOnMountain);
		Vector3 position = chunk.GetPositionAlongLine(placeOnChunk);//Vector3.SmoothDamp(transform.position, chunk.GetPositionAlongLine(placeOnChunk), ref positionVelocity, smoothTime);
		Vector3 rotation = new Vector3(0, 0, GetRotationAtPlace(chunk, placeOnChunk));//new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, GetRotationAtPlace(chunk, placeOnChunk), ref rotationVelocity, smoothTime));
		transform.position = position;
		transform.eulerAngles = rotation;
	}

	private float GetRotationAtPlace(MountainChunk chunk, float place) {
		float minPlace = place - sizeOfRotationCheck / 2f;
		float maxPlace = place + sizeOfRotationCheck / 2f;
		Vector2 minPosition = chunk.GetPositionAlongLine(minPlace);
		Vector2 maxPosition = chunk.GetPositionAlongLine(maxPlace);
		Vector2 vector = maxPosition - minPosition;
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;

		return angle;
	}

	private void Update() {
//		PlaceOnMountain(placeOnMountain + speed * Time.deltaTime);
	}

	private IEnumerator Climb() {
		while (true) {
			GoTween tween = new GoTween(this, 0.22f, new GoTweenConfig().floatProp("placeOnMountain", 0.02f, true).setDelay(0.22f).setEaseType(GoEaseType.SineInOut));
			Go.addTween(tween);
			tween.play();
			yield return StartCoroutine(tween.waitForCompletion());
		}
	}
}
