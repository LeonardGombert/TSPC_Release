using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class AIManager : MonoBehaviour
    {
        public bool testAI;

        [ShowInInspector, ReadOnly]
        public static List<AgentManager> agents = new List<AgentManager>();
        public List<AgentManager> Agents { get => agents; }

        public void AddMyAgents()
        {
            agents.Clear();
            agents.AddRange(GetComponentsInChildren<AgentManager>());
        }

        void Start()
        {
            if (testAI) StartAllAgents();
        }

        public void StartAllAgents()
        {
            foreach (AgentManager agent in agents) agent.StartAgent();
        }
    } 
}
