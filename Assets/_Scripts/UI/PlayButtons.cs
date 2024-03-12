using UnityEngine;
using UnityEngine.UI;

public class PlayButtons : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Button buttonEasy;
    [SerializeField] private Button buttonMedium;
    [SerializeField] private Button buttonHard;
    [SerializeField] private Button buttonExpert;

    private void OnEasyButtonClick() => mainMenu.OnPlayButtonClick(GameDifficulty.Easy);
    private void OnMediumButtonClick() => mainMenu.OnPlayButtonClick(GameDifficulty.Medium);
    private void OnHardButtonClick() => mainMenu.OnPlayButtonClick(GameDifficulty.Hard);
    private void OnExpertButtonClick() => mainMenu.OnPlayButtonClick(GameDifficulty.Expert);

    private void OnEnable()
    {
        buttonEasy.onClick.AddListener(OnEasyButtonClick);
        buttonMedium.onClick.AddListener(OnMediumButtonClick);
        buttonHard.onClick.AddListener(OnHardButtonClick);
        buttonExpert.onClick.AddListener(OnExpertButtonClick);
    }

    private void OnDisable()
    {
        buttonEasy.onClick.RemoveListener(OnEasyButtonClick);
        buttonMedium.onClick.RemoveListener(OnMediumButtonClick);
        buttonHard.onClick.RemoveListener(OnHardButtonClick);
        buttonExpert.onClick.RemoveListener(OnExpertButtonClick);
    }
}
