using UnityEngine;
using UnityEngine.Rendering;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;
    public SpriteRenderer SpriteRenderer;
    public int LiveNum;
    void Start()
    {
        
    }
    void Update()
    {
        if(GameManager.Instance.Lives >= LiveNum)
        {
            SpriteRenderer.sprite = OnHeart;
        }
        else
        {
            SpriteRenderer.sprite = OffHeart;
        }
    }
}
