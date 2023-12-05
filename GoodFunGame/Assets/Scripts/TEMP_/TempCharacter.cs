using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotPattern
{
    Circle,
    Fan,
    A,
}

public class TempCharacter : Player
{
    public ShotPattern pattern = ShotPattern.Fan;
    public int projectileCount = 10;
    public float projectileSpeed = 5;
    public float projectileStartAngle;
    public float projectileSpreadAngle;
    public float shotTime = 1;
    public float basicShotSpawnTime = 0.5f;
    public int petals = 2;
    public int bulletsPerPetal = 10;
    public float angleIncrementPerBullet = 10;
    public float timeBetweenBullets = 0.1f;
    private Coroutine coShot;

    void Start()
    {
        StartCoroutine(BasicShot());
    }

    protected override void Update()
    {

        // 이동 임시
        float x = UnityEngine.Input.GetAxisRaw("Horizontal");
        float y = UnityEngine.Input.GetAxisRaw("Vertical");
        Input = new(x, y);
        Velocity = this.Input.normalized * 20;
        this._rigidbody.MovePosition(_rigidbody.position + Velocity * Time.deltaTime);


        // 공격 임시
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
            //// Projectile 프리팹 생성 임시.
            //// ObjectManager에서 Spawn<Projectile>을 통해 생성하게끔!
            //GameObject newObject = Main.Resource.InstantiatePrefab("Projectile_TEMP.prefab");

            //// Projectile 기본 정보 설정 임시.
            //newObject.transform.position = this.transform.position;

            //// Projectile 투사체 정보 설정 임시.
            //Projectile projectile = newObject.GetComponent<Projectile>();
            //projectile.SetInfo(null, 10, 1.1f, 2);

            //// Projectile 운동 정보 설정: 커서가 있는 방향으로 날아가게끔
            //Vector2 direction = (Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - this.transform.position).normalized;
            //float speed = 3;
            //projectile.SetVelocity(direction * speed);
        }
    }

    private IEnumerator BasicShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(basicShotSpawnTime);

            Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
            projectile.SetInfo(this, Damage, 1, 8);
            projectile.SetVelocity(Vector2.up * projectileSpeed);
        }
    }

    private void Shot()
    {
        //switch (pattern)
        //{
        //    case ShotPattern.Circle:
        //        coShot ??= StartCoroutine(CoShot_Circle());
        //        break;
        //    case ShotPattern.Fan:
        //        coShot ??= StartCoroutine(CoShot_FanShape());
        //        break;
        //    case ShotPattern.A:
        //        coShot ??= StartCoroutine(CoShot_Petal());
        //        break;
        //}
    }

    //private IEnumerator CoShot_Circle()
    //{
    //    float deltaAngle = 2 * Mathf.PI / projectileCount;
    //    float deltaTime = shotTime / projectileCount;
    //    float startAngle = projectileStartAngle * Mathf.Deg2Rad;
    //    for (int i = 0; i < projectileCount; i++)
    //    {
    //        float angleX = Mathf.Cos(i * deltaAngle + startAngle);
    //        float angleY = Mathf.Sin(i * deltaAngle + startAngle);
    //        Vector2 direction = new Vector2(angleX, angleY).normalized;


    //        Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
    //        projectile.SetInfo(null, 10, 1.1f, 2);
    //        projectile.SetVelocity(direction * projectileSpeed);

    //        if (deltaTime > 0) yield return new WaitForSeconds(deltaTime);
    //    }
    //    coShot = null;
    //    yield break;
    //}

    //private IEnumerator CoShot_FanShape()
    //{
    //    if (projectileCount <= 1)
    //    {
    //        coShot = null;
    //        yield break;
    //    }
    //    float startAngle = projectileStartAngle * Mathf.Deg2Rad; // 부채꼴의 시작 각도
    //    float endAngle = (projectileStartAngle + projectileSpreadAngle) * Mathf.Deg2Rad; // 부채꼴의 끝 각도
    //    float deltaAngle = (endAngle - startAngle) / (projectileCount - 1); // 탄막 사이의 각도 차이
    //    float deltaTime = shotTime / projectileCount; // 탄막 사이의 시간 간격

    //    for (int i = 0; i < projectileCount; i++)
    //    {
    //        float angle = startAngle + i * deltaAngle; // 현재 탄막의 각도
    //        float angleX = Mathf.Cos(angle);
    //        float angleY = Mathf.Sin(angle);
    //        Vector2 direction = new Vector2(angleX, angleY).normalized;

    //        Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
    //        projectile.SetInfo(null, 10, 1.1f, 2);
    //        projectile.SetVelocity(direction * projectileSpeed);

    //        if (deltaTime > 0) yield return new WaitForSeconds(deltaTime);
    //    }
    //    coShot = null;
    //    yield break;
    //}


    //private IEnumerator CoShot_Petal()
    //{
    //    float angleIncrement = 360f / petals;
    //    float currentAngle = 0f;

    //    for (int i = 0; i < petals; i++)
    //    {
    //        for (int j = 0; j < bulletsPerPetal; j++)
    //        {
    //            float angleRad = (currentAngle + j * angleIncrementPerBullet) * Mathf.Deg2Rad;
    //            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

    //            Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
    //            projectile.SetInfo(null, 10, 1.1f, 2);
    //            projectile.SetVelocity(direction * projectileSpeed);

    //            if (j < bulletsPerPetal - 1)
    //            {
    //                yield return new WaitForSeconds(timeBetweenBullets);
    //            }
    //        }
    //        currentAngle += angleIncrement;
    //    }
    //    coShot = null;
    //    yield break;
    //}
}