public class MBullet
{
    private float _speed = 10f;
    private int _damage = 0;
    private float _penetration = 0f;

    public float Speed
    {
        get { return _speed; }

        set { _speed = value; }
    }

    public int Damage
    {
        get { return _damage; }

        set { _damage = value; }
    }

    public float Penetration
    {
        get { return _penetration; }

        set { _penetration = value; }
    }
}
