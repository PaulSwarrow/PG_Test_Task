﻿using System;
using System.Collections.Generic;
using DefaultNamespace.Configs;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Data
{
    public class Inventory
    {
        private class GrenadeSlot
        {
            public GrenadeConfig type;
            public float amount;
        }

        public event Action UpdateEvent;

        private List<GrenadeSlot> Grenades = new List<GrenadeSlot>();
        private int grenadeType;


        public void NextGrenade()
        {
            grenadeType++;
            if (grenadeType >= Grenades.Count) grenadeType = 0;
        }

        public void PrevGrenade()
        {
            grenadeType++;
            if (grenadeType >= Grenades.Count) grenadeType = 0;
        }

        public void AddGrenade(GrenadeConfig type)
        {
            if (Grenades.TryFind(slot => slot.type == type, out var slot))
            {
                slot.amount++;
            }
            else
            {
                Grenades.Add(new GrenadeSlot
                {
                    type = type,
                    amount = 1
                });
            }
        }

        public void UseGrenade()
        {
            if (Grenades.Count == 0) return;
            var slot = Grenades[grenadeType];
            slot.amount--;
            if (slot.amount <= 0) Grenades.RemoveAt(grenadeType);
            grenadeType = Mathf.Min(grenadeType, Grenades.Count - 1);
        }

        public GrenadeConfig CurrentGrenadeType => Grenades.Count > 0 ? Grenades[grenadeType].type : null;
    }
}