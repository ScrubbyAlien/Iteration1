using UnityEngine;

[CreateAssetMenu(fileName = "ItemStore", menuName = "Scriptable Objects/ItemStore")]
public class ItemStore : Stats<int>
{
    [SerializeField]
    private int startingAmount;
    private int currentAmount;

    public bool Use() {
        if (currentAmount > 0) {
            currentAmount--;
            InvokeStatChanged(currentAmount);
            return true;
        }
        return false;
    }

    public bool Use(int number) {
        if (currentAmount - number >= 0) {
            currentAmount -= number;
            InvokeStatChanged(currentAmount);
            return true;
        }
        return false;
    }

    public void Get(int number) {
        currentAmount += number;
        InvokeStatChanged(currentAmount);
    }

    /// <inheritdoc />
    public override int ReadStat() {
        return currentAmount;
    }

    /// <inheritdoc />
    protected override void InitializeStat() {
        currentAmount = startingAmount;
        InvokeStatChanged(currentAmount);
    }
    
}