using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text nearbyBombCounter;
    [SerializeField] private Image bgImage;
    [Header("Colors")]
    [SerializeField] private Color closedColor;
    [SerializeField] private Color openedColor;
    
    [HideInInspector]
    public bool hasBomb;
    [HideInInspector]
    public int nearbyBombAmount = -1;
    
    private int cellId;

    public void Init(int id)
    {
        cellId = id;
        SetState(CellState.Closed);
    }

    public void SetState(CellState state)
    {
        nearbyBombCounter.gameObject.SetActive(false);
        
        switch (state)
        {
            case CellState.Opened:
                bgImage.color = openedColor;
                nearbyBombCounter.gameObject.SetActive(true);
                nearbyBombCounter.text = $"{nearbyBombAmount}";
                break;
            case CellState.Closed:
                bgImage.color = closedColor;
                break;
        }
        
        
        
        bgImage.color = openedColor;
        nearbyBombCounter.gameObject.SetActive(true);
        nearbyBombCounter.text = $"{nearbyBombAmount}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MineSweeperField.OnCellClick.Invoke(cellId);
    }
    
    public enum CellState
    {
        Opened,
        Closed
    }
}
