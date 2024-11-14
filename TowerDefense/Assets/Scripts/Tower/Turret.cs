// Classe base para torres, implementa interfaces para ataque e busca de alvo.
using UnityEngine;

public class Turret : MonoBehaviour, Iatacavel, IBuscadorDeAlvo
{
    [SerializeField] protected float targetingRange = 5f; // Alcance de detecção de inimigos.
    [SerializeField] protected LayerMask enemyMask; // Máscara de camada para identificar inimigos.
    [SerializeField] protected GameObject bulletPrefab; // Prefab do projétil disparado pela torre.
    [SerializeField] protected Transform firingPoint; // Ponto de origem do disparo.
    [SerializeField] private float bps = 1f; // Taxa de tiros por segundo (bullets per second).

    protected Transform target; // Alvo atual da torre.
    protected float timeUntilFire; // Tempo acumulado até o próximo disparo.

    // Método para ataque. Pode ser sobrescrito por subclasses específicas.
    public virtual void Atacar() { }

    private void Update()
    {
        // Se não há alvo, busca um novo alvo usando o método ObterAlvo().
        if (target == null)
        {
            target = ObterAlvo();
            return; // Sai do método se não encontrou alvo.
        }

        // Verifica se o alvo atual ainda está no alcance; caso contrário, remove-o.
        if (!CheckTargetIsInRange())
        {
            target = null; // Remove o alvo pois está fora do alcance.
        }
        else
        {
            // Acumula o tempo desde o último disparo.
            timeUntilFire += Time.deltaTime;

            // Se o tempo acumulado é suficiente para disparar novamente, chama Shoot().
            if (timeUntilFire >= 1f / bps)
            {
                Shoot(); // Dispara o projétil.
                timeUntilFire = 0f; // Reseta o tempo acumulado para o próximo disparo.
            }
        }
    }

    // Método responsável pelo disparo de um projétil em direção ao alvo.
    protected virtual void Shoot()
    {
        // Instancia um novo projétil no ponto de disparo.
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        // Obtém o script Bullet para definir o alvo do projétil.
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target); // Define o alvo para o projétil.
    }

    // Verifica se o alvo atual está dentro do alcance da torre.
    private bool CheckTargetIsInRange()
    {
        // Calcula a distância entre a torre e o alvo.
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Busca um novo alvo dentro do alcance da torre.
    public Transform ObterAlvo()
    {
        // Executa um CircleCast para detectar inimigos no alcance da torre.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Retorna o primeiro alvo encontrado ou null se não houver alvos no alcance.
        return hits.Length > 0 ? hits[0].transform : null;
    }
}