using UnityEngine;

namespace DefaultNamespace.Configs
{
    [CreateAssetMenu(fileName = "Grenade", menuName = "Game/GrenadeAsset")]
    public class GrenadeConfig : ScriptableObject
    {
        public GrenadeActor prefab;
        public string name;
        public float radius = 1 ;
        public float damage = 1;

    }
}