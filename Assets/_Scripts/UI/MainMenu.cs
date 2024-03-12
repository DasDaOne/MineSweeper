using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BasePanel
{
	[SerializeField] private GameplayMenu gameMenu;

	public void OnPlayButtonClick(GameDifficulty difficulty)
	{
		Hide();
		gameMenu.StartGame(difficulty);
	}
}
