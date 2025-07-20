public class CellData
{
    public int Amount { get { return amount; } }
    private int amount;

    public CellData(int num)
    {
        SetResourceAmount(num);
    }

    public void SetResourceAmount(int num)
    {
        amount = num;
    }
}
