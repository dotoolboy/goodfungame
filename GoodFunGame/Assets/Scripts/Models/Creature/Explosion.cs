using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Thing
{
    public void DespawnExplosion()
    {
        StartCoroutine(EndExplosion());
    }
    IEnumerator EndExplosion()
    {
        yield return new WaitForSeconds(1f);

        Main.Object.Despawn(this);
    }
}
