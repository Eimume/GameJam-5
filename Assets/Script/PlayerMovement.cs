using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] tiles; // Array �ͧ���˹觢ͧ���Ъ�ͧ���
    public int currentTileIndex = 0; // ���˹�������鹢ͧ������
    public float moveSpeed = 2f; // ��������㹡������͹���ͧ������

    public GameObject upModel; // ��������Ѻ�������͹�����
    public GameObject downModel; // ��������Ѻ�������͹���ŧ
    public GameObject leftModel; // ��������Ѻ�������͹������
    public GameObject rightModel; // ��������Ѻ�������͹�����

    private bool isMoving = false;

    void Start()
    {
        // ��͹���ŷ�����������������
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);
        // ��������Ţ�����������������������ö��Ѻ�������ͧ�����
        upModel.SetActive(true);
    }

    public IEnumerator MovePlayer(int steps)
    {
        isMoving = true;

        // �Թ仵���ӹǹ�����ҡ��÷�����
        while (steps > 0)
        {
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

            if (currentTileIndex >= tiles.Length - 1)
            {
                currentTileIndex = tiles.Length - 1; // ��ͧ�ѹ�������Թ�Թ
                break;
            }

            steps--;
            yield return new WaitForSeconds(0.2f); // ˹�ǧ������硹����������١������͹���Ѵਹ
        }

        // ��ѧ�ҡ�Թ�������� ��Ǩ�ͺ��Ҫ�ͧ�Ѩ�غѹ�繪�ͧ������������
        SpecialTile specialTile = tiles[currentTileIndex].GetComponent<SpecialTile>();
        if (specialTile != null && specialTile.isMoveBackwardTile)
        {
            StartCoroutine(MoveBackward(specialTile.moveBackwardSteps)); // ��¡�Ѻ����ӹǹ����˹�
        }
        else
        {
            isMoving = false;
        }
    }

    public IEnumerator MoveBackward(int steps)
    {
        while (steps > 0)
        {
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
            if (currentTileIndex < 0)
            {
                currentTileIndex = 0; // ��ͧ�ѹ�������¡�Ѻ仹��¡��� 0
                break;
            }

            steps--;
            yield return new WaitForSeconds(0.2f); // ˹�ǧ������硹����������١�ö�¡�Ѻ�Ѵਹ
        }

        isMoving = false;
    }

    private void ChangeModel()
    {
        Vector3 movementDirection = tiles[currentTileIndex + 1].position - (currentTileIndex > 0 ? tiles[currentTileIndex].position : transform.position);

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
}
