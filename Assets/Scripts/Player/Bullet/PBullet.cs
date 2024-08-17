using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class PBullet : MonoBehaviour
{
    private BulletPool _bulletPool = null;
    private List<VBullet> _bullets = new List<VBullet>();
    private bool _flyNow = true;

    [Inject]
    private void Construct(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void SpawnBullet(Vector3 shotPoint, Vector3 enemyPos, int damage, int penetration)
    {
        VBullet newBullet = _bulletPool.GetItem();

        if (newBullet != null)
        {
            newBullet.MyModel.Damage = damage;
            newBullet.MyModel.Penetration = penetration;
            newBullet.Presenter = this;

            newBullet.transform.SetParent(transform);
            newBullet.transform.position = shotPoint;

            Vector3 direction = enemyPos - shotPoint;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            newBullet.gameObject.SetActive(true);
            _bullets.Add(newBullet);
        }
    }

    public void ReturnBullet(VBullet bullet)
    {
        _bulletPool.Return(bullet);
        _bullets.Remove(bullet);
    }

    private void Update()
    {
        if (_flyNow)
        {
            List<VBullet> removeBullets = new List<VBullet>();

            foreach (VBullet bullet in _bullets)
            {
                bullet.transform.position += bullet.transform.up * bullet.MyModel.Speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, bullet.transform.position) >= 20)
                {
                    removeBullets.Add(bullet);
                    break;
                }
            }

            foreach (VBullet bullet in removeBullets)
            {
                ReturnBullet(bullet);
            }
        }
    }

    public void FlyStat(bool value)
    {
        _flyNow = value;
    }
}
