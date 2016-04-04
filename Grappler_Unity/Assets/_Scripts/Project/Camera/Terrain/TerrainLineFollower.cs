using UnityEngine;
using System.Collections;

public class TerrainLineFollower : MonoBehaviour {
	[SerializeField] private TerrainLine[] terrainLines;
	[SerializeField] private float speed = 10;
	[SerializeField] private float smoothDampTime = 0.3f;

	private Vector3 smoothDampVelocity;
	private float x = 0;

	private void Update() {
		x += speed * Time.deltaTime;
		Vector2 totalPoints = Vector2.zero;
		foreach (TerrainLine terrainLine in terrainLines) {
			totalPoints += terrainLine.GetAveragePointAtX(x);
		}
		Vector2 averagePoint = totalPoints / terrainLines.Length;
		Vector3 targetPosition = new Vector3(x, averagePoint.y, transform.position.z);
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothDampTime);
		transform.position = smoothedPosition;
	}
}
