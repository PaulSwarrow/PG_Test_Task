using System;
using UnityEngine;

namespace DefaultNamespace.Tools
{
    public class CarryItemView : MonoBehaviour
    {
        [SerializeField] private Transform holder;
        private GameCharacterActor actor;

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
        
        public void UpdateView()
        {
            
        }
    }
}