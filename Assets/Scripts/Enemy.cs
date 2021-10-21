using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    //�G�̏o���ʒu�̎��
    public enum RESPAWN_TYPE
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        SIZEOF, //�G�̏o���ʒu�̐�
    }

    public class Enemy : MonoBehaviour
    {

        public Vector2 m_respownPosInside;  //�G�̓����̏o���ʒu
        public Vector2 m_respownPosOutside; //�G�̊O���̏o���ʒu
        public float m_speed;
        public int m_hpMax;
        public int m_exp;
        public int m_damage;

        private int m_hp;
        private Vector3 m_direction;    //�i�s����
        public Explosion m_explosionPrefab;   //�G�̔����G�t�F�N�g

        private void Start()
        {
            m_hp = m_hpMax;
        }
        private void Update()
        {
            transform.localPosition += m_direction * m_speed;
        }

        //�G�̏����o���ʒu�Ɛi�ޕ���
        public void Init(RESPAWN_TYPE respownType)
        {
            var pos = Vector3.zero;
            switch (respownType)
            {
                case RESPAWN_TYPE.UP:
                    pos.x = Random.Range(-m_respownPosInside.x, m_respownPosInside.x);
                    pos.y = Random.Range(-m_respownPosOutside.y, m_respownPosOutside.y);
                    m_direction = Vector2.up;
                    break;

                case RESPAWN_TYPE.RIGHT:
                    pos.x = Random.Range(-m_respownPosInside.x, m_respownPosInside.x);
                    pos.y = Random.Range(-m_respownPosOutside.y, m_respownPosOutside.y);
                    m_direction = Vector2.right;
                    break;

                case RESPAWN_TYPE.DOWN:
                    pos.x = Random.Range(-m_respownPosInside.x, m_respownPosInside.x);
                    pos.y = Random.Range(-m_respownPosOutside.y, m_respownPosOutside.y);
                    m_direction = Vector2.down;
                    break;

                case RESPAWN_TYPE.LEFT:
                    pos.x = Random.Range(-m_respownPosInside.x, m_respownPosInside.x);
                    pos.y = Random.Range(-m_respownPosOutside.y, m_respownPosOutside.y);
                    m_direction = Vector2.left;
                    break;
            }
            transform.localPosition = pos;
        }

        //�e�ɏՓ˂����Ƃ��Ăяo��
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "PLayer")
            {
                var player = col.GetComponent<Player>();
                player.Damage(m_damage);
                return;
            }

            if (col.gameObject.tag == "Shot")
            {
                Debug.Log("����");
                Instantiate(m_explosionPrefab, col.transform.localPosition, Quaternion.identity);
                m_hp--;
                if (0 < m_hp) return;

                Destroy(gameObject);
            }

        }
    }
}