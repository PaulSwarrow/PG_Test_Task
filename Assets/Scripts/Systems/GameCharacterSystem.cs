using System.Collections.Generic;
using DefaultNamespace.Data;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class GameCharacterSystem : IGameSystem
    {
        private GameCharacterActor prefab;
        private List<GameCharacterActor> pool = new List<GameCharacterActor>();
        private HashSet<GameCharacter> list = new HashSet<GameCharacter>();

        public void Init()
        {
            prefab = GameManager.GameProperties.characterActorPrefab;
        }

        public void Start()
        {
        }

        public void Destroy()
        {
        }

        public GameCharacter Spawn(Vector3 position, Vector3 lookDirection)
        {
            var character = new GameCharacter(CreateActor(position, lookDirection), pool);
            character.DestroyEvent += OnCharacterDestroyed;
            list.Add(character);
            return character;
        }

        private GameCharacterActor CreateActor(Vector3 position, Vector3 lookDirection)
        {
            var rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            if (pool.Count == 0) return Object.Instantiate(prefab, position, rotation);
            var actor = pool.Shift();
            actor.gameObject.SetActive(true);
            actor.transform.position = position;
            actor.transform.rotation = rotation;
            return actor;
        }

        private void OnCharacterDestroyed(GameCharacter character)
        {
            list.Remove(character);
        }
    }
}