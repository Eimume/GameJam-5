using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public bool isMoveBackwardTile = false; // ��˹���Ҫ�ͧ����繪�ͧ�����ѧ�������
    public int moveBackwardSteps = 3; // �ӹǹ��ͧ����¡�Ѻ (㹡óշ���繪�ͧ�����)

    public bool isDamageTile = false; // ��˹���Ҫ�ͧ����繪�ͧŴ���ʹ�������
    public int damageAmount = 10; // �ӹǹ���ʹ���Ŵŧ�������ش�������ͧ���

    public bool isShopTile = false; // ��˹���Ҫ�ͧ����繪�ͧ��ҹ����������
}
