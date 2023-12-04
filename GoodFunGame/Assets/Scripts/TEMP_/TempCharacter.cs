using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharacter : Player
{

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
            // Projectile 프리팹 생성 임시.
            // ObjectManager에서 Spawn<Projectile>을 통해 생성하게끔!
            GameObject newObject = Main.Resource.InstantiatePrefab("Projectile_TEMP.prefab");

            // Projectile 기본 정보 설정 임시.
            newObject.transform.position = this.transform.position;

            // Projectile 투사체 정보 설정 임시.
            Projectile projectile = newObject.GetComponent<Projectile>();
            projectile.SetInfo(null, 10, 1.1f, 2);

            // Projectile 운동 정보 설정: 커서가 있는 방향으로 날아가게끔
            Vector2 direction = (Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - this.transform.position).normalized;
            float speed = 3;
            projectile.SetVelocity(direction * speed);
        }
    }

}