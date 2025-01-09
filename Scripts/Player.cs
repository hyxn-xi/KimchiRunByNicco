using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]                    // inspector 창에서 더 깔끔하게 보기 위해 헤더 작성
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;     // 리지드바디 접근 가능
    public Animator PlayerAnimator;         // 애니메이터 접근 가능
    public BoxCollider2D PlayerCollider;    // 콜라이더 접근 가능

    private bool isGrounded = true;
    public bool isInvincible = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)          // 스페이스키를 누르는 순간 && isGrounded가 true일 때
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            // y축 방향으로 JumpForce만큼의 힘을 추가 (ForceMode2D.Impulse는 순간적인 힘을 뜻함)
            isGrounded = false;
            // 플레이어가 더 이상 땅에 있지 않으므로 false로 바꿔주기
            PlayerAnimator.SetInteger("State", 1);
            // 애니메이터에 있는 State의 값을 1로 바꿔주기 == 점프 애니메이션 실행(달리기에서 점프로 애니메이션 전환)
        }
    }
    void OnCollisionEnter2D(Collision2D collision)      // 충돌 감지 메서드
        {
            if(collision.gameObject.name == "Platform")  // 충돌한 게임 오브젝트의 이름이 Platform인 경우
            {
                if(!isGrounded)                         // 플레이어가 땅에 닿지 않았을 때만
                {
                    PlayerAnimator.SetInteger("State", 2);  // 점프에서 착지로 애니메이션 전환
                }
                isGrounded = true;                      // 플레이어가 땅에 닿았으므로 true로 전환
            }
        }
    public void KillPlayer() 
    { 
        PlayerCollider.enabled = false;                 // 콜라이더 비활성화(지면과 충돌하지 않음)
        PlayerAnimator.enabled = false;                 // 애니메이션 비활성화
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);  // 마지막에 점프 한 번
    }
    void Hit() 
    { 
        GameManager.Instance.Lives -= 1;                                     // 플레이어 목숨 1 감소
    }
    void Heal() { GameManager.Instance.Lives += Mathf.Min(3, GameManager.Instance.Lives + 1); }   
    // Mathf.Min메서드 사용해 플레이어 목숨 1 증가(3이 상한선)
    // Mathf.Min == 두 값 중 더 작은 값 반환(3을 최대치로 설정, 그 이상 회복하지 않도록)
    void StartInvincible(){ isInvincible = true; Invoke("StopInvincible", 5f); }      
    // 플레이어 무적 상태로. 5초 뒤에 스탑인빈시블 메서드 호출, 무적 상태 종료
    void StopInvincible(){ isInvincible = false; }      // 플레이어 무적 상태 풀기
    void OnTriggerEnter2D(Collider2D collider)          // 충돌 감지 메서드
    {
        if(collider.gameObject.tag == "enemy")          // 플레이어와 적이 충돌한 경우
        {
            if(!isInvincible)
            {
                Destroy(collider.gameObject);           // 무적상태가 아닌 경우에만 충돌 시 삭제
                Hit();
            }
        }
        else if(collider.gameObject.tag == "food")      // 플레이어와 음식이 충돌한 경우
        {
            Destroy(collider.gameObject);               // 음식 삭제
            Heal();
        }
        else if(collider.gameObject.tag == "golden")    // 플레이어와 골든배추가 충돌한 경우
        {
            Destroy(collider.gameObject);               // 음식 삭제
            StartInvincible();                          // 무적 상태 시작
        }
    }
}
