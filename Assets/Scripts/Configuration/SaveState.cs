public class SaveState
{
    public int cans = 120;
    public int bags = 130;
    public int bottles = 140;
    private int completedLevel = 3;
    public int swordOwned = 0;
    private bool staticMode = false;

    public bool StaticMode
    {
        get
        {
            return staticMode;
        }

        set
        {
            staticMode = value;
        }
    }
    public int CompletedLevel
    {
        get
        {
            return completedLevel;
        }

        set
        {
            completedLevel = value;
        }
    }
}