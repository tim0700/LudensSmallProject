using UnityEngine;

/// <summary>
/// Ÿ�� ���潺 ������ Ÿ�� ��ġ ������ �����ϴ� Ŭ����
/// ��ġ "�Ǽ� ������ ����"ó�� �����ϸ�, �÷��̾ ���콺�� �ø��� �ð��� �ǵ���� �����մϴ�.
/// </summary>
public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;        // ������ ǥ���ϴ� ��������Ʈ ������ (���� ����� �׸��� ��)
    [SerializeField] private Color hoverColor;         // ���콺�� �÷��� �� ǥ���� ���� (���̶���Ʈ ����)
    [SerializeField] private Color sellHoverColor = Color.red;    // 판매 모드에서 호버 시 색상

    // === �����̺� ������ ===
    private GameObject tower;       // �� ������ ��ġ�� Ÿ�� (����� ������ ����)
    private Color startColor;       // ���� ������ ����ϴ� ���� (�⺻ ���·� ���ư��� ����)

    /// <summary>
    /// ���� ���� �� �ʱ� ����
    /// ��ġ "������ ���� ������ �������� ���δ�" �Ͱ� �����ϴ�.
    /// </summary>
    private void Start()
    {
        startColor = sr.color;  // ���� ������ �⺻ �������� ����
    }
    /// <summary>
    /// ���콺�� �� ���� ���� �ö���� �� ȣ��
    /// ��ġ "���� ���� �����⸦ �÷��� �����ϴ�" ȿ���� �ݴϴ�.
    /// </summary>
    private void OnMouseEnter()
    {
        // 판매 모드일 때는 타워가 있는 곳에서만 하이라이트
        if (BuildManager.main.IsSellMode())
        {
            if (tower != null)
            {
                sr.color = sellHoverColor;  // 판매 전용 하이라이트 색상
            }
        }
        else
        {
            // 일반 모드에서는 비어있는 곳에서만 하이라이트
            if (tower == null)
            {
                sr.color = hoverColor;  // 일반 하이라이트 색상
            }
        }
    }

    /// <summary>
    /// ���콺�� �� ������ ����� �� ȣ��
    /// ��ġ "�����⸦ ġ���� ���� ���·� �ǵ�����" �Ͱ� �����ϴ�.
    /// </summary>
    private void OnMouseExit()
    {
        sr.color = startColor;  // ���� �������� ����
    }

    private void OnMouseDown()
    {
        // 판매 모드일 때의 처리
        if (BuildManager.main.IsSellMode())
        {
            SellTower();
            return;
        }

        // 일반 모드: 타워 건설
        BuildTower();
    }

    /// <summary>
    /// 타워를 건설하는 메서드
    /// </summary>
    private void BuildTower()
    {
        // 이미 타워가 있다면 건설 불가 (중복 건설 방지)
        if (tower != null) return;

        // BuildManager에서 현재 선택된 타워 정보를 가져오기
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You can't afford this tower");
            return; // 타워를 살 돈이 없는 경우 아무 것도 하지 않음
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost); // 타워 건설 비용 지불

        // 선택된 타워를 이 위치에 생성 (건설 완료)
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 타워를 판매하는 메서드
    /// </summary>
    private void SellTower()
    {
        // 타워가 없다면 판매할 것이 없음
        if (tower == null)
        {
            Debug.Log("판매할 타워가 없습니다.");
            return;
        }

        // 타워 옵젝매에서 타워 정보 찾기 (타워 비용 계산을 위해)
        Tower towerInfo = GetTowerInfoFromPrefab(tower);
        
        if (towerInfo != null)
        {
            int sellPrice = towerInfo.GetSellPrice();
            LevelManager.main.IncreaseCurrency(sellPrice); // 환불 금액 지급
            Debug.Log($"타워 판매! {sellPrice}원 환불받았습니다.");
        }

        // 타워 게임오브젝트 제거
        Destroy(tower);
        tower = null;
    }

    /// <summary>
    /// 타워 프리팩으로부터 타워 정보를 찾는 메서드
    /// </summary>
    /// <param name="towerGameObject">찾을 타워 게임오브젝트</param>
    /// <returns>매칭되는 타워 정보</returns>
    private Tower GetTowerInfoFromPrefab(GameObject towerGameObject)
    {
        // BuildManager의 타워 리스트에서 매칭되는 타워 찾기
        Tower[] towers = BuildManager.main.GetAllTowers();
        
        foreach (Tower tower in towers)
        {
            // 프리팩 이름으로 비교 (인스턴스는 이름 뒤에 (Clone)이 붙음)
            if (towerGameObject.name.Contains(tower.prefab.name))
            {
                return tower;
            }
        }
        
        Debug.LogWarning("타워 정보를 찾을 수 없습니다: " + towerGameObject.name);
        return null;
    }
}