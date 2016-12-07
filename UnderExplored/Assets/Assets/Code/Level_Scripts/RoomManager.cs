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
    private List<GameObject> doorFramesSinceCheckpoint; //holds all of the doorFrames the player has passed through since the last checkpoint
    private List<int> torchCountDefaults;
    private int points; // used to keep track of how many points the player has towards various dungeon endings

    //private List<GameObject> destroyableObjects;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(12, 11, true);
        activeDoorFrame = GetComponent<Checkpoints>().getCheckpoint().getNextDoorFrame();
        previousDoorFrame = activeDoorFrame;
        torchSources = GameObject.FindGameObjectsWithTag("TorchSource");
        torchCountDefaults = new List<int>();
        doorFramesSinceCheckpoint = new List<GameObject>();
        createTorchCountDefaults();
        points = GetComponent<Checkpoints>().getCheckpoint().getPoints();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(points);
    }

    public void addDoorFrameSinceCheckpoint(GameObject doorFrame)
    {
        doorFramesSinceCheckpoint.Add(doorFrame);
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
        resetDoorFrames();
        resetPoints();
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

    private void resetTorchSources()
    {
        if (torchSources.Length > 0)
        {
            for (int x = 0; x < torchSources.Length; x++)
            {
                torchSources[x].GetComponent<TorchSource>().setTorchCount(torchCountDefaults[x]);
            }
        }
    }

    private void resetDoorFrames()
    {
        previousDoorFrame = GetComponent<Checkpoints>().getCheckpoint().getNextDoorFrame();
        //previousDoorFrame.GetComponentInChildren<OutsideCollider>().setIsDespawned(false);
        //previousDoorFrame.GetComponentInChildren<OutsideCollider>().playerBlock.SetActive(false);
        if (doorFramesSinceCheckpoint.Count > 0)
        {
            foreach (GameObject doorFrame in doorFramesSinceCheckpoint)
            {
                doorFrame.GetComponentInChildren<OutsideCollider>().setIsDespawned(false);
                doorFrame.GetComponentInChildren<OutsideCollider>().playerBlock.SetActive(false);
            }
        }
        resetDoorFramesSinceLastCheckpoint();
        activeDoorFrame = previousDoorFrame;
    }

    private void resetPoints()
    {
        setPoints(this.GetComponent<Checkpoints>().getCheckpoint().getPoints());
    }

    //To be used after door frames are reset or after a checkpoint is reached
    public void resetDoorFramesSinceLastCheckpoint()
    {
        doorFramesSinceCheckpoint = new List<GameObject>();
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
            SceneManager.LoadScene(2);
        }
        else if (points >= minPointsForOkayEnding)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }
}
