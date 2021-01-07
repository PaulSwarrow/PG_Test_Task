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
            actor.character = this;
            this.pool = pool;
        }
        
        public Vector3 position => actor.transform.position;

        public bool Aiming;
        public Vector3 AimPoint;
        public Vector3 Move;
        public Vector3 Direction;


        public void ThrowCurrentItem()
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