using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    public Button rollDiceButton; // ��������١���
    public TextMeshProUGUI diceResultText; // ��ͤ����ʴ����١���
    public sumlong playerMovement; // ʤ�Ի��������͹���ͧ������
    public float rollDuration = 1.0f; // ��������㹡����ع
    public float rollSpeed = 0.05f; // ��������㹡������¹����Ţ

    private System.Random random = new System.Random();

    void Start()
    {
        // �����ѧ��ѹ������¡����ͻ����١��ԡ
        rollDiceButton.onClick.AddListener(RollDice);
    }

    void RollDice()
    {
        int diceResult = random.Next(1, 7); // ����١���Ẻ 6 ˹��
        StartCoroutine(RollDiceAnimation(diceResult)); // ���¡��ҹ͹�����蹡����ع
    }

    IEnumerator RollDiceAnimation(int finalResult)
    {
        float elapsedTime = 0f;

        // ��ع����ŢẺǹ�������� �����ҨФú���ҷ���˹�
        while (elapsedTime < rollDuration)
        {
            // ����¹����Ţ 1 �֧ 6 ��������
            int currentNumber = (int)Mathf.Ceil(elapsedTime / rollSpeed) % 6 + 1;
            diceResultText.text = "Result: " + currentNumber.ToString(); // ��������� "Result: " �����ҧ��ع

            elapsedTime += rollSpeed;
            yield return new WaitForSeconds(rollSpeed);
        }

        // �����͹�����蹨� ����ʴ����Ѿ���ش����
        diceResultText.text = "Result : " + finalResult.ToString();

        // �觤�Ҩӹǹ���Ƿ������蹵�ͧ�Թ
        StartCoroutine(playerMovement.MovePlayer(finalResult)); // ���¡��ҹ MovePlayer ��ҹ Coroutine
    }
}