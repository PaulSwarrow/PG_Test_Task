using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class CharacterSpawnSystem : IGameSystem
    {
        private GameCharacter prefab;
        private List<GameCharacter> pool = new List<GameCharacter>();

        public void Init(GameManager.Properties properties)
        {
            prefab = properties.characterPrefab;
        }

        public void Start()
        {
            
        }

        public void Destroy()
        {
            
        }

        public GameCharacter Spawn(Vector3 position, Vector3 lookDirection)
        {
            var rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            if (pool.Count == 0) return Object.Instantiate(prefab, position, rotation);
            
            var character = pool.Shift();
            character.gameObject.SetActive(true);
            character.transform.position = position;
            character.transform.rotation = rotation;
            return character;
        }

        public void Destroy(GameCharacter character)
        {
            character.gameObject.SetActive(false);
            pool.Add(character);
        }
    }
}