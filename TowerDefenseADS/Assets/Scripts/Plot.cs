using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.instance.GetSelectedTower();
        if (towerToBuild != null && GameManager.instance.SpendMoney(towerToBuild.cost))
        {
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Dinheiro insuficiente ou torre não selecionada!");
        }
    }
}
