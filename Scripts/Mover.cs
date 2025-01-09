using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime; 
        // Vector3의 x값을 왼쪽으로(-1) * moveSpeed와 델타타임을 곱한 속도로
    }
}
