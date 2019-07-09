using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(PlayerManager))]
public class Managers : MonoBehaviour
{
    public static InventoryManager Inventory {get; private set;}
    public static PlayerManager Player {get; private set;}

    private List<IGameManager> _startSequence;

    void Awake()
    {
        Inventory = GetComponent<InventoryManager>();
        Player = GetComponent<PlayerManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Inventory);
        _startSequence.Add(Player);

        StartCoroutine(StartupManagers());
    }

    IEnumerator StartupManagers() {
        foreach(IGameManager manager in _startSequence) {
            manager.Startup();
        }
        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while(numReady != numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence) {
                if(manager.status == ManagerStatus.Started) {
                    numReady++;
                }
                print(numReady);
            }

            if(numReady > lastReady) {
                Debug.Log(numReady + "/" + numModules + " managers successfully started up");
            }
            yield return null;
        }
        Debug.Log("All managers started up");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
