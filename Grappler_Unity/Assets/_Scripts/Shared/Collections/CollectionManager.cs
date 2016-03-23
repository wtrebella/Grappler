using UnityEngine;
using System.Collections;

public class CollectionManager : MonoBehaviour {
	public static CollectionManager instance;

	public Collection[] collectionPrefabs;

	private void Awake() {
		InitializeSingleton();
		LoadCollections();
	}

	private void InitializeSingleton() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		}
	}

	private void LoadCollections() {
		foreach (Collection collection in collectionPrefabs) Instantiate(collection).transform.SetParent(this.transform);
	}
}
