using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//몬스터 유한상태 머신
public class EnemeFSM : MonoBehaviour
{
    //public GameObject target;
    //public float speed = 4.0f;
    //
    //private int hpMax = 5;
    //private int hp = 5;

    private bool die;
    //몬스터상태 이넘문
    enum EnemyState
    {
        Idle,Move,Attack,Return,Damaged,Die
    }

    EnemyState state; //몬스터 상태변수

    /// 유용한 기능

    #region"Idle 상태에 필요한변수들"
    #endregion
    #region"Move 상태에 필요한변수들"
    #endregion
    #region"Attack 상태에 필요한변수들"
    #endregion
    #region"Return 상태에 필요한변수들"
    #endregion
    #region"damaged 상태에 필요한변수들"
    #endregion
    #region"Die 상태에 필요한변수들"
    #endregion

    //필요변수
    public float findRange = 15f;   //플레이어 찾는범위
    public float moveRange = 30f;   //시작지점에서 최대이동가능지점
    public float attackRange = 2f;  //공격가능범위
    Vector3 defaultLocation;//몬스터 시작위치

    Transform player; //플레이어 찾기위해( 코드로찾자 드래그앤드랍 일일히 하기힘듬)
    CharacterController cc; //몬스터이동을 위해 캐릭터 컨트롤러

    int hp = 100;       //체력
    int att = 5;        //공격력
    float speed = 5.0f; //이동속도

    //공격딜레이
    float attTime = 2f; //2초에한번공격
    float timer = 0f;   //타이머
    










    // Start is called before the first frame update
    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        //시작지점 저장
        defaultLocation = transform.position;
        //플레이어 트렌스폼 컴포넌트
        player = GameObject.Find("Player").transform;
        //캐릭터 컨트롤러 컴포넌트
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        //플레이어와 일정범위가 되면 이동상태로 변경
        //- 플레이어 찾기 (GameObject.Find(player)) 
        //일정거리(20미터) (Distance 매그니튜드 sqr매그니튜드)
        //- 상태변경



        //if(Vector3.SqrMagnitude(target.transform.position - transform.position)<20f*20f)
        //{
        //state = EnemyState.Move;
        //}
        
        //Vector3 distance = transform.position - player.position;
        //if(distance.magnitude < findRange)
        Vector3 dir = transform.position - player.position;
        //float distance = dir.magnitude; //노말라이즈 절대사용X
        //if(distance < findRange)
        //{
        //
        //}

        if(Vector3.Distance(transform.position, player.position) < findRange)
        {
            state = EnemyState.Move;
            print("상태전환 : Idle -> Move");
        }

