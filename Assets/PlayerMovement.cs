using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ��������㹡������͹���
    public Transform[] tiles; // ��ͧ��ҧ � ������������ö�Թ���
    private int currentTileIndex = 0; // ��ͧ�Ѩ�غѹ������������

    public IEnumerator MovePlayer(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentTileIndex < tiles.Length - 1)
            {
                currentTileIndex++; // ��ѧ��ͧ�Ѵ�
                yield return StartCoroutine(MoveToTile(currentTileIndex)); // ����͹�����ѧ��ͧ���
            }
            else
            {
                break; // ��ش��Ҷ֧��ͧ�ش����
            }
        }
    }

    private IEnumerator MoveToTile(int tileIndex)
    {
        Vector3 startPosition = transform.position; // ���˹�������鹡�͹����͹���
        Vector3 targetPosition = tiles[tileIndex].position; // ���˹��������

        // ��Ǩ�ͺ��Ҽ���������͹�������᡹ X ���� Y ����ʴ���ȷҧ + ���� -
        if (targetPosition.x > startPosition.x)
        {
            Debug.Log("Moving along +X axis");
        }
        else if (targetPosition.x < startPosition.x)
        {
            Debug.Log("Moving along -X axis");
        }

        if (targetPosition.y > startPosition.y)
        {
            Debug.Log("Moving along +Y axis");
        }
        else if (targetPosition.y < startPosition.y)
        {
            Debug.Log("Moving along -Y axis");
        }

        // �������͹���
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // ������Ѵ�
        }

        // ��Ѻ���˹�����繨ӹǹ���
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private int RollDice()
    {
        return Random.Range(1, 7); // ����١�����Ф׹��Ҽ��Ѿ�� (1-6)
    }
}
