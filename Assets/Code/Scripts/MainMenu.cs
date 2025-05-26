using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 게임 시작 버튼 클릭시
    public void StartGame()
    {
        // SampleScene (메인 게임 씬) 로드
        SceneManager.LoadScene("SampleScene");
    }

    // 게임 종료 버튼 클릭시
    public void QuitGame()
    {
        Debug.Log("게임을 종료합니다.");
        
        #if UNITY_EDITOR
            // 에디터에서 실행중일 때
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 빌드된 게임에서 실행중일 때
            Application.Quit();
        #endif
    }
}
