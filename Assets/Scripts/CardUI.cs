using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private Image _image;
    private RectTransform _rectTransform;
    public int Index;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetPos(Vector2 pos, int index)
    {
        _rectTransform.anchoredPosition = pos;
        Index = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
