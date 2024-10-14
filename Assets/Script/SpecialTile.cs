using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public int stepsToMoveBack = 1; // �ӹǹ���Ƿ���ͧ�����ѧ������׹���躹��ͧ�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered special tile"); // ���� Debug.Log ���ʹ١�õ�Ǩ�Ѻ
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.MovePlayer(-stepsToMoveBack);
            }
        }
    }

}
