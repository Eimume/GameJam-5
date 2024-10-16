using System.Collections;
using UnityEngine;

public class sumlong : MonoBehaviour
{
    public Transform[] tiles; // Array �ͧ���˹觢ͧ���Ъ�ͧ���
    public int currentTileIndex = 0; // ���˹�������鹢ͧ������
    public float moveSpeed = 2f; // ��������㹡������͹���ͧ������

    public GameObject upModel; // ��������Ѻ�������͹�����
    public GameObject downModel; // ��������Ѻ�������͹���ŧ
    public GameObject leftModel; // ��������Ѻ�������͹������
    public GameObject rightModel; // ��������Ѻ�������͹�����

    private bool isMoving = false;

    // ��ҧ�ԧ��ѧ���ͧ��ҧ�
    public Camera gameCamera; // ���ͧ����ѡ
    public Camera shopCamera; // ���ͧ��ҹ���

    // ��ҧ�ԧ��ѧ UI ��ҹ���
    public GameObject shopUI;

    void Start()
    {
        // ��͹���ŷ�����������������
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);
        upModel.SetActive(true);

        // ��Ǩ�ͺ��ҡ��ͧ��� UI �١��駤�����
        if (gameCamera != null)
            gameCamera.gameObject.SetActive(true);
        if (shopCamera != null)
            shopCamera.gameObject.SetActive(false);
        if (shopUI != null)
            shopUI.SetActive(false);
    }

    public IEnumerator MovePlayer(int steps)
    {
        isMoving = true;

        while (steps > 0)
        {
            if (currentTileIndex + 1 >= tiles.Length)
            {
                // ��ͧ�ѹ�������Թ�Թ�ͺࢵ�ͧ tiles
                break;
            }

            Vector3 targetPosition = tiles[currentTileIndex + 1].position;

            // ����¹���ŵ����ȷҧ�������͹����͹������Թ
            ChangeModel();

            // ����͹���价���ͧ�Ѵ价��йԴ
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentTileIndex++; // �������˹觷��� 1

            steps--;
            yield return new WaitForSeconds(0.2f); // ˹�ǧ������硹����������١������͹���Ѵਹ
        }

        // ��Ǩ�ͺ��ͧ�������ѧ�ҡ�������͹����������
        CheckSpecialTile();

        isMoving = false;
    }

    public IEnumerator MoveBackward(int steps)
    {
        while (steps > 0)
        {
            if (currentTileIndex - 1 < 0)
            {
                // ��ͧ�ѹ�������¡�Ѻ仹��¡��� 0
                break;
            }

            Vector3 targetPosition = tiles[currentTileIndex - 1].position;

            // ����¹���ŵ����ȷҧ�������͹����͹������Թ
            ChangeModel();

            // ����͹����Ѻ价���ͧ�Ѵ价��йԴ
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentTileIndex--; // Ŵ���˹觷��� 1
            steps--;
            yield return new WaitForSeconds(0.2f); // ˹�ǧ������硹����������١�ö�¡�Ѻ�Ѵਹ
        }

        // ��Ǩ�ͺ��ͧ�������ѧ�ҡ��ö�¡�Ѻ�������
        CheckSpecialTile();

        isMoving = false;
    }

    private void ChangeModel()
    {
        Vector3 movementDirection = Vector3.zero;
        if (currentTileIndex + 1 < tiles.Length)
        {
            movementDirection = tiles[currentTileIndex + 1].position - (currentTileIndex > 0 ? tiles[currentTileIndex].position : transform.position);
        }

        // �Դ���ŷ�������͹
        upModel.SetActive(false);
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);

        // ��Ǩ�ͺ��ȷҧ�������͹���
        if (movementDirection.y > 0)
        {
            upModel.SetActive(true); // ����͹�����
        }
        else if (movementDirection.y < 0)
        {
            downModel.SetActive(true); // ����͹���ŧ
        }
        else if (movementDirection.x > 0)
        {
            rightModel.SetActive(true); // ����͹�����
        }
        else if (movementDirection.x < 0)
        {
            leftModel.SetActive(true); // ����͹������
        }
    }

    private void CheckSpecialTile()
    {
        SpecialTile specialTile = tiles[currentTileIndex].GetComponent<SpecialTile>();
        if (specialTile != null)
        {
            if (specialTile.isMoveBackwardTile)
            {
                StartCoroutine(MoveBackward(specialTile.moveBackwardSteps));
            }
            else if (specialTile.isDamageTile)
            {
                // Ŵ���ʹ������
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(specialTile.damageAmount);
                }
            }
            else if (specialTile.isShopTile)
            {
                // �Դ��ҹ���
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        if (gameCamera != null && shopCamera != null)
        {
            gameCamera.gameObject.SetActive(false);
            shopCamera.gameObject.SetActive(true);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }

        // �س����ö�����ѧ��ѹ��� � �������Ǣ�ͧ�Ѻ��ҹ��������� �� �����ش�������͹��� ���͡���ʴ���ͤ���
        Debug.Log("�Դ��ҹ���");
    }

    public void CloseShop()
    {
        if (gameCamera != null && shopCamera != null)
        {
            shopCamera.gameObject.SetActive(false);
            gameCamera.gameObject.SetActive(true);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        Debug.Log("�Դ��ҹ���");
    }
}
