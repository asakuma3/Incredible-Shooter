using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class BackGround : MonoBehaviour
    {
        public Transform m_player;
        public Vector2 m_limit; // �w�i�̈ړ��͈�

        private void Update()
        {

            var pos = m_player.localPosition;
            var limit = Utils.m_moveLimit;

            // �v���C���[�̈ʒu��0 ���� 1 �̒l�ɒu��������
            var tx = 1 - Mathf.InverseLerp(-limit.x, limit.x, pos.x);
            var ty = 1 - Mathf.InverseLerp(-limit.y, limit.y, pos.y);

            // �v���C���[�̌��ݒn����w�i�̕\���ʒu���Z�o����
            var x = Mathf.Lerp(-m_limit.x, m_limit.x, tx);
            var y = Mathf.Lerp(-m_limit.y, m_limit.y, ty);

            // �w�i�̕\���ʒu���X�V����
            transform.localPosition = new Vector3(x, y, 0);
        }
    }
}