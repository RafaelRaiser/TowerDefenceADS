using System;
using UnityEngine;


// Classe que representa uma Torre (arma), armazenando dados básicos como nome e prefab
[Serializable]
public class Tower
{
    public string name;
    public GameObject prefab;
    public int cost;


// Construtor para inicializar a torre com nome e prefab fornecidos
public Tower(string _name, GameObject _prefab)
    {
        name = _name;
        prefab = _prefab;
    }
    
}
