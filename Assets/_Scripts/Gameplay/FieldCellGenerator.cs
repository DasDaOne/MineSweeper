using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FieldCellGenerator
{
    public static Cell[,] GenerateField(Cell prefab, GridLayoutGroup parentGroup, int cellAmount, float cellSpacingFactor = 0.1f)
    {
        Cell[,] cells = new Cell[cellAmount,cellAmount];

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i / cellAmount, i % cellAmount] = Object.Instantiate(prefab, parentGroup.transform);
        }
        
        float availableSpace = (parentGroup.transform as RectTransform)!.rect.width - parentGroup.padding.horizontal;
        float cellSize = availableSpace / (cellAmount + (cellAmount - 1) * cellSpacingFactor);
        parentGroup.cellSize = Vector2.one * cellSize;
        parentGroup.spacing = Vector2.one * (cellSize * cellSpacingFactor);

        return cells;
    }
}
