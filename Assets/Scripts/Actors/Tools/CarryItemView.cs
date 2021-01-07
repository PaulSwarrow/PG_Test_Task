using System;
using DefaultNamespace.Configs;
using UnityEngine;

namespace DefaultNamespace.Tools
{
    public class CarryItemView : MonoBehaviour
    {
        [SerializeField] private Transform holder;
        private GameCharacterActor actor;

        private GrenadeActor currentGrenade;
        
        private void Awake()
        {
            actor = GetComponent<GameCharacterActor>();
        }

        private void OnEnable()
        {
            actor.Inventory.UpdateEvent += UpdateView;
            UpdateView();

        }

        private void OnDisable()
        {
            actor.Inventory.UpdateEvent -= UpdateView;
        }
        
        private void UpdateView()
        {
            if (currentGrenade)
            {
                GameManager.ObjectSpawner.Destroy(currentGrenade);
            }

            if (!actor.Inventory.HasGrenade)
            {
                currentGrenade = null;
                return;
            }
            var grenadeConfig = actor.Inventory.CurrentGrenadeType;
            currentGrenade = GameManager.ObjectSpawner.Spawn(grenadeConfig.prefab, Vector3.zero, Quaternion.identity, holder);
        }
    }
}