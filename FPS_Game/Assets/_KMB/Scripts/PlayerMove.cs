using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    CharacterController cc; //캐릭터 컨트롤러 컴포넌트


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
        dir = Camera.main.transform.InverseTransformDirection(dir);
        //transform.Translate(dir.normalized * speed * Time.deltaTime);

        //하늘/땅 뚫고다니는 문제가있음
        //간단한 방법 : rigid body 붙이기 => 물론안씀
        //유사한 캐릭터 컨트롤러 사용해서 해결
        cc.Move(dir * speed * Time.deltaTime);

    }
}
