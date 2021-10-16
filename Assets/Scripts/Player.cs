using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float m_speed;
    public Shot m_shotPrefab;
    public float m_shotSpeed;
    public float m_shotAngleRange;  //�V���b�g�̊p�x��
    public float m_shotTimer;   //�e�̔��˃^�C�~���O���Ǘ�
    public int m_shotCount; //���ː�
    public float m_shotInterval;

    private void Update()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var velocity = new Vector3(h, v) * m_speed;
        transform.localPosition += velocity;

        // �v���C���[����ʊO�ɏo�Ȃ��悤�Ɉʒu�𐧌�����
        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        var direction = Input.mousePosition - screenPos;

        // �}�E�X�J�[�\�������݂�������̊p�x���擾����
        var angle = Utils.GetAngle(Vector3.zero, direction);

        // �v���C���[���}�E�X�J�[�\���̕���������悤�ɂ���
        var angles = transform.localEulerAngles;
        Debug.Log(angles);
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        m_shotTimer += Time.deltaTime;
        if (m_shotTimer < m_shotInterval) return;
        m_shotTimer = 0;
        ShotNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);
    }

    //�e�𔭎˂���֐�
    private void ShotNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition;
        var rot = transform.localRotation;
        if (1 < count)
        {
            for (int i = 0; i < count; i++)
            {
                var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);
                var shot = Instantiate(m_shotPrefab, pos, rot);
                shot.Init(angle, speed);
            }
        }
        else if (count == 1)
        {
            var shot = Instantiate(m_shotPrefab, pos, rot);
            shot.Init(angleBase, speed);
        }
    }



}