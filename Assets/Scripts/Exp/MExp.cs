public class MExp
 {
    private int _currentLevel = 0;
    private int _needExp = 1;
    private int _currentExp = 0;

    public int CurrentLevel
    {
        get { return _currentLevel; }

        set { _currentLevel = value; }
    }

    public int NeedExp
    {
        get { return _needExp; }

        set { _needExp = value; }
    }

    public int CurrentExp
    {
        get { return _currentExp; }
        set { _currentExp = value; }
    }
}
