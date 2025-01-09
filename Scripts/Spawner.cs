using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    // 세팅을 활용해 한 스크립트를 커스텀하여 재사용 할 수 있게 함
    
    [Header("References")]
    public GameObject[] gameObjects;
    void OnEnable()                     // 오브젝트가 활성화 될 때 마다 호출되는 메서드
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));        // Invoke 최초 호출
        // Spawn 메서드를(불러오기) Random.Range를 이용해 minSpawnDelay와 maxSpawnDelay 사이의 값만큼 호출
    }

    void OnDisable()
    {
        CancelInvoke();             // Invoke 함수 취소
    }
    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        // 랜덤오브젝트 변수에 게임오브젝트 배열 속에 있는 프리펩 아무거나 랜덤으로 집어넣기(0부터 배열길이 사이의 숫자에 해당하는 프리펩)
        Instantiate(randomObject, transform.position, Quaternion.identity); 
        // 2D 게임이라 회전값을 넣을 필요가 없지만, 요구하고 있기 때문에 회전값을 표현하는 Quaternion.identity값 입력
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));    
        // Invoke 메서드 호출, Spawn 메서드가 2초에 한 번씩 호출 (Spawn메서드에서 본인 자동 호출)
        // Invoke() == 클래스에서 특정 메서드를 불러올 수 있도록 해줌
    }
}
