using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ��������㹡������͹���
    public Transform[] tiles; // ��͵�ҧ������Ф�����ö�Թ���
    private int currentTileIndex = 0; // ��͵���ФûѨ�غѹ

    public GameObject leftAppearance; // ����ʴ��ŷ�ȷҧ����
    public GameObject rightAppearance; // ����ʴ��ŷ�ȷҧ���
    public GameObject upAppearance; // ����ʴ��ŷ�ȷҧ���
    public GameObject downAppearance; // ����ʴ��ŷ�ȷҧŧ

    public int HPplayer = 100; // ��� HP �ͧ������
    public Text Hp; // UI �ʴ���� HP
    public GameObject potioon; // Potion UI
    public GameObject Potoin; // ������ Potion
    public int BuffPotion = 20; // ������� HP ������� Potion

    private Vector3 lastPosition; // ���˹�����ش�ͧ������
    private bool hasStoppedMoving = false; // ����Ҽ�������ش����͹��������������

    // ��������������㹡�õ�Ǩ�ͺ��� Player ����㹺��͡����� Tag "DamageBlock"
    private bool isInDamageBlock = false;

    private void Start()
    {
        lastPosition = transform.position; // ��駤�ҵ��˹��������
    }

    private void UpdateAppearance(GameObject newAppearance)
    {
        // �Դ����ʴ��ŷء��ȷҧ
        leftAppearance.SetActive(false);
        rightAppearance.SetActive(false);
        upAppearance.SetActive(false);
        downAppearance.SetActive(false);

        // �Դ����ʴ��ŷ�ȷҧ����
        newAppearance.SetActive(true);
    }

    public IEnumerator MovePlayer(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentTileIndex < tiles.Length - 1)
            {
                currentTileIndex++; // 价���ͶѴ�
                yield return StartCoroutine(MoveToTile(currentTileIndex)); // ���µ���Ф�价���ͶѴ�
            }
            else
            {
                break; // ��ش�ҡ����շ������Թ
            }
        }
    }

    private IEnumerator MoveToTile(int tileIndex)
    {
        Vector3 startPosition = transform.position; // ���˹��������
        Vector3 targetPosition = tiles[tileIndex].position; // ���˹觷�ͷ���ͧ����

        // ��Ǩ�ͺ��ȷҧ����Ѿവ����ʴ���
        if (targetPosition.x > startPosition.x)
        {
            Debug.Log("Moving along +X axis");
            UpdateAppearance(rightAppearance);
        }
        else if (targetPosition.x < startPosition.x)
        {
            Debug.Log("Moving along -X axis");
            UpdateAppearance(leftAppearance);
        }

        if (targetPosition.y > startPosition.y)
        {
            Debug.Log("Moving along +Y axis");
            UpdateAppearance(upAppearance);
        }
        else if (targetPosition.y < startPosition.y)
        {
            Debug.Log("Moving along -Y axis");
            UpdateAppearance(downAppearance);
        }

        // ����͹������Ф���ѧ��ͷ���˹�
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // �ͨ����Ҩ�����
        }

        // ��Ѻ���˹觵���Ф����ç�Ѻ���˹觷��
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private int RollDice()
    {
        return Random.Range(1, 7); // ����١��� (1-6)
    }

    // �ѧ��ѹ���Ŵ��ѧ���Ե�ͧ����Ф�
    public void TakeDamage(int damage)
    {
        HPplayer -= damage;
        Debug.Log("Player's HP: " + HPplayer);

        if (HPplayer <= 0)
        {
            Debug.Log("Player is dead!");
            // �س����ö�ӡ�ûŴ��͡������� Game Over �����
        }
    }

    // �ѧ��ѹ����Ǩ�ͺ����� Player ���Ѻ���͡����� Tag �� "DamageBlock"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DamageBlock")) // ��Ǩ�ͺ����繺��͡����� Tag "DamageBlock"
        {
            isInDamageBlock = true; // ����������㹺��͡
        }
    }

    // �ѧ��ѹ����Ǩ�ͺ����� Player �͡�ҡ���͡
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DamageBlock")) // ��Ǩ�ͺ����繺��͡����� Tag "DamageBlock"
        {
            isInDamageBlock = false; // �������͡�ҡ���͡
        }
    }

    // ����Ѿവ UI �ͧ HP
    void Update()
    {
        Hp.text = $"HP: {HPplayer}";
        if (HPplayer > 100)
        {
            HPplayer = 100;
        }
        if (HPplayer < 0)
        {
            HPplayer = 0;
        }

        // ����Ҽ�������ش����͹���
        if (transform.position == lastPosition)
        {
            if (!hasStoppedMoving)
            {
                // ��Ҽ�������ش����͹�������ѧ�����Ŵ���ʹ
                if (isInDamageBlock)
                {
                    TakeDamage(10); // Ŵ���ʹ 10 �������ش�����͡
                }
                hasStoppedMoving = true; // ��駤��ʶҹ�������ʹŴ����
            }
        }
        else
        {
            // ��Ҽ����蹡��ѧ����͹���, �Ѿവ���˹�����ش������絡����ش����͹���
            lastPosition = transform.position;
            hasStoppedMoving = false; // ����ʶҹ���������������͹�������
        }
    }

    public void UsePotion() // ����͡���������Ѿ��� UI ������������
    {
        if (HPplayer > 0 && HPplayer < 100)
        {
            HPplayer += BuffPotion;
            Potoin.SetActive(false);
            potioon.SetActive(false);
        }
    }

    public void NoUse() // �Դ˹�ҵ�ҧ
    {
        potioon.SetActive(false);
    }

    public void OpenPotion()
    {
        potioon.SetActive(true);
    }
}