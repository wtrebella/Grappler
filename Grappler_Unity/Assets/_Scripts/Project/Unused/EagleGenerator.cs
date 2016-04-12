using UnityEngine;
using System.Collections;

public class EagleGenerator : MonoBehaviour {
	[SerializeField] private Eagle eaglePrefab;
	
	private void Start() {
		StartCoroutine(EagleSpawnLoop());
	}
	
	private IEnumerator EagleSpawnLoop() {
		while (true) {
			GenerateEagle();
			yield return new WaitForSeconds(1);
		}
	}

	private void GenerateEagle() {
		Eagle eagle = eaglePrefab.Spawn();
		eagle.transform.parent = transform;
		Vector3 position = Vector3.zero;
		position.x = ScreenUtility.instance.lowerRightWithMargin.x;
		float minY = ScreenUtility.instance.center.y;
		float maxY = ScreenUtility.instance.center.y + ScreenUtility.instance.height;
		position.y = Mathf.Lerp(minY, maxY, Random.value);
		eagle.transform.position = position;
		eagle.StartFlying();
	}
}
