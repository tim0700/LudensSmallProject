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
        sr.color = hoverColor;  // ���̶���Ʈ �������� �����Ͽ� "���� ����"���� ǥ��
    }

    /// <summary>
    /// ���콺�� �� ������ ����� �� ȣ��
    /// ��ġ "�����⸦ ġ���� ���� ���·� �ǵ�����" �Ͱ� �����ϴ�.
    /// </summary>
    private void OnMouseExit()
    {
        sr.color = startColor;  // ���� �������� ����
    }

    /* === Ÿ�� ��ġ ��� (���� �̱���) ===
     * �Ʒ� �ڵ�� ���߿� Ÿ�� �ý����� �ϼ��Ǹ� Ȱ��ȭ�� �����Դϴ�.
     * ��ġ "������ �ǹ��� ����" ��ɰ� �����ϴ�.
     */
    /*
    private void OnMouseDown()
    {
        // �̹� Ÿ���� �ִٸ� ���� ���� ���� (�ߺ� �Ǽ� ����)
        if (tower != null) return;

        // BuildManager���� ���� ���õ� Ÿ�� ������ ������
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You can't afford this tower");
            return; // Ÿ���� ���õ��� ���� ��� �ƹ� �͵� ���� ����
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost); // Ÿ�� �Ǽ� ��� ����

        // ���õ� Ÿ���� �� ��ġ�� ���� (�Ǽ� ����)
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
    */
}