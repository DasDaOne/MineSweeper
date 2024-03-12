using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BasePanel
{
    [SerializeField] private GameplayMenu gameplayMenuPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
    }

    private void OnRestartButtonClick()
    {
        Hide();
        gameplayMenuPanel.OnGameOverRestart();
    }

    private void OnMainMenuButtonClick()
    {
        Hide();
        gameplayMenuPanel.Hide();
        gameplayMenuPanel.OnGameOverBack();
    }
}
