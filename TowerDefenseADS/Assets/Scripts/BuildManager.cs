using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField] private Tower[] towers;
    private Tower selectedTower;

    private void Awake()
    {
        instance = this;
    }

    public Tower GetSelectedTower()
    {
        return selectedTower;
    }

    public void SetSelectedTower(int index)
    {
        if (index < 0 || index >= towers.Length) return;
        selectedTower = towers[index];
    }
}
