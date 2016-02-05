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
		position.x = GameScreen.instance.lowerRightWithMargin.x;
		float minY = GameScreen.instance.center.y;
		float maxY = GameScreen.instance.center.y + GameScreen.instance.height;
		position.y = Mathf.Lerp(minY, maxY, Random.value);
		eagle.transform.position = position;
		eagle.StartFlying();
	}
}
