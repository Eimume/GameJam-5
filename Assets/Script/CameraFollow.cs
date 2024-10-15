using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // �ԧ����ѧ Transform �ͧ������
    public Vector3 offset;    // ������ҧ�����ҧ���ͧ�Ѻ������

    void Update()
    {
        // �����˹觡��ͧ��������������� offset
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
