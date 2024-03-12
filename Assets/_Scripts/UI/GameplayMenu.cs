using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : BasePanel
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MineSweeperField mineSweeperField;
    [SerializeField] private Button backButton;

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonClick);
    }

    public void StartGame(GameDifficulty difficulty)
    {
        Show();
        mineSweeperField.GenerateField(difficulty);
    }

    private void OnBackButtonClick()
    {
        Hide();
        mainMenu.Show();
    }
}
