using UnityEngine;
using System.Collections;

public enum GameStateType
{
	None = 0,

	// Menus
    MainMenu = 450,
	CharacterSelect = 500,
	GameOver = 550,

	// Game Modes
	Soccer = 1500,
	CaptureTheFlagTwoTeam = 1600,
	CaptureTheFlagMultiTeam = 1650,
	HotPotato = 1700,
	Coinz = 1800,
	Sumo = 1900,
	Volleyball = 2000
}