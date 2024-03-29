using System;
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
    
    [NonSerialized]
    public bool HasBomb;
    [NonSerialized]
    public int NearbyBombAmount = -1;
    [NonSerialized]
    public int CellId;
    [NonSerialized]
    public CellState CurrentState;

    public void Init(int id)
    {
        CellId = id;
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
                nearbyBombCounter.text = $"{NearbyBombAmount}";
                break;
            case CellState.Closed:
                bgImage.color = closedColor;
                break;
        }

        CurrentState = state;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MineSweeperField.OnCellClick.Invoke(this);
    }
    
    public enum CellState
    {
        Opened,
        Closed
    }
}
