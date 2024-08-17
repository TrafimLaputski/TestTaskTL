using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PEnemy : MonoBehaviour
{
    private EnemyPool _poolEnemy = null;
    private PExp _pExp = null;

    private List<VEnemy> _enemyList = new List<VEnemy>();
    private bool _moveNow = true;
    public List<VEnemy> EnemyList
    {
         get { return _enemyList; } 
    }

    [Inject]
    private void Construct(EnemyPool poolEnemy, PExp pExp)
    {
        _poolEnemy = poolEnemy;
        _pExp = pExp;
    }

    void Start()
    {
        CreateEnemy();
    }

    void Update()
    {
        Move();
    }

    private void CreateEnemy()
    {
        VEnemy tempEnemy = _poolEnemy.GetItem();

        if (tempEnemy != null)
        {
            _enemyList.Add(tempEnemy);

            float x = Random.Range(-2.0f, 2.0f);
            Vector3 startPos = new Vector3(x, 5, 0);

            tempEnemy.transform.SetParent(transform);
            tempEnemy.transform.position = startPos;
            tempEnemy.MyModel.Health = 1;
            tempEnemy.Presenter = this;
            tempEnemy.gameObject.SetActive(true);
        }
    }

    private void Move()
    {
        if (_moveNow)
        {
            foreach (VEnemy enemy in _enemyList)
            {
                enemy.transform.position += Vector3.down * enemy.MyModel.Speed * Time.deltaTime;

                if (enemy.transform.position.y <= -2.5)
                {
                    Debug.Log("You Lose");
                    enemy.transform.position = new Vector3(enemy.transform.position.x, 5);
                }
            }
        }
    }

    public void TakeDamage(int damage, VEnemy view)
    {
        view.MyModel.Health -= damage;

        if (view.MyModel.Health <= 0)
        {
            _poolEnemy.Return(view);
            _enemyList.Remove(view);

            _pExp.AddExp(1);

            for (int i = 0; i < 2; i++)
            {
                CreateEnemy();
            }
        }
    }

    public void MovingStatus(bool vakue)
    {
        _moveNow = vakue;
    }
}
