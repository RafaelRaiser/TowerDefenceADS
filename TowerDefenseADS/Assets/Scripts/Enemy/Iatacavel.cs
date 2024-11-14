using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iatacavel
{
    void Atacar(); // Define método de ataque.
}

public interface IBuscadorDeAlvo
{
    Transform ObterAlvo(); // Define método de obtenção de alvo.
}
