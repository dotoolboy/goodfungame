using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ReflectShield : SkillBase
{
    private float _duration = 5f;

    private Skill_ReflectShieldObject _shield;

    public override void Initialize()
    {
        base.Initialize();
        Cooldown = 7f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        _shield = Main.Resource.InstantiatePrefab("ReflectShield.prefab").GetComponent<Skill_ReflectShieldObject>();
        _shield.transform.SetParent(this.transform, true);
        _shield.Initialize(_duration);

        return true;
    }
}