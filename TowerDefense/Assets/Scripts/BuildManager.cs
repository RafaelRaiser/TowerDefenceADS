using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField] private Tower[] towers;
    private int towerSelected;

    private void Awake()
    {
        instance = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[towerSelected];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        towerSelected = _selectedTower;
    }
}

