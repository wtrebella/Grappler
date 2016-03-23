using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class ClothingPackage : CollectablePackage {
	public virtual ClothingPackageType type {get {return ClothingPackageType.None;}}
}