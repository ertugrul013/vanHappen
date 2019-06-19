using UnityEngine;

[System.Serializable]
public class PowerUpManager
{
    public int ActionAmount;

    public int paperTrashCount, greenTrashCount, chemicalTrashCount, plasticTrashCount;

    public GameObject[] Trees;
    private int _treesIndex;
    public GameObject[] FactoryTrash;
    private int _factoryIndex;

    public PowerUpManager()
    {
        _treesIndex = 0;
        _factoryIndex = 0;
    }

    public void PowerUpHandeler(TrashConfig t)
    {
        switch (t._trash.mytype)
        {
            case Trash.Type.paper:
                //shooting
                paperTrashCount++;
                if (paperTrashCount >= 2)
                {
                    PlayerController.instance.bulletCount++;
                    paperTrashCount = 0;
                }
                break;
            case Trash.Type.chemicals:
                //remove trash game2
                chemicalTrashCount++;
                if (chemicalTrashCount >= 5)
                {
                    EnvirmontPowerUp(false, false);
                    chemicalTrashCount = 0;
                }
                break;
            case Trash.Type.green:
                //add tree
                greenTrashCount++;
                if (greenTrashCount >= 2)
                {
                    EnvirmontPowerUp(true, true);
                    greenTrashCount = 0;
                }
                break;
            case Trash.Type.plastic:
                //truck 
                break;
        }
    }

    /// <summary>
    /// handels activating and deactivating gameobject based on the trash that is picked up
    /// </summary>
    /// <param name="_isIncreasing">are the object increasing or decreasing</param>
    /// <param name="_isTree">is the powerup meant for the trees</param>
    /// <returns>Returns if the action is complete</returns>
    private bool EnvirmontPowerUp(bool _isIncreasing, bool _isTree)
    {
        if (_isTree)
        {
            if (_isIncreasing && !Trees[_treesIndex++].activeSelf)
            {
                for (int i = 0; i < ActionAmount; i++)
                {
                    _treesIndex++;
                    Trees[_treesIndex].SetActive(true);
                }
                return true;
            }
            else if (!_isIncreasing && Trees[_treesIndex--].activeSelf)
            {
                for (int i = 0; i < ActionAmount; i++)
                {
                    _treesIndex--;
                    Trees[_treesIndex].SetActive(false);
                }
                return true;
            }
            return false;
        }
        else if (!_isTree)
        {
            if (_isIncreasing && !FactoryTrash[_factoryIndex].activeSelf)
            {
                _factoryIndex++;
                FactoryTrash[_factoryIndex].SetActive(true);
                return true;
            }
            else if (!_isIncreasing && FactoryTrash[_factoryIndex].activeSelf)
            {
                _factoryIndex--;
                FactoryTrash[_factoryIndex].SetActive(false);
                return true;
            }
            return false;
        }
        return false;
    }
}
