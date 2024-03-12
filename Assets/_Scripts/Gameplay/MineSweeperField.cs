using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MineSweeperField : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private GameDifficulty gameDifficulty;
    private Cell[,] cells;

    private bool areCellsInitialized;

    public static readonly UnityEvent<int> OnCellClick = new ();

    private void OnEnable()
    {
        OnCellClick.AddListener(OnAnyCellClick);
    }

    private void OnDisable()
    {
        OnCellClick.RemoveListener(OnAnyCellClick);
    }

    public void GenerateField(GameDifficulty difficulty)
    {
        ClearField();
        cells = FieldCellGenerator.GenerateField(cellPrefab, gridLayoutGroup, difficulty.FieldSize());

        areCellsInitialized = false;
        gameDifficulty = difficulty;
        
        for (int i = 0; i < cells.Length; i++)
        {
            CellFromOneDimId(i).Init(i);
        }
    }
    
    private void ClearField()
    {
        if(cells == null || cells.Length == 0)
            return;

        foreach (Cell cell in cells)
        {
            Destroy(cell.gameObject);
        }
    }

    private void InitializeCells(int clickedCellId)
    {
        List<int> indexes = new List<int>();
        
        for (int i = 0; i < cells.Length; i++)
        {
            if(i != clickedCellId)
                indexes.Add(i);
        }

        var rng = new System.Random();
        indexes = indexes.OrderBy(x => rng.Next()).ToList();

        for (int i = 0; i < gameDifficulty.BombAmount(); i++)
        {
            CellFromOneDimId(indexes[i]).hasBomb = true;
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if(!CellFromOneDimId(i).hasBomb)
                CellFromOneDimId(i).nearbyBombAmount = GetNearbyBombsAmount(i);
        }
    }
    
    private void OnAnyCellClick(int cellId)
    {
        if(!areCellsInitialized)
            InitializeCells(cellId);
        
        
    }

    private int GetNearbyBombsAmount(int cellId)
    {
        int bombAmount = 0;

        int row = cellId / gameDifficulty.FieldSize();
        int col = cellId % gameDifficulty.FieldSize();

        // TOP
        if (row > 0)
            bombAmount += GetRowNeighbours(row - 1, col);

        // BOTTOM
        if (row < gameDifficulty.FieldSize() - 1)
            bombAmount += GetRowNeighbours(row + 1, col);

        // RIGHT
        if (col < gameDifficulty.FieldSize() - 1)
            bombAmount += cells[row, col + 1].hasBomb ? 1 : 0;
        
        // LEFT
        if (col > 0)
            bombAmount += cells[row, col - 1].hasBomb ? 1 : 0;

        return bombAmount;
    }

    private int GetRowNeighbours(int row, int col)
    {
        return (col > 0 && cells[row, col - 1].hasBomb ? 1 : 0) +
               (cells[row, col].hasBomb ? 1 : 0) +
               (col < gameDifficulty.FieldSize() - 1 && cells[row, col + 1].hasBomb ? 1 : 0);
    }

    private Cell CellFromOneDimId(int id)
    {
        return cells[id / gameDifficulty.FieldSize(), id % gameDifficulty.FieldSize()];
    }
}
