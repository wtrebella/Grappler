using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class Generator : MonoBehaviour {
	public enum GeneratableType {
		Horizontal,
		Vertical,
		HorizontalAndVertical,
		Free,
		NONE
	}
	
	public Generatable[] generateablePrefabs;
	public GeneratableType generatorType = GeneratableType.NONE;
	public int maxGeneratedObjects = -1;
	public float minGenerationTime = 0.5f;
	public float maxGenerationTime = 2;
	public float xMargin = 0;
	public float yMargin = 0;
	
	protected Rect boundsRect;
	protected List<Generatable> generatedObjects;
	
	void Start () {
		Init();
		
		if (generateablePrefabs != null && generateablePrefabs.Length > 0) StartCoroutine(StartGenerationLoop());
	}
	
	void Update () {
		
	}
	
	public virtual void Init() {
		Bounds bounds = GetComponent<Collider2D>().bounds;
		GetComponent<Collider2D>().isTrigger = true;
		generatedObjects = new List<Generatable>();
		
		Vector2 origin = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
		
		boundsRect = new Rect(origin.x, origin.y, bounds.size.x, bounds.size.y);
	}
	
	public virtual IEnumerator StartGenerationLoop() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime));
			
			int side = Random.Range(0, 2);
			Vector3 pos = GetGenerationPosition(side);
			
			if (pos.x < boundsRect.x || pos.x > boundsRect.xMax) {
				yield return null;
				continue;
			}
			
			if (pos.y < boundsRect.y || pos.y > boundsRect.yMax) {
				yield return null;
				continue;
			}
			
			Generatable obj = (Generatable)Instantiate(generateablePrefabs[Random.Range(0, generateablePrefabs.Length)]);
			obj.transform.parent = transform;
			obj.transform.position = new Vector3(pos.x, pos.y, pos.z);
			obj.GenerationComplete(this, side);
			generatedObjects.Add(obj);
			
			if (maxGeneratedObjects > 0) {
				if (generatedObjects.Count > maxGeneratedObjects) {
					foreach (Generatable sp in generatedObjects) if (sp == null) Debug.Log(generatedObjects.IndexOf(sp));
					Generatable s = generatedObjects[0];
					generatedObjects.RemoveAt(0);
					s.NeedsDelete(this);
				}
			}
		}
	}
	
	protected Vector3 GetGenerationPosition(int horizontalSide) {
		Vector3 pos = Vector3.zero;
		Rect worldRect = GameScreen.instance.worldRect;
		
		if (generatorType == GeneratableType.Horizontal || generatorType == GeneratableType.HorizontalAndVertical) {
			if (horizontalSide == 0) pos.x = worldRect.x + xMargin;
			else pos.x = worldRect.xMax - xMargin;
		}
		else pos.x = Random.Range(worldRect.x + xMargin, worldRect.xMax - xMargin);
		
		if (generatorType == GeneratableType.Vertical || generatorType == GeneratableType.HorizontalAndVertical) {
			int verticalSide = Random.Range(0, 2);
			
			if (verticalSide == 0) pos.y = worldRect.y + yMargin;
			else pos.y = worldRect.yMax - yMargin;
		}
		else pos.y = Random.Range(worldRect.y + yMargin, worldRect.yMax - yMargin);
		
		return pos;
	}
}
