using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public int minPointsForGoodEnding;
    public int minPointsForOkayEnding;
    public GameObject activeDoorFrame; //doorframe the player is currently trying to get through
    private GameObject previousDoorFrame; //holds on to the previous doorframe for checkpoint usage
    private GameObject[] torchSources; //holds all of the torchSources in the level
    private List<int> torchCountDefaults;
    private int points; // used to keep track of how many points the player has towards various dungeon endings

    //private List<GameObject> destroyableObjects;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(12, 11, true);
        previousDoorFrame = activeDoorFrame;
        torchSources = GameObject.FindGameObjectsWithTag("TorchSource");
        torchCountDefaults = new List<int>();
        createTorchCountDefaults();
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setActiveDoor(GameObject doorFrame)
    {
        previousDoorFrame = activeDoorFrame;
        activeDoorFrame = doorFrame;

        //Debug.Log("Active DF: " + activeDoorFrame.name);
        //Debug.Log("Previous DF: " + previousDoorFrame.name);
    }

    public GameObject getActiveDoorFrame()
    {
        return activeDoorFrame;
    }

    public GameObject getPreviousDoorFrame()
    {
        return previousDoorFrame;
    }

    private void createTorchCountDefaults()
    {
        if (torchSources.Length > 0)
        {
            foreach (GameObject obj in torchSources)
            {
                torchCountDefaults.Add(obj.GetComponent<TorchSource>().getTorchCount());
            }
        }
    }

    //=============================================================================================
    // Methods that handle respawning at a checkpoint
    // We need to reset doorFrames, despawn torches and ghosts, points, as well as reset torch sources
    //=============================================================================================

    public void handleCheckpoint()
    {
        despawnObjects();
        resetTorchSources();
        resetPreviousDoorFrame();
    }

    public void despawnObjects()
    {
        // Destroy All Torches
        GameObject[] torchObjects = GameObject.FindGameObjectsWithTag("Torch");
        if (torchObjects.Length > 0)
        {
            foreach (GameObject obj in torchObjects)
            {
                obj.GetComponent<Torch>().destroyT();
            }
        }

        // Destroy All Ghosts
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyObjects.Length > 0)
        {
            foreach (GameObject obj in enemyObjects)
            {
                Destroy(obj);
            }
        }
    }

    public void resetTorchSources()
    {
        if (torchSources.Length > 0)
        {
            for (int x = 0; x < torchSources.Length; x++)
            {
                torchSources[x].GetComponent<TorchSource>().setTorchCount(torchCountDefaults[x]);
            }
        }
    }

    public void resetPreviousDoorFrame()
    {
        previousDoorFrame.GetComponentInChildren<OutsideCollider>().setIsDespawned(false);
        previousDoorFrame.GetComponentInChildren<OutsideCollider>().playerBlock.SetActive(false);
        activeDoorFrame = previousDoorFrame;
    }

    //=============================================================================================
    // Methods that handle the points system
    //=============================================================================================

    public int getPoints()
    {
        return points;
    }

    public void addPoints(int addedPoints)
    {
        points += addedPoints;
        Debug.Log(points);
    }

    public void setPoints(int newPoints)
    {
        points = newPoints;
    }

    public void endDungeon()
    {
        if (points >= minPointsForGoodEnding)
        {
            SceneManager.LoadScene(4);
        }
        else if (points >= minPointsForOkayEnding)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
}
