using UnityEngine;

/// <summary>
/// 타워 디펜스 게임의 타워 설치 구역을 관리하는 클래스
/// 마치 "건설 가능한 토지"처럼 동작하며, 플레이어가 마우스를 올리면 시각적 피드백을 제공합니다.
/// </summary>
public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;        // 구역을 표시하는 스프라이트 렌더러 (땅의 모양을 그리는 붓)
    [SerializeField] private Color hoverColor;         // 마우스를 올렸을 때 표시할 색상 (하이라이트 색상)

    // === 프라이빗 변수들 ===
    private GameObject tower;       // 이 구역에 설치된 타워 (현재는 사용되지 않음)
    private Color startColor;       // 원래 색상을 기억하는 변수 (기본 상태로 돌아가기 위해)

    /// <summary>
    /// 게임 시작 시 초기 설정
    /// 마치 "토지의 원래 색깔을 사진으로 찍어두는" 것과 같습니다.
    /// </summary>
    private void Start()
    {
        startColor = sr.color;  // 현재 색상을 기본 색상으로 저장
    }

    /// <summary>
    /// 마우스가 이 구역 위에 올라왔을 때 호출
    /// 마치 "토지 위에 돋보기를 올려서 강조하는" 효과를 줍니다.
    /// </summary>
    private void OnMouseEnter()
    {
        sr.color = hoverColor;  // 하이라이트 색상으로 변경하여 "선택 가능"임을 표시
    }

    /// <summary>
    /// 마우스가 이 구역을 벗어났을 때 호출
    /// 마치 "돋보기를 치워서 원래 상태로 되돌리는" 것과 같습니다.
    /// </summary>
    private void OnMouseExit()
    {
        sr.color = startColor;  // 원래 색상으로 복구
    }

    /* === 타워 설치 기능 (현재 미구현) ===
     * 아래 코드는 나중에 타워 시스템이 완성되면 활성화할 예정입니다.
     * 마치 "토지에 건물을 짓는" 기능과 같습니다.
     */
    /*
    private void OnMouseDown()
    {
        // 이미 타워가 있다면 새로 짓지 않음 (중복 건설 방지)
        if (tower != null) return;

        // BuildManager에서 현재 선택된 타워 정보를 가져옴
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You can't afford this tower");
            return; // 타워가 선택되지 않은 경우 아무 것도 하지 않음
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost); // 타워 건설 비용 차감

        // 선택된 타워를 이 위치에 생성 (건설 실행)
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
    */
}