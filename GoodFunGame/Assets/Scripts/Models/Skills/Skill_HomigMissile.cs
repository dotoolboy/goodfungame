using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HomigMissile : SkillBase
{
    GameObject _homingMissile;
    
    float _generateTime = 1f;
    float _totalTime = 3f;

    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 5f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        StartCoroutine(HomingMissileGeneration());


        return true;
    }

    IEnumerator HomingMissileGeneration()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _totalTime)
        {
            _homingMissile = Main.Resource.InstantiatePrefab("HomingMissile.prefab");
            _homingMissile.GetComponent<Rigidbody2D>().velocity = Vector2.up * Time.deltaTime;
            _homingMissile.transform.position = transform.position;

            yield return new WaitForSeconds(_generateTime);

            elapsedTime += _generateTime;
        }
    }
}
