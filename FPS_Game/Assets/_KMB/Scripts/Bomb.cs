using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //폭탄의 역할
    //예전 총알은 생성하면 스스로 날아가다 충돌햇음
    //하지만 폭탄은 스스로 이동X
    //폭탄은 플레이어가 던져야함
    //폭탄은 다른 오브젝트와충돌하면 Booooomb
    public GameObject fxFactory;

    private void OnCollisionEnter(Collision collision)
    {
        //폭발이펙트
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        //혹시 이펙트가 사라지지않는 경우
        //Destroy(fx, 2.0f);  //2초뒤 폭발이펙트 삭제       
        //다른오브젝트삭제
        //자기자신삭제
        Destroy(gameObject);
    }
}
