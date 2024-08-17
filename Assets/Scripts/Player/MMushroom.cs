public class MMushroom
{
    private VEnemy _currentEnemy = null;

    private int _cooldown = 1000;
    private int _currentLvl = 0;

    private int _penetration = 0;
    private int _shoot = 1;
    private int _lazer = 1;

    private int _damage = 6;
    private float _shootDistance = 10;

    private bool _canUpgrade = true;

    public VEnemy CurrentEnemy
    {
        get { return _currentEnemy; }

        set { _currentEnemy = value; }
    }

    public int Cooldown
    {
        get { return _cooldown; }

        set { _cooldown = value; }
    }

    public int Damage
    {
        get { return _damage; }

        set { _damage = value; }
    }

    public float ShootDistance
    {
        get { return _shootDistance; }

        set { _shootDistance = value; }
    }

    public int CurrentLvl
    {
        get { return (_currentLvl); }

        set { _currentLvl = value; }
    }

    public bool CanUpgrade
    {
        get { return (_canUpgrade); }

        set { _canUpgrade = value; }
    }

    public int Penetration
    {
        get { return _penetration; }

        set { _penetration = value; }
    }

    public int Shoot
    {
        get { return _shoot; }

        set { _shoot = value; }
    }

    public int Lazer
    {
        get { return _lazer; }

        set { _lazer = value; }
    }
}
