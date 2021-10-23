using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class EnemyManager : MonoBehaviour
    {
        public Enemy[] m_enemyPrefabs;
        public float m_interval;    //敵の出現間隔
        private float m_timer;  //出現タイミングを管理するタイマー

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