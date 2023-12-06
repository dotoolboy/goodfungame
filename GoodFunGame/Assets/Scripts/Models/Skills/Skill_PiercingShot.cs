using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PiercingShot : SkillBase
{
    float _generateTime = 0.5f;
    float _totalTime = 5f;
    GameObject _piercingProjectile;


    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 8f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        StartCoroutine(PiercingShotGenerate());
        Main.Object.Player.IsPiercingAttack = true;

        return true;
    }

    IEnumerator PiercingShotGenerate()
    {
        float elapsedTime = 0f;
        Vector3 offset = new Vector3(0, 1f, 0f);

        while (elapsedTime < _totalTime)
        {
            _piercingProjectile = Main.Resource.InstantiatePrefab("PiercingProjectile.prefab");
            _piercingProjectile.transform.position = transform.position + offset;

            yield return new WaitForSeconds(_generateTime);
            elapsedTime += _generateTime;
        }
        Main.Object.Player.IsPiercingAttack = false;
    }
}
