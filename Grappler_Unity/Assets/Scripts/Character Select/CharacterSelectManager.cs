using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour {
	[SerializeField] private Transform characterHolder;

	private Character currentCharacter;
	private Character[] characters;

	public void ShowNextCharacter() {
		CharacterType characterType = currentCharacter.characterType;
		characterType = (CharacterType)(((int)characterType + 1) % ((int)CharacterType.MAX));
		ShowCharacter(characterType);
	}

	public void ShowCharacter(CharacterType characterType) {
		DestroyCurrentCharacter();
		InstantiateCharacterTypeInScene(characterType);
	}

	public void PlayWithCurrentCharacter() {
		if (currentCharacter == null) Debug.LogError("no current character!");
		StateVariablesManager.stateVariables.characterType = currentCharacter.characterType;
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}

	private void DestroyCurrentCharacter() {
		if (currentCharacter == null) return;

		Destroy(currentCharacter.gameObject);
	}

	private void InstantiateCharacterTypeInScene(CharacterType characterType) {
		Character characterPrefab = GetCharacter(characterType);
		Character character = Instantiate(characterPrefab);
		character.transform.parent = characterHolder;
		character.transform.localPosition = Vector3.zero;
		currentCharacter = character;
	}

	private void Awake() {
		LoadCharactersIfNeeded();
	}

	private void Start() {
		InstantiateCharacterTypeInScene(StateVariablesManager.stateVariables.characterType);
	}

	private Character GetCharacter(CharacterType characterType) {
		LoadCharactersIfNeeded();

		foreach (Character character in characters) {
			if (character.characterType == characterType) return character;
		}

		Debug.LogError("could not find character of type: " + characterType);
		return null;
	}

	private void LoadCharacters() {
		characters = CharacterAssetLoader.LoadCharacters();	
	}

	private void LoadCharactersIfNeeded() {
		if (characters == null) LoadCharacters();
	}
}
