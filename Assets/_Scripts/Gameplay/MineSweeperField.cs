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
	[SerializeField] private GameOverScreen gameOverScreen;

	private GameDifficulty gameDifficulty;
	private Cell[,] cells;

	private bool areCellsInitialized;

	public static readonly UnityEvent<Cell> OnCellClick = new ();

	private void OnEnable()
	{
		OnCellClick.AddListener(OnAnyCellClick);
	}

	private void OnDisable()
	{
		OnCellClick.RemoveListener(OnAnyCellClick);
	}

	public void Restart() => GenerateField(gameDifficulty);

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
		if (cells == null || cells.Length == 0)
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
			if (i != clickedCellId)
				indexes.Add(i);
		}

		var rng = new System.Random();
		indexes = indexes.OrderBy(x => rng.Next()).ToList();

		for (int i = 0; i < gameDifficulty.BombAmount(); i++)
		{
			CellFromOneDimId(indexes[i]).HasBomb = true;
		}

		for (int i = 0; i < cells.Length; i++)
		{
			if (!CellFromOneDimId(i).HasBomb)
				CellFromOneDimId(i).NearbyBombAmount =
					GetNeighbours(i / gameDifficulty.FieldSize(), i % gameDifficulty.FieldSize()).Count(x => x.HasBomb);
		}

		areCellsInitialized = true;

		string debugLog = "";
		for (int row = 0; row < gameDifficulty.FieldSize(); row++)
		{
			for (int col = 0; col < gameDifficulty.FieldSize(); col++)
			{
				debugLog += $"[{(cells[row, col].HasBomb ? "*" : (cells[row, col].NearbyBombAmount == 0 ? " " : cells[row, col].NearbyBombAmount))}]";
				if (col != gameDifficulty.FieldSize() - 1)
					debugLog += " ";
			}
			
			if(row != gameDifficulty.FieldSize() - 1)
				debugLog += "\n";
		}
		
		Debug.Log("Field\n" + debugLog);
	}

	private void OnAnyCellClick(Cell clickedCell)
	{
		if (!areCellsInitialized)
			InitializeCells(clickedCell.CellId);

		if (clickedCell.HasBomb)
		{
			gameOverScreen.Show();
			return;
		}

		OpenCells(clickedCell);
	}

	private void OpenCells(Cell cell)
	{
		if (cell.CurrentState == Cell.CellState.Opened)
			return;

		cell.SetState(Cell.CellState.Opened);

		if (cell.NearbyBombAmount != 0)
			return;
		
		foreach (var neighbour in GetNeighbours(cell.CellId))
		{
			OpenCells(neighbour);
		}
	}

	private List<Cell> GetNeighbours(int cellId) => GetNeighbours(cellId / gameDifficulty.FieldSize(), cellId % gameDifficulty.FieldSize());

	private List<Cell> GetNeighbours(int row, int col)
	{
		List<Cell> neighbourCells = new List<Cell>();

		if (row > 0)
			neighbourCells.AddRange(GetRowNeighbours(row - 1, col));
		if (row < gameDifficulty.FieldSize() - 1)
			neighbourCells.AddRange(GetRowNeighbours(row + 1, col));
		if (col > 0)
			neighbourCells.Add(cells[row, col - 1]);
		if (col < gameDifficulty.FieldSize() - 1)
			neighbourCells.Add(cells[row, col + 1]);

		return neighbourCells;
	}

	private List<Cell> GetRowNeighbours(int row, int col)
	{
		List<Cell> neighbourCells = new List<Cell>();

		if (col > 0)
			neighbourCells.Add(cells[row, col - 1]);
		if (col < gameDifficulty.FieldSize() - 1)
			neighbourCells.Add(cells[row, col + 1]);
		neighbourCells.Add(cells[row, col]);

		return neighbourCells;
	}

	private Cell CellFromOneDimId(int id)
	{
		return cells[id / gameDifficulty.FieldSize(), id % gameDifficulty.FieldSize()];
	}
}
