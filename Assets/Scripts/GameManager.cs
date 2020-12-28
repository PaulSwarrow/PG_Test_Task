using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Systems;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<IGameSystem> systems = new List<IGameSystem>();
    public static PlayerController PlayerController { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = InitSystem<PlayerController>();
        
        
        systems.ForEach(system=> system.Init());
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
        
    }
}
