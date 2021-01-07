using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Data
{
    public class GameCharacter
    {
        public event Action<GameCharacter> DestroyEvent;
        public Inventory Inventory;
        private GameCharacterActor actor;
        private List<GameCharacterActor> pool;

        public GameCharacter(GameCharacterActor actor, List<GameCharacterActor> pool)
        {
            Inventory = new Inventory();
            this.actor = actor;
            this.pool = pool;
        }
        
        public Vector3 position => actor.transform.position;

        public Vector3 Move
        {
            get => actor.Move;
            set => actor.Move = value;
        }

        public Vector3 Direction
        {
            get => actor.Direction;
            set => actor.Direction = value;
        }


        public void ThrowCurrentItem(Vector3 targetPosition)
        {
            
        }

        public void Destroy()
        {
            actor.gameObject.SetActive(false);
            pool.Add(actor);
            
            Inventory = null;
            actor = null;
            DestroyEvent?.Invoke(this);
        }
    }
}