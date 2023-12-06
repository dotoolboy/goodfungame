using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class Skill_AutoTarget : SkillBase
{
    GameObject _autoProjectile;

    float _generateTime = 1f;
    float _totalTime = 3f;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        StartCoroutine(PeriodicGeneration());

        Cooldown = 13f;

        return true;
    }

    IEnumerator PeriodicGeneration()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _totalTime)
        {
            _autoProjectile = Main.Resource.InstantiatePrefab("AutoProjectile.prefab");
            _autoProjectile.transform.position = transform.position;

            yield return new WaitForSeconds(_generateTime);

            elapsedTime += _generateTime;
        }
    }
}
