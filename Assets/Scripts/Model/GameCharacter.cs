﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Data
{
    public class GameCharacter
    {
        public event Action<GameCharacter> DestroyEvent;
        public Inventory Inventory;
        private GameCharacterActor actor;

        public GameCharacter(GameCharacterActor prefab)
        {
            Inventory = new Inventory();
            actor = GameManager.ObjectSpawner.Spawn(prefab, Vector3.zero, Quaternion.identity);
            actor.Activate(this);
        }

        public Vector3 Position => actor.transform.position;

        public bool Aiming;
        public Vector3 AimPoint;
        public Vector3 Move;
        public Vector3 Direction;


        public void ThrowCurrentItem()
        {
        }

        public void Destroy()
        {
            GameManager.ObjectSpawner.Destroy(actor);
            Inventory = null;
            actor = null;
            DestroyEvent?.Invoke(this);
        }

        public void SetPosition(Vector3 position, Vector3 lookDirection)
        {
            actor.transform.position = position;
            actor.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}