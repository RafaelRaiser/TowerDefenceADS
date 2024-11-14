using System;
using UnityEngine;


// Classe que representa uma Torre (arma), armazenando dados básicos como nome e prefab
[Serializable]
public class Tower
{
    // Nome da torre, para identificação
    public string name;

    // Prefab da torre que será instanciado no jogo
    public GameObject prefab;

    // Construtor para inicializar a torre com nome e prefab fornecidos
    public Tower(string _name, GameObject _prefab)
    {
        name = _name;
        prefab = _prefab;
    }
}
