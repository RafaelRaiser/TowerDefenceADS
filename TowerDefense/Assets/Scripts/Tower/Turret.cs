// Classe base para torres, implementa interfaces para ataque e busca de alvo.
using UnityEngine;

public class Turret : MonoBehaviour, Iatacavel, IBuscadorDeAlvo
{
    [SerializeField] protected float targetingRange = 5f; // Alcance de detec��o de inimigos.
    [SerializeField] protected LayerMask enemyMask; // M�scara de camada para identificar inimigos.
    [SerializeField] protected GameObject bulletPrefab; // Prefab do proj�til disparado pela torre.
    [SerializeField] protected Transform firingPoint; // Ponto de origem do disparo.
    [SerializeField] private float bps = 1f; // Taxa de tiros por segundo (bullets per second).

    protected Transform target; // Alvo atual da torre.
    protected float timeUntilFire; // Tempo acumulado at� o pr�ximo disparo.

    // M�todo para ataque. Pode ser sobrescrito por subclasses espec�ficas.
    public virtual void Atacar() { }

    private void Update()
    {
        // Se n�o h� alvo, busca um novo alvo usando o m�todo ObterAlvo().
        if (target == null)
        {
            target = ObterAlvo();
            return; // Sai do m�todo se n�o encontrou alvo.
        }

        // Verifica se o alvo atual ainda est� no alcance; caso contr�rio, remove-o.
        if (!CheckTargetIsInRange())
        {
            target = null; // Remove o alvo pois est� fora do alcance.
        }
        else
        {
            // Acumula o tempo desde o �ltimo disparo.
            timeUntilFire += Time.deltaTime;

            // Se o tempo acumulado � suficiente para disparar novamente, chama Shoot().
            if (timeUntilFire >= 1f / bps)
            {
                Shoot(); // Dispara o proj�til.
                timeUntilFire = 0f; // Reseta o tempo acumulado para o pr�ximo disparo.
            }
        }
    }

    // M�todo respons�vel pelo disparo de um proj�til em dire��o ao alvo.
    protected virtual void Shoot()
    {
        // Instancia um novo proj�til no ponto de disparo.
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        // Obt�m o script Bullet para definir o alvo do proj�til.
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target); // Define o alvo para o proj�til.
    }

    // Verifica se o alvo atual est� dentro do alcance da torre.
    private bool CheckTargetIsInRange()
    {
        // Calcula a dist�ncia entre a torre e o alvo.
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Busca um novo alvo dentro do alcance da torre.
    public Transform ObterAlvo()
    {
        // Executa um CircleCast para detectar inimigos no alcance da torre.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Retorna o primeiro alvo encontrado ou null se n�o houver alvos no alcance.
        return hits.Length > 0 ? hits[0].transform : null;
    }
}