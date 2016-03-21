using UnityEngine;
using System.Collections;

public class SkeletonGhostController : MonoBehaviour {
	[SerializeField] private SkeletonGhost[] ghosts;
	[SerializeField] private float ghostDuration;

	private bool ghostsEnabled = false;
	private bool runningGhostCoroutine = false;

	public void EnableGhosting() {
		if (ghostsEnabled) return;
		ghostsEnabled = true;
		foreach (SkeletonGhost ghost in ghosts) ghost.ghostingEnabled = true;
	}

	public void DisableGhosting() {
		if (!ghostsEnabled) return;
		ghostsEnabled = false;
		StopGhostCoroutine();
		foreach (SkeletonGhost ghost in ghosts) ghost.ghostingEnabled = false;
	}

	private void EnableGhostsForDuration() {
		StartCoroutine("EnableGhostsCoroutine");
	}

	private IEnumerator EnableGhostsCoroutine() {
		if (runningGhostCoroutine) yield break;
		runningGhostCoroutine = true;
		EnableGhosting();
		yield return new WaitForSeconds(ghostDuration);
		DisableGhosting();
	}

	private void StopGhostCoroutine() {
		StopCoroutine("EnableGhostsCoroutine");
		runningGhostCoroutine = false;
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
