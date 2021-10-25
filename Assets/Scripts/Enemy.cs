using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        public bool m_isFollow; //�ǔ�����ꍇtrue

        public Explosion m_explosionPrefab;   //�G�̔����G�t�F�N�g
        private float m_explosionTimer = 0f;    //�A���������Ȃ����߂̃^�C�}�[
        private float m_setTimer = 0.05f;    //�����^�C�}�[�̊�l

        public Gem[] m_gemPrefabs;
        public float m_dropRangeMin;    //�A�C�e���̍L����͈�
        public float m_dropRangeMax;

        private void Start()
        {
            m_hp = m_hpMax;
        }
        private void Update()
        {
            m_explosionTimer -= Time.deltaTime;

            if (m_isFollow)
            {
                var angle = Utils.GetAngle(transform.localPosition, Player.m_playerInstance.transform.localPosition);
                var direction = Utils.GetDirection(angle);

                transform.localPosition += direction * m_speed; //�v���C���[����������Ɉړ�
                var angles = transform.localEulerAngles;    //�v���C���[���������������
                angles.z = angle - 90;
                transform.localEulerAngles = angles;
                return;
            }

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
                Instantiate(m_explosionPrefab, col.transform.localPosition, Quaternion.identity);
                Destroy(col.gameObject);

                if (m_explosionTimer < 0)
                {
                    m_explosionTimer = m_setTimer;
                }

                m_hp--;
                if (0 < m_hp) return;

                Destroy(gameObject);

                var exp = m_exp;
                while (1 < exp)
                {
                    var gemPrefabs = m_gemPrefabs.Where(c => c.m_exp <= exp).ToArray();
                    var gemPrefab = gemPrefabs[Random.Range(0, gemPrefabs.Length)];
                    var gem = Instantiate(gemPrefab, transform.localPosition, Quaternion.identity);
                    gem.Init(m_exp, m_dropRangeMin, m_dropRangeMax);
                    exp -= gem.m_exp;
                    Debug.Log(exp);
                }
            }

        }
    }
}