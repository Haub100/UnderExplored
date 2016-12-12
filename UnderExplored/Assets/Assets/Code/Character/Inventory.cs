using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private int Torches;
    private int TorchCapacity;
    private GameObject TorchCount;

    // Use this for initialization
    void Awake()
    {
		setInventory();
    }

	// Seudo Constructors used to manipulate the current inventory on the player via checkpoints
    public void setInventory()
    {
        Torches = 0;
        TorchCapacity = 10;
		TorchCount = GameObject.Find("TorchCount");
		TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
    }
    public void setInventory(int torches, int torchCapacity)
    {
        Torches = torches;
        TorchCapacity = torchCapacity;
		TorchCount = GameObject.Find("TorchCount");
		TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
    }
    public void setInventory(Inventory newInventory)
    {
		Torches = newInventory.getTorches();
		TorchCapacity = newInventory.getTorchCapacity();
		TorchCount = GameObject.Find("TorchCount");
		TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
    }

	// Getters & Setters
    public void setTorches(int torches)
    {
        Torches = torches;
        TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
    }

    public int getTorches()
    {
        return Torches;
    }

	public int getTorchCapacity()
	{
		return TorchCapacity;
	}

	// Torch Placement Inventory Methods
    public bool isFull()
    {
        if (TorchCapacity == Torches)
        {
            return true;
        }
        return false;
    }

    public int addTorches(int torchesAdded)
    {
        if (Torches < TorchCapacity)
        {
            Torches += torchesAdded;
            if (Torches > TorchCapacity)
            {
                Torches = TorchCapacity;
            }
        }
        else
        {
            //torches is at maximum capacity
        }
        TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
        return Torches;
    }

    public int removeTorches(int torchesRemoved)
    {
        if (Torches > 0)
        {
            Torches -= torchesRemoved;
            if (Torches < 0)
            {
                Torches = 0;
            }
        }
        else
        {
            //torches already zero torches
        }
        TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
        return Torches;
    }

    public int torchesNeeded()
    {
        return (TorchCapacity - Torches);
    }
}
