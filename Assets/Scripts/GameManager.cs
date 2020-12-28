using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Systems;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class Properties
    {
        public GameCharacter characterPrefab;
    }

    [SerializeField] private Properties properties;
    private List<IGameSystem> systems = new List<IGameSystem>();

    public static event Action UpdateEvent;
    public static PlayerController PlayerController { get; private set; }
    public static CharacterSpawnSystem CharacterSpawn { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = InitSystem<PlayerController>();
        CharacterSpawn = InitSystem<CharacterSpawnSystem>();
        
        
        systems.ForEach(system=> system.Init(properties));
        systems.ForEach(system=> system.Start());
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
}
