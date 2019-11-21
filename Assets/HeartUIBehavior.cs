using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIBehavior : MonoBehaviour
{
    public enum Hearts
    {
        Empty = 0,
        Half = 1,
        Full = 2
    }
    public List<Sprite> heartsSprite;
    public Image heartImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeartState(Hearts state)
    {
        heartImage.sprite = heartsSprite[(int)state];
    }
}
