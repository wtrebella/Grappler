using UnityEngine;
using System.Collections;

public class Climber : MonoBehaviour {
	private float _placeOnMountain = 0;
	public float placeOnMountain {
		get {return _placeOnMountain;}
		set {PlaceOnMountain(value);}
	}

	[SerializeField] private Player player;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfTangentCheck = 0.01f;
	[SerializeField] private float smoothTimeRotation = 0.1f;
	[SerializeField] private Transform body;
	[SerializeField] private Transform feet;

	private float bodyRotationVelocity;
	private float feetRotationVelocity;
	private bool isClimbing = false;

	public void StartClimbing() {
		if (isClimbing) return;
		PlaceOnMountain(0);//GetPlaceNearestPoint(transform.position));
		StartCoroutine("Climb");
		isClimbing = true;
	}

	public void StopClimbing() {
		if (!isClimbing) return;

		StopCoroutine("Climb");
		Go.killAllTweensWithTarget(this);
		isClimbing = false;
	}

//	private float GetPlaceNearestPoint(MountainChunk mountainChunk, Vector2 point) {
//		float goalPlace;
//		Anchorable anchorable;
//		if (anchorableFinder.FindAnchorableInCircle(out anchorable)) {
//			// 1. get the Point of the anchorable
//			Point linePoint = anchorable.linePoint;
//			
//			// 2. find out if it's this Point and the NEXT or the PREVIOUS
//			Point pointA = null;
//			Point pointB = null;
//			if (linePoint.y > point.y) {
//				int linePointIndex = mountainChunk.GetIndexOfLinePoint(linePoint);
//				if (linePointIndex == 0) return 0;
//				else {
//					pointA = mountainChunk.GetLinePoint(linePointIndex - 1);
//					pointB = linePoint;
//				}
//			}
//			else if (linePoint.y < point.y) {
//				int linePointIndex = mountainChunk.GetIndexOfLinePoint(linePoint);
//				if (linePointIndex == mountainChunk.GetListOfLinePoints().Count - 1) {
//					return 1;
//				}
//				else {
//					pointA = linePoint;
//					pointB = mountainChunk.GetLinePoint(linePointIndex + 1);
//				}
//			}
//			
//			// 3. float goalPlace = pointAPlace + (point - pointA).magnitude / (pointB - pointA).magnitude
//			float pointAPlace = mountainChunk.GetPlaceAtPoint(pointA);
//			goalPlace = pointAPlace + (point - pointA.pointVector).magnitude / (pointB.pointVector - pointA.pointVector).magnitude;
//			return goalPlace;
//		}
//		return 0;
//	}

	private void PlaceOnMountain(float place) {
		_placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtPlace(_placeOnMountain);
		float placeOnChunk = placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtPlace(_placeOnMountain);
		Vector3 position = chunk.GetPositionFromPlace(placeOnChunk);

		body.transform.localEulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(body.transform.localEulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk + 0.01f), ref bodyRotationVelocity, smoothTimeRotation));
		feet.transform.localEulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(feet.transform.localEulerAngles.z, GetTangentAtPlace(chunk, placeOnChunk - 0.01f), ref feetRotationVelocity, smoothTimeRotation));

		player.transform.position = position;
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
			GoTween tween = new GoTween(this, Random.Range(0.2f, 0.3f), new GoTweenConfig().floatProp("placeOnMountain", 0.02f, true).setDelay(Random.Range(0.2f, 0.3f)).setEaseType(GoEaseType.SineInOut));
			Go.addTween(tween);
			tween.play();
			yield return StartCoroutine(tween.waitForCompletion());
		}
	}
}
