using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iatacavel
{
    void Atacar(); // Define m�todo de ataque.
}

public interface IBuscadorDeAlvo
{
    Transform ObterAlvo(); // Define m�todo de obten��o de alvo.
}
