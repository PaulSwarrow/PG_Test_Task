using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Systems;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class Properties
    {
        public GameCharacterActor characterActorPrefab;
        public LineRenderer trajectoryRenderer;
    }

    [SerializeField] private Properties properties;
    private List<IGameSystem> systems = new List<IGameSystem>();

    public static event Action UpdateEvent;
    public static event Action GizmosEvent;
    public static PlayerController PlayerController { get; private set; }
    public static GameCharacterSystem GameCharacter { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = InitSystem<PlayerController>();
        GameCharacter = InitSystem<GameCharacterSystem>();
        
        
        systems.ForEach(system=> system.Init(properties));
        systems.ForEach(system=> system.Start());
    }

    private void OnDestroy()
    {
        systems.ForEach(system=>system.Destroy());
    }

    private T InitSystem<T>() where T : IGameSystem, new()
    {
        var item = new T();
        systems.Add(item);
        return item;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvent?.Invoke();
    }

    private void OnDrawGizmos()
    {
        GizmosEvent?.Invoke();
    }
}
