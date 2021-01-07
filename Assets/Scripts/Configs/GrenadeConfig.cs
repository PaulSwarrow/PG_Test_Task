using UnityEngine;

namespace DefaultNamespace.Configs
{
    [CreateAssetMenu(fileName = "Grenade", menuName = "Game/GrenadeAsset")]
    public class GrenadeConfig : ScriptableObject
    {
        public GameObject prefab;
        public string name;
        public float radius = 1 ;
        public float damage = 1;

    }
}