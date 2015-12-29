using UnityEngine;
using System.Collections;

public class GrapplerCharacterSetter : MonoBehaviour {
	[SerializeField] private Transform characterHolder;

	private void Start() {
		SetCharacter();
	}

	private void SetCharacter() {
		Character characterPrefab = CharacterAssetLoader.LoadCharacter(StateVariablesManager.stateVariables.characterType);
		Character character = Instantiate(characterPrefab);
		character.transform.parent = characterHolder;
		character.transform.localPosition = Vector3.zero;
	}
}
