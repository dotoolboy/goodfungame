using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TempBoss : Enemy
{
    #region Enums

    enum Phase
    {
        None,
        Phase1,
        Phase2,
        Phase3,
    }

    #endregion

    #region Fields

    private Phase _currentPhase = Phase.Phase1;
    private Phase _doingPhase = Phase.None;
    private bool _action = false;

    private ProjectileGenerator _projectileGenerator;
    private Coroutine _coAction;
    private Coroutine _coMove;

    #endregion

    #region MonoBehaviours

    protected override void Update()
    {
        base.Update();

        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            if (_currentPhase != Phase.Phase3)
            {
                _currentPhase++;
                if (_coAction != null)
                {
                    StopCoroutine(_coAction);
                    _coAction = null;

                    StopAllCoroutines();
                }
                if (_coMove != null)
                {
                    StopCoroutine(_coMove);
                    _coMove = null;
                }
            }
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            _action = true;
        }
        if (_action == false) return;
        _coAction ??= _currentPhase switch
        {
            Phase.Phase1 => StartCoroutine(CoPhase1()),
            Phase.Phase2 => StartCoroutine(CoPhase2()),
            Phase.Phase3 => StartCoroutine(CoPhase3()),
            _ => null
        };
    }

    #endregion

    private IEnumerator CoPhase1()
    {
        _doingPhase = Phase.Phase1;

        yield return StartCoroutine(CoMove(new Vector2(4, 2)));
        yield return StartCoroutine(CoShot_1());
        yield return StartCoroutine(CoMove(new Vector2(-4, 2)));
        yield return StartCoroutine(CoShot_1());
        yield return StartCoroutine(CoMove(new Vector2(0, 0)));
        yield return StartCoroutine(CoShot_2());
        yield return StartCoroutine(CoShot_2());
        yield return StartCoroutine(CoShot_2());
        yield return StartCoroutine(CoShot_2());
        yield return new WaitForSeconds(1);
        _coAction = null;
        yield break;
    }
    private IEnumerator CoPhase2()
    {
        _doingPhase = Phase.Phase2;

        yield return StartCoroutine(CoMove(new Vector2(0, 2)));
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return new WaitForSeconds(1);
        _coAction = null;
        yield break;
    }
    private IEnumerator CoPhase3()
    {
        _doingPhase = Phase.Phase3;
        _coMove ??= StartCoroutine(CoMoveTurn(new(-4, 1), new(4, 1), 1));

        yield return StartCoroutine(CoShot_1());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_1());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_3());
        yield return StartCoroutine(CoShot_4());
        yield return StartCoroutine(CoShot_2());
        yield return StartCoroutine(CoShot_4());
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(CoShot_4());
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(CoShot_4());
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(CoShot_4());
        yield return new WaitForSeconds(0.25f);
        _coAction = null;
        yield break;
    }

    private IEnumerator CoMove(Vector2 targetPosition)
    {
        Vector2 originPosition = this.transform.position;
        float t = 0;
        while (t <= 1)
        {
            this.transform.position = Vector2.Lerp(originPosition, targetPosition, t);
            t += Time.deltaTime;
            yield return null;
        }
        this.transform.position = targetPosition;
        yield break;
    }
    private IEnumerator CoMoveTurn(Vector2 pos1, Vector2 pos2, float waitTime = 1f)
    {
        yield return StartCoroutine(CoMove(pos1));
        while (true)
        {
            yield return StartCoroutine(CoMove(pos2));
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(CoMove(pos1));
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator CoShot_1()
    {
        for (int i = 0; i < 5; i++)
        {
            float startAngle = 22.5f * i;
            PG_Circle circleShot = Main.Object.SpawnProjectileGenerator<PG_Circle>();
            circleShot.transform.position = this.transform.position;
            circleShot.transform.SetParent(this.transform);
            circleShot.Initialize(this, "Bullet_1_KSJ", 20, 1, 5, startAngle);
            circleShot.Shot();

            _projectileGenerator = circleShot;
        }
        yield return new WaitUntil(() => _projectileGenerator == null);
        yield break;
    }

    private IEnumerator CoShot_2()
    {
        for (int i = 0; i < 5; i++)
        {
            float startAngle = 22.5f * i;
            PG_Circle circleShot = Main.Object.SpawnProjectileGenerator<PG_Circle>();
            circleShot.transform.position = this.transform.position;
            circleShot.transform.SetParent(this.transform);
            circleShot.Initialize(this, "Bullet_2_KSJ", 30, 1, 2.5f, startAngle);
            circleShot.Shot();

            _projectileGenerator = circleShot;
        }
        yield return new WaitUntil(() => _projectileGenerator == null);
        yield break;
    }

    private IEnumerator CoShot_3()
    {
        PG_Fan fanShot = Main.Object.SpawnProjectileGenerator<PG_Fan>();
        fanShot.transform.position = this.transform.position;
        fanShot.transform.SetParent(this.transform);
        fanShot.Initialize(this, "Bullet_1_KSJ", 12, 0.6f, 3.2f, 340, -140);
        fanShot.Shot();
        _projectileGenerator = fanShot;
        yield return new WaitUntil(() => _projectileGenerator == null);
        fanShot = Main.Object.SpawnProjectileGenerator<PG_Fan>();
        fanShot.transform.position = this.transform.position;
        fanShot.transform.SetParent(this.transform);
        fanShot.Initialize(this, "Bullet_2_KSJ", 12, 0.6f, 3.2f, 200, 140);
        fanShot.Shot();
        _projectileGenerator = fanShot;
        yield return new WaitUntil(() => _projectileGenerator == null);
        yield break;
    }
    private IEnumerator CoShot_4()
    {
        PG_Circle circleShot = Main.Object.SpawnProjectileGenerator<PG_Circle>();
        circleShot.transform.position = this.transform.position;
        circleShot.transform.SetParent(this.transform);
        circleShot.Initialize(this, "Bullet_3_KSJ", 20, 0, 5);
        circleShot.Shot();
        yield break;
    }
    
}