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
            actor.ActivatedEvent += OnActorActivated;
            actor.DeactivatedEvent += OnActorDeactivated;
        }
        
        private void OnActorActivated()
        {
            actor.character.Inventory.UpdateEvent += UpdateView;
            UpdateView();
        }
        
        private void OnActorDeactivated()
        {
            actor.character.Inventory.UpdateEvent -= UpdateView;
        }

        private void OnDestroy()
        {
            actor.ActivatedEvent -= OnActorActivated;
            actor.DeactivatedEvent -= OnActorDeactivated;
        }

        private void UpdateView()
        {
            if (currentGrenade)
            {
                GameManager.ObjectSpawner.Destroy(currentGrenade);
            }

            if (!actor.character.Inventory.HasGrenade)
            {
                currentGrenade = null;
                return;
            }

            var grenadeConfig = actor.character.Inventory.CurrentGrenadeType;
            currentGrenade =
                GameManager.ObjectSpawner.Spawn(grenadeConfig.prefab, holder.position, holder.rotation, holder);
        }
    }
}