using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x < -12)          // x값이 -12보다 작으면
        {
            Destroy(gameObject);                // 스크립트가 적용된 게임오브젝트 파괴
        }
    }
}
