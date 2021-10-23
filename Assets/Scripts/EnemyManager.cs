using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class EnemyManager : MonoBehaviour
    {
        public Enemy[] m_enemyPrefabs;
        public float m_interval;    //�G�̏o���Ԋu
        private float m_timer;  //�o���^�C�~���O���Ǘ�����^�C�}�[

        void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer < m_interval) return;

            m_timer = 0;

            var enemyIndex = Random.Range(0, m_enemyPrefabs.Length);
            var enemyPrefabs = m_enemyPrefabs[enemyIndex];
            var enemy = Instantiate(enemyPrefabs);
            var respawnType = (RESPAWN_TYPE)Random.Range(0, (int)RESPAWN_TYPE.SIZEOF);

            enemy.Init(respawnType);
        }
    }
}