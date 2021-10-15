using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �v���C���[�𐧌䂷��R���|�[�l���g
public class Player : MonoBehaviour
{
    public float m_speed; // �ړ��̑���

    // ���t���[���Ăяo�����֐�
    private void Update()
    {
        // ���L�[�̓��͏����擾����
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // ���L�[��������Ă�������Ƀv���C���[���ړ�����
        var velocity = new Vector3(h, v) * m_speed;
        transform.localPosition += velocity;

        // �v���C���[����ʊO�ɏo�Ȃ��悤�Ɉʒu�𐧌�����
        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        // �v���C���[�̃X�N���[�����W���v�Z����
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // �v���C���[���猩���}�E�X�J�[�\���̕������v�Z����
        var direction = Input.mousePosition - screenPos;

        // �}�E�X�J�[�\�������݂�������̊p�x���擾����
        var angle = Utils.GetAngle(Vector3.zero, direction);
        
        // �v���C���[���}�E�X�J�[�\���̕���������悤�ɂ���
        var angles = transform.localEulerAngles;
        Debug.Log(angles);
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        //Debug.Log(direction);
    }

    
}