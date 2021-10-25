using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class Gem : MonoBehaviour
    {
        public int m_exp;
        public float m_brake = 0.9f;    //�U��΂�Ƃ��̌�����

        private Vector3 m_direction;
        private float m_speed;

        void Update()
        {
            //�U��΂鏈��
            var velocity = m_direction * m_speed;
            transform.localPosition += velocity;
            m_speed *= m_brake;
            //transform.localPosition = Utils.ClampPosition(transform.localPosition); //��΂���ʊO�ɏo�Ȃ�����
        }

        //��΂��o�����鎞�ɏ�����
        public void Init(int score, float dropRangeMin, float dropRangeMax)
        {
            var angle = Random.Range(0, 360);
            var f = angle * Mathf.Deg2Rad;  //���W�A���ɕϊ�
            m_direction = new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0);   //�i�s�����̃x�N�g�����쐬
            m_speed = Mathf.Lerp(dropRangeMin, dropRangeMax, Random.value);
            Destroy(gameObject, 8);
        }
    }
}