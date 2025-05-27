using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    // 일시정지 버튼 클릭시
    public void OnPauseButtonClick()
    {
        if (GameManager.Instance != null && GameManager.Instance.GetCurrentState() == GameManager.GameState.Playing)
        {
            GameManager.Instance.PauseGame();
        }
    }
}
