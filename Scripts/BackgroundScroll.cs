using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("텍스쳐의 스크롤 속도가 얼마나 빨라야 하는가?")]
    public float scrollSpeed;

    [Header("References")]
    public MeshRenderer meshRenderer;               // 메테리얼에 직접적으로 속도 조절하기 위해 필요
    void Start()
    {
        
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.Instance.CalculateGameSpeed() / 20 * Time.deltaTime , 0);
        // 메쉬렌더러에있는.메테리얼.메인텍스처의오프셋 += 새로운 벡터2값(스크롤스피드 * 프레임과 프레임 사이의 초 , 0)
        //                                          벡터 2 (x값, y값)
        // 플랫폼 가로 길이가 20이기 때문에 20을 나눠주는 것이 좋음
    }
}
