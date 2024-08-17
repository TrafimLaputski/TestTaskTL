using UnityEngine;

public class VEnemy : MonoBehaviour
{
    private PEnemy _presenter = null;
    private MEnemy _myModel = null;

    public MEnemy MyModel
    {
        get { return _myModel; }
        
        set { _myModel = value; }
    }

    public PEnemy Presenter
    {
        get { return _presenter; }

        set { _presenter = value; }
    }

    public void TakeDamage(int damage)
    {
        _presenter.TakeDamage(damage, this);
    }
}
