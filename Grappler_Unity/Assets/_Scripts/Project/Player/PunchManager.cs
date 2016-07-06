using UnityEngine;
using System.Collections;
using WhitTerrain;

public class PunchManager : MonoBehaviour {
	public static PunchManager instance;

	[SerializeField] private CragFinder cragFinder;
	[SerializeField] private ContourPair terrainPair;
	[SerializeField] private Player player;

	private void Awake() {
		instance = this;
	}

	public void PunchThroughCragIfNear() {
		Crag crag = cragFinder.FindInDirection(terrainPair.GetThroughDirection(player.body.transform.position));
		if (crag) {
//			player.SetState(Player.PlayerStates.Punching);
			crag.Explode();
		}
	}
}
