/*using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SausageSoccerPrefs : ScriptableObjectSingleton<SausageSoccerPrefs>
{
#pragma warning disable 414
	[SerializeField]
	bool _skipMenu = false;
	public static bool skipMenu
	{
#if UNITY_EDITOR
		get { return instance._skipMenu; }
#else
		get { return false; }
#endif
	}

	[SerializeField]
	bool _disableMusic = false;
	public static bool disableMusic
	{
#if UNITY_EDITOR
		get { return instance._disableMusic; }
#else
		get { return false; }
#endif
	}

    [SerializeField]
    CharacterSkin _characterCostumeOverride = new CharacterSkin();
    public static CharacterSkin characterCostumeOverride
    {
#if UNITY_EDITOR
        get { return instance._characterCostumeOverride; }
#else
        get { return new CharacterSkin(); }
#endif
    }

	[SerializeField]
	bool _skipCountdowns = false;
	public static bool skipCountdowns
	{
#if UNITY_EDITOR
		get { return instance._skipCountdowns; }
#else
		get { return false; }
#endif
	}

	[SerializeField]
	float _gameTimeOverride = 5f;
	public static float gameTimeOverride
	{
#if UNITY_EDITOR
		get { return instance._gameTimeOverride; }
#else
		get { return 0f; }
#endif
	}

	[SerializeField]
	GameStateType _initGameState = GameStateType.CharacterSelect;
	public static GameStateType initGameState
	{
#if UNITY_EDITOR
		get { return instance._initGameState; }
#else
		get { return GameStateType.None; }
#endif
	}

	[SerializeField]
	GameScenes _levelOverride = GameScenes.Menu;
	public static GameScenes levelOverride
	{
#if UNITY_EDITOR
		get { return instance._levelOverride; }
#else
		get { return GameScenes.Menu; }
#endif
	}

	[SerializeField]
	bool _spawnHumanByDefault = false;
	public static bool spawnHumanByDefault
	{
#if UNITY_EDITOR
		get { return instance._spawnHumanByDefault; }
#else
		get { return false; }
#endif
	}

	[SerializeField, Range(0, 8)]
	int _defaultAICount = 0;
	public static int defaultAICount
	{
#if UNITY_EDITOR
		get { return instance._defaultAICount; }
#else
		get { return 0; }
#endif
	}

#pragma warning restore 414
}
*/