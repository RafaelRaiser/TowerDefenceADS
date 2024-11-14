using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 

{
    public static LevelManager instance;     
    public Transform startPoint;   
    public Transform[] path;   

    #region Singleton
    private void Awake()    // M�todo chamado quando o objeto � inicializado.

    {
        instance = this; // Inicializa a inst�ncia singleton.
    }
    #endregion
}
