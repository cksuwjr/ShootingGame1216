using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    private List<IBackgroundScroller> scrollObjects;
    [SerializeField] private float scrollSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        scrollObjects = InterfaceFinder.FindObjectsOfInterface<IBackgroundScroller>();

        //SetScrollSpeed(2.5f); 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var scroll in scrollObjects)
        {
            if(scroll != null)
            {
                scroll.Scroll(Time.deltaTime);
            }
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        foreach (var scroll in scrollObjects)
        {
            if (scroll is IBackgroundScroller verticalScroll)
            {
                verticalScroll.SetScrollSpeed(newSpeed);
            }
        }
    }
}
