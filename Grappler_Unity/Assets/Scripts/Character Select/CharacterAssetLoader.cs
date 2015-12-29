using UnityEngine;
using System.Collections;

public static class CharacterAssetLoader {
	public static Character[] LoadCharacters() {
		Object[] objects = Resources.LoadAll(GetFolderPath());
		Character[] characters = new Character[objects.Length];
		for (int i = 0; i < objects.Length; i++) {
			characters[i] = ((GameObject)objects[i]).GetComponent<Character>();
		}
		return characters;
	}

	public static Character LoadCharacter(CharacterType characterType) {
		Character character = ((GameObject)Resources.Load(GetCharacterPath(characterType))).GetComponent<Character>();
		return character;
	}

	private static string GetFolderPath() {
		return "Characters";
	}

	private static string GetCharacterPath(CharacterType characterType) {
		return "Characters/" + characterType.ToString();
	}
}
