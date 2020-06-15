using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //카메라가 플레이어 따라다니기
    //플레이어에게 카메라를 붙여이동해도 상관없음
    //하지만 게임에따라 연출이 필요한경우에 1,3인칭변경
    //또 슈팅게임에서 하위개체가 따라다니는 효과연출
    //현재는 눈역할이라 바로 이동
    
    public Transform target; //카메라가 따라다닐 타겟(플레이어)
    public float followSpeed = 10.0f;
    private bool view = false;

    public Transform targer2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 위치를 강제로 타겟위치에 고정
        //transform.position = target.position;

        SelectView();
        FollowTarget();
    }

    private void SelectView()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            view = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            view = false;
        }
    }

    private void FollowTarget()
    {
        //타겟방향 구하기 ( 벡터의 뺄셈 )
        //방향 = 타겟 - 자신
        Vector3 targetPos;
        if (view) targetPos = target.position;
        else targetPos = targer2.position;


        Vector3 dir = targetPos - transform.position;
        

        dir.Normalize();
        transform.Translate(dir * followSpeed * Time.deltaTime);

        
            //문제점 : 타겟에 도착하면 떨림    ~~카메라 쉐이킹이라고 우기자~~
            if (Vector3.Distance(transform.position, targetPos) < 1.0f)
            {
                transform.position = targetPos;

            //if(view) transform.rotation = Quaternion.Euler(0, 0, 0);
            //else transform.rotation = Quaternion.Euler(30, 0, 0);
            }

        //}
        //else
        //{
        //    if (Vector3.Distance(transform.position, target.position) < 8.0f && Vector3.Distance(transform.position, target.position) > 7.0f)
        //    {
        //        transform.position = target.position + new Vector3(0,5,-5);
        //        transform.rotation = Quaternion.Euler(30, 0, 0);
        //    }
        //}
    }
}
