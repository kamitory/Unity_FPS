using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    CharacterController cc; //캐릭터 컨트롤러 컴포넌트

    public float gravity = -20;
    public int maxJump = 2;
    float velocityY;        //낙하속도( 벨로시티는 방향과 힘을 가지고있다. )
    float jumpPower = 10f;
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        Vector3 dir = new Vector3(h, 0, v);
        //dir.Normalize(); 
        //대각선 이동속도를 상하좌우이동과 동일 , 게임에 따라 대각선이동이 빠르게하는경우도있음

        // transform.Translate(dir.normalized * speed * Time.deltaTime);

        //카메라가 보는방향으로 이동해야한다
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.Translate(dir.normalized * speed * Time.deltaTime);

        //하늘/땅 뚫고다니는 문제가있음
        //간단한 방법 : rigid body 붙이기 => 물론안씀
        //유사한 캐릭터 컨트롤러 사용해서 해결
        //cc.Move(dir * speed * Time.deltaTime);


        //중력적용하기
        velocityY += gravity * Time.deltaTime;
        dir.y = velocityY;
        cc.Move(dir * speed * Time.deltaTime);


        //캐릭터 점프
        //점프를 누르면수직속도에 점프파워를 넣는다
        //땅에 닿으면 0으로 초기화
        if (cc.isGrounded)//땅에닿음?
        {
        }
        if (cc.collisionFlags == CollisionFlags.Below)     //Sides중간//Above위//Below아래
        {
            velocityY = 0;
            jumpCount = 0;
        }
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            velocityY = jumpPower;
            jumpCount++;
        }


        //if( cc.isGrounded)
        //if (cc.collisionFlags == CollisionFlags.Below)     //Sides중간//Above위//Below아래
        //{
        //    velocityY = 0;
        //    jumpCount = 0;
        //}
        //else
        //{
        //    velocityY += gravity * Time.deltaTime;
        //    dir.y = velocityY;
        //}
        //if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        //{
        //    velocityY = jumpPower;
        //    jumpCount++;
        //}
        //cc.Move(dir * speed * Time.deltaTime);

    }
}
