using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float speed = 150; //회전속도 ( Time.DeltaTime을 통해 1초에 150도 회전 )
    float angleX, angleY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(h, v, 0);
        //회전은 각각의 축을 기반으로 회전됨
        //Vector3 dir = new Vector3(-v, h, 0);

        //transform.Rotate(dir * speed * Time.deltaTime);

        //유니티엔진에서 제공하는 함수를 사용함에 있어서
        //Translate함수는 쓰는데 큰불편은없지만
        //회전을 담당하는 Rotate함수를 사용하면
        //우리가 제어하기 힘들다
        //인스펙터창의 로테이션값은 우리가 보기편하게 오일러각도로 표시괴지만
        //내부적으로는 쿼터니온으로 회전처리중이다
        //쿼터니온을 사용하는 이유는 짐벌락현상을 방지할 수 있기때문
        //회전을 직접제어 할때는 Rotate()를 사용하지 않고
        //트랜스폼의 오일러앵글을 사용하면 된다.

        // P = P0 +vt;
        //transform.position += dir * speed * Time.deltaTime;
        //각도또한 마찬가지
        //transform.eulerAngles += dir * speed * Time.deltaTime;
        //카메라 문제 (-90~90) 고정,고정해제 하는 문제잇음
        //직접 회전각도를 제한해서 처리

        //Vector3 angle = transform.eulerAngles;
        //angle += dir * speed * Time.deltaTime;
        //if (angle.x > 60) angle.x = 60;
        //if (angle.x < -60) angle.x = -60;
        //transform.eulerAngles = angle;

        //여기에는 또 문제가있다
        //유니티 내부적으로 -각도는 360더해서 처리함
        //내가 만든 각도를 가지고 계산처리해야한다

        angleX += h * speed * Time.deltaTime;
        angleY += v * speed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60, 60);

        transform.eulerAngles = new Vector3(-angleY, angleX, 0);
    }
}
