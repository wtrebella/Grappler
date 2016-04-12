using UnityEngine;
using System.Collections;
using Spine;

[System.Serializable]
public class WhitSpineSlot : MonoBehaviour {
	public string slotName;

	private WhitSpineSkeleton parentSkeleton;

	private void Awake() {
		parentSkeleton = GetComponentInParent<WhitSpineSkeleton>();
		if (parentSkeleton == null) Debug.LogError("must be child of WhitSpineSkeleton component");
	}

	public void SetSprite(string spritePath) {
		parentSkeleton.spineSkeleton.skeleton.SetAttachment(slotName, spritePath);
	}

	public void SetSprite(SpineCollectionItemSprite itemSprite) {
		if (!itemSprite.HasValidSpriteName()) return;

		parentSkeleton.spineSkeleton.skeleton.SetAttachment(slotName, itemSprite.spineSpritePath);
	}

	public void RemoveSprite() {
		parentSkeleton.spineSkeleton.skeleton.SetAttachment(slotName, null);
	}
}