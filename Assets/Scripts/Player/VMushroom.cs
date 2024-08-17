using UnityEngine;

public class VMushroom : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private GameObject _arrowUp = null;
    [SerializeField] private GameObject _maxLabel = null;
    private MMushroom _myModel = null;
    private PMushroom _presenter = null;
    public MMushroom MyModel
    {
        get { return _myModel; }

        set { _myModel = value; }
    }

    public PMushroom Presenter
    {
        get { return _presenter; }
        set { _presenter = value; }
    }

    private void Start()
    {
        LabelsUpdate();
    }

    private void LabelsUpdate()
    {
        if (_myModel.CanUpgrade)
        {
            _arrowUp.SetActive(true);
            _maxLabel.SetActive(false);
        }
        else
        {
            _arrowUp.SetActive(false);
            _maxLabel.SetActive(true);
        }
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = Color.green;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (_myModel.CanUpgrade)
        {
            _presenter.Upgrade(this);
        }

        LabelsUpdate();
    }
}
