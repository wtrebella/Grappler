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
		float lowerQuarterY = GameScreen.instance.minY + GameScreen.instance.height * 0.25f;
		float upperQuarterY = GameScreen.instance.maxY - GameScreen.instance.height * 0.25f;
		position.y = Mathf.Lerp(lowerQuarterY, upperQuarterY, Random.value);
		eagle.transform.position = position;
		eagle.StartFlying();
	}
}
