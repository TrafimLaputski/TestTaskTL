using UnityEngine;

public class VBullet : MonoBehaviour
{
    private MBullet _myModel = null;
    private PBullet _presenter = null;

    public MBullet MyModel
    {
        get { return _myModel; }

        set{ _myModel = value; }
    }

    public PBullet Presenter
    {
        get { return _presenter; }

        set { _presenter = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VEnemy enemy = collision.GetComponent<VEnemy>();

        if (enemy != null && _myModel.Penetration >= 0)
        {
            enemy.TakeDamage(_myModel.Damage);

            _myModel.Penetration--;

            if (_myModel.Penetration < 0)
            {
                _presenter.ReturnBullet(this);
            }
        }
    }
}
