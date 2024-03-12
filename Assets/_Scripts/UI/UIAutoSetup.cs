using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAutoSetup : MonoBehaviour
{
    [SerializeField] private Panel[] panels;
    
    [Serializable]
    private struct Panel
    {
        public BasePanel basePanel;
        public bool activeOnStart;
    }

    private void Start()
    {
        foreach (var panel in panels)
        {
            if(panel.activeOnStart)
                panel.basePanel.Show();
            else
                panel.basePanel.Hide();
        }
    }
}
