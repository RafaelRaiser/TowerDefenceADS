using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 

{
    public static LevelManager instance;     
    public Transform startPoint;   
    public Transform[] path;   

    #region Singleton
    private void Awake()    // Método chamado quando o objeto é inicializado.

    {
        instance = this; // Inicializa a instância singleton.
    }
    #endregion
}
