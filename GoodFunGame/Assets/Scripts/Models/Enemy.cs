using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature {

    #region Properties



    #endregion

    #region Fields



    #endregion

    #region MonoBehaviours



    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        return true;
    }

    public override void SetInfo(string key) {
        base.SetInfo(key);


    }

    #endregion

    #region State

    protected override void OnStateEntered_Dead() {
        base.OnStateEntered_Dead();

        // TODO:: Player의 KillCount 증가.

        // TODO:: 오브젝트 디스폰
    }

    #endregion
}