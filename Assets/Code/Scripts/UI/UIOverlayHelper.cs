using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 블러 오버레이가 버튼 클릭을 방해하지 않도록 하는 스크립트
public class UIOverlayHelper : MonoBehaviour
{
    private void Awake()
    {
        // 이 오브젝트의 Image 컴포넌트에서 raycastTarget을 false로 설정
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = false;
        }
        
        // 이 오브젝트의 모든 자식 Image 컴포넌트에서도 raycastTarget을 false로 설정
        Image[] childImages = GetComponentsInChildren<Image>();
        foreach (Image childImage in childImages)
        {
            childImage.raycastTarget = false;
        }
    }
}
