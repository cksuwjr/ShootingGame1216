using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크롤 기능
// 위치 리셋 기능
// 스크롤 속도 변경 기능
public interface IBackgroundScroller
{
    void Scroll(float deltaTime);
    void ResetPosition();
    void SetScrollSpeed(float newSpeed);
}

public class BackgroundScroll : MonoBehaviour, IBackgroundScroller
{
    [SerializeField] private float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0, 12.75f, 0f);

    private float resetPositionY = -12.75f;

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.down * (scrollSpeed * deltaTime);
        if(transform.position.y < resetPositionY )
        {
            ResetPosition();
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = Mathf.Clamp(newSpeed, 0f, 15f);
    }

}
