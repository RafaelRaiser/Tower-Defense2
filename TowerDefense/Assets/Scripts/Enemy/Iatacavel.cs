using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IAtacavel.cs
public interface Iatacavel
{
    void Atacar();
}

// IBuscadorDeAlvo.cs
public interface IBuscadorDeAlvo
{
    Transform ObterAlvo();
}