        //- 상태전환 출력
        //Debug.Log("Idle");
    }

    private void Move()
    {

        //플레이어를 향해 이동후 공격범위가 되면 번경
        //- 플레이어 추격하도록 처음위치에서 어느정도 벗어가면 돌아가게
        //- 플레이어 처럼 캐릭터 컨트롤러 이용하기
        //- 상태변경

        if(Vector3.Distance(transform.position, defaultLocation)> moveRange)
        {
            state = EnemyState.Return;
            print("상태전환 : Move -> Return");
        }
        else if ( Vector3.Distance(transform.position, player.position)>attackRange)
        {
            //플레이어추격
            //이동방향 (벡터의뺄샘)
            Vector3 dir = (player.position - transform.position).normalized;


            //몬스터가 방향전환안하고 쫓아옴
            //몬스터가 타겟을 바라보도록 하자
            //방법1
            //transform.forward = dir;
            //방법2
            //transform.LookAt(player);

            //좀더 자연스럽게 회전처리
            //transform.forward = Vector3.Lerp(transform.forward, dir, 10 * Time.deltaTime);
            //이방식은 정확히 정면일때 덤블링으로 방향전환하는 문제가있음

            //최종적으로 자연스런 회전처리는 쿼터니온을 사용하게된다
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);


            //캐릭터 컨트롤러 이용해서 이동하기
            //cc.Move(dir * speed * Time.deltaTime);
            //중력이 적용안되는 문제가있다

            //중력문제를 해결하기 위해서 심플무브를 사용한다
            //심플무브는 최소한의 물리가 적용되어 중력문제를 해결할 수 있다.
            //대신 내부적으로 시간처리를 하기때문에 Time.deltaTime을 사용하지 않는다
            cc.SimpleMove(dir * speed);
        }
        else //공격범위안으로
        {
            state = EnemyState.Attack;
            print("상태전환 : Move -> Attack");
        }







        //Vector3 dir = target.transform.position - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime);
        //
        //if ( Vector3.SqrMagnitude(defaultLocation-transform.position)>30f*30f)
        //{
        //state = EnemyState.Return;
        //}
        //else if (Vector3.SqrMagnitude(target.transform.position - transform.position) < 1f)
        //{
        //    state = EnemyState.Attack;
        //}

        //- 상태전환 출력






        //Debug.Log("Move");
    }

    private void Attack()
    {
        //1.플레이어가 공격범위 안에있다면 일정한 시간 간격으로 플레이어 공격
        //2.플레이어가 공격범위를 벗어나면 이동상태(재추격)
        //- 공격범위 1미터
        //Att();
        //- 상태변경

        if(Vector3.Distance(transform.position,player.position)<attackRange)
        {
            //일정시간마다 공격하기
            timer += Time.deltaTime;
            if(timer>attTime) 
            {
                //플레이어의 필요한 스크립트 컴포넌트 가져와서 사용하기
                //player.GetComponent<PlayerMove>().
                print("공격");

                //타이머초기화
                timer = 0f;
            }
        }
        else // 공격범위 벗어나면 추격으로 상태전환 (재추격)
        {
            state = EnemyState.Move;
            print("상태전환 : Attack -> Move");

            //타이머초기화
            timer = 0f;
        }




        //if (Vector3.SqrMagnitude(target.transform.position - transform.position) > 1f)
        //{
        //    state = EnemyState.Move;
        //}

        //- 상태전환 출력
        //Debug.Log("Attack");
    }

    private void Return()
    {
        //1. 몬스터가 플레이어를 추격하더라도 처음위치에서 일정범위를 벗어나면 다시돌아옴
        //- 처음위치에서 일정범위 30미터

        //시작위치까지 도달하지 않을때는 이동
        //도착하면 대기상태로 변경
        if(Vector3.Distance(transform.position, defaultLocation) >0.1f)
        {
            Vector3 dir = (defaultLocation - transform.position).normalized;
            cc.SimpleMove(dir * speed);    
        }
        else
        {
            transform.position = defaultLocation;

            state = EnemyState.Idle;
            print("상태전환 : Return -> Idle");
        }




        //Vector3 dir = defaultLocation - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime);
        ////- 상태변경
        //if( Vector3.SqrMagnitude(defaultLocation - transform.position)<1f)
        //{
        //    transform.position = defaultLocation;
        //    state = EnemyState.Idle;
        //}


        //- 상태 전환 출력
        //Debug.Log("Return");
    }

    //플레이어쪽에서 충돌감지를 할 수 있으니 이함수는 퍼블릭으로
    public void hitDamage(int value)
    {
        //예외처리
        //피격상태거나 죽은상태일때는 데미지중첩x

        if (state == EnemyState.Damaged || state == EnemyState.Die) return;
        
        //체력깍기
        hp -= value;

        //몬스터의 체력디 1+면 피격 
        if(hp>0)
        {
            state = EnemyState.Damaged;
            print("상태전환 : AnyState -> Damaged");
            print(" HP : " + hp);

            Damaged();
        }
        //0이하면사망
        else
        {
            state = EnemyState.Die;
            print("상태전환 : AnyState -> Die");

            Die();

        }

    }





    //피격상태(Any State)
    private void Damaged()
    {
        //코루틴을 사용하자
        //1. 몬스터 체력이 1이상
        //2. 다시 상태를 이전상태로변경
        //- 상태변경
        //- 상태전환 출력

        //피격상태를 처리하기 위한 코루틴을 실행
        StartCoroutine(DamageProc());
    }

    IEnumerator DamageProc()
    {
        //피격 모션 시간 만큼 기다리기
        yield return new WaitForSeconds(1.0f);
        //현재상태를 이동으로 전환
        state = EnemyState.Move;
        print("상태전환 : Damaged -> Move");
    }
    
    //피격상태(Any State)
    private void Die()
    {
        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트를삭제
        //-상태변경
        //- 상태전환 출력 (사망)

        //진행중인 코루틴정지
        StopAllCoroutines();

        //죽음상태를 처리하기위한 코루틴

        StartCoroutine(DieProc());
    }
    
    IEnumerator DieProc()
    {
        //캐릭터 컨트롤러 비활성화 (안해도 큰문제는안됨)
        cc.enabled = false;

        //2초후 자기자신을 제거
        yield return new WaitForSeconds(2.0f);
        print("적 사망");
        Destroy(gameObject);

    }

    private void OnDrawGizmos()
    {
        //공격가능범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어 찾을수 있는범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //이동가능한 최대범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(defaultLocation, moveRange);
    }

}
