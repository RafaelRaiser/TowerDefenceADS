using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour 

{
    [SerializeField] private SpriteRenderer sr;    
    [SerializeField] private Color hoverColor;    

    private GameObject tower;    
    private Color startColor;  

    private void Start()
    {
        startColor = sr.color;// Armazena cor inicial do SpriteRenderer.
    }
    private void OnMouseEnter()    // Método chamado quando  mouse entra na área do plot.

    {
        sr.color = hoverColor; // Muda  cor do plot para a cor de hover.
    }
    private void OnMouseExit()    // Método chamado quando  mouse sai da área do plot.

    {
        sr.color = startColor;// Restaura  cor inicial do plot.
    }
    private void OnMouseDown()    // Método chamado quando mouse clica no plot.

    {
        if (tower != null) return;        


        Tower towerToBuild = BuildManager.instance.GetSelectedTower();        // Obtém a torre selecionada do BuildManager.

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);       


    }
}
