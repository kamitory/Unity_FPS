using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject grenadeFactory;
    public GameObject bulletImpactFactory;
    public float throwPower = 20.0f;   

    private bool grenadeInHand = false;
    GameObject grenade;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //마우스왼쪽버튼 클릭시 레이캐스트로 총알발사
        if(Input.GetMouseButtonDown(0))
        {
            //RaycastHit hitInfo;
            //
            //if( Physics.Raycast(firePoint.position,firePoint.forward,out hitInfo))
            //{
            //    Debug.Log(hitInfo);
            //    Destroy(hitInfo.collider.gameObject);
            //}

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
         //레이 오리진 = 포지션
         //레이 디렉션 = 포워드
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
        
                Debug.Log(hitInfo.transform.name);

                EnemeFSM enemy = hitInfo.collider.GetComponent<EnemeFSM>();
    
                //hitInfo.collider.gameObject.GetComponent<EnemeFSM>().hitDamage(10);
                //hitInfo.transform.GetComponent<EnemeFSM>().hitDamage(10);
                //충돌지점에 이펙트
                GameObject bulletImpact = Instantiate(bulletImpactFactory);
                //부딪힌지점 힛인포안에 정보가있음
                bulletImpact.transform.position = hitInfo.point;

                bulletImpact.transform.forward = hitInfo.point;
                
            }
            ////레이어 마스크 사용 충돌처리
            //// 유니티 내부적으로 속도향상을 위해 비트연선처리함
            ////총 32비트사용 -> 레이어도 32개까지가능
            //int layer = gameObject.layer;
            ////layeer = 1 << 8;
            //
            //layer = 1 << 8 + 1 << 9 | 1 << 12;
            //
            //if(Physics.Raycast(ray, out hitInfo,100,~layer))
            //{
            //    //if많이쓰면 성능 조금이지만 떨어짐 
            //}


            
        }
        //마우스우측버튼 클릭시 수류탄투척
        if (Input.GetMouseButtonDown(1))
        {
            //if (grenadeInHand == false)
            //{
            //    grenadeInHand = true;
            //    grenade = Instantiate(grenadeFactory);
            //    
            //}
            //
            //else
            //{
            //    grenadeInHand = false;
            //}
            GameObject bomb = Instantiate(grenadeFactory);
            bomb.transform.position = firePoint.position;
            //폭탄은 플레이어가 던지기때문에
            //폭탄의 리지드바디를 이용해서 던지면된다
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //전방으로 물리적인 힘을 가한다
            //rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
            //ForceMode.Force => 연속적인 힘을 가한다 (질량영향o)
            //ForceMode.Acceleration => 연속적인 힘을 가한다 (질량영향x)
            //ForceMode.Impulse => 순간적인 힘을가한다. (질량영향o)
            //ForceMode.VelocityChange => 순간적인 힘을가한다 (질량영향x)

            //45도 각도로 발사
            //45도 각도를 주려면 어케해야할까(백터의덧셈)
            Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
            dir.Normalize();
            rb.AddForce(dir * throwPower, ForceMode.Impulse);

        }

    }
}
