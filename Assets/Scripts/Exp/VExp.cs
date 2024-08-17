using UnityEngine;
using UnityEngine.UI;
public class VExp : MonoBehaviour
{
    [SerializeField] private Image _progressBar = null;

    private PExp _presenter = null;
    private MExp _myModel = null;

    public PExp Presenter
    {
        get { return _presenter; }
        set { _presenter = value; }
    }

    public MExp MyModel
    {
        get { return _myModel; }
        set { _myModel = value; }
    }
    public void Upgrade()
    {
        float fill = (float)_myModel.CurrentExp / _myModel.NeedExp;

        _progressBar.fillAmount = fill;

        if (fill >= 1)
        {
            _presenter.Upgrade();
        }
    }
}
