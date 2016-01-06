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
	[SerializeField] private float speed = 0.01f;

	private Vector3 positionVelocity;
	private float rotationVelocity;

	private void PlaceOnMountain(float place) {
		_placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtDist(_placeOnMountain);
		float placeOnChunk = placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtDist(_placeOnMountain);
		Vector3 position = Vector3.SmoothDamp(transform.position, chunk.GetPositionAlongLine(placeOnChunk), ref positionVelocity, smoothTime);
		Vector3 rotation = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, GetRotationAtPlace(chunk, placeOnChunk), ref rotationVelocity, smoothTime));
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

	void Start() {
		Go.to(this, 5, new GoTweenConfig().floatProp("placeOnMountain", 3));

		StartCoroutine(Blah());
	}

	private IEnumerator Blah() {
		yield return new WaitForSeconds(1);

		Go.to(this, 10, new GoTweenConfig().floatProp("placeOnMountain", 0));
	}
}
