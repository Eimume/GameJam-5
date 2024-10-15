using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // �ԧ����ѧ Transform �ͧ������
    public Vector3 offset;    // ������ҧ�����ҧ���ͧ�Ѻ������
    private Camera cameraComponent;

    void Start()
    {
        // �ѧ�Ѻ����ͧ����繡��ͧ��ѡ������������
        cameraComponent = GetComponent<Camera>();

        if (cameraComponent != null)
        {
            // �Դ�����ҹ���ͧ��� � ����� MainCamera
            Camera[] allCameras = Camera.allCameras;
            foreach (Camera cam in allCameras)
            {
                if (cam != cameraComponent && cam.CompareTag("MainCamera"))
                {
                    cam.gameObject.SetActive(false);
                }
            }

            // ��駡��ͧ����� Main Camera
            cameraComponent.tag = "MainCamera";
        }
    }

    void Update()
    {
        // �����˹觡��ͧ��������������� offset
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
