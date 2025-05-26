using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Resume 버튼 클릭시 - 게임 재개
    public void Resume()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    // Restart 버튼 클릭시 - 게임 재시작
    public void Restart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    // Exit 버튼 클릭시 - 메인 메뉴로
    public void ExitToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}
