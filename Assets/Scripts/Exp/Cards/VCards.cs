using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VCards : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardText = null;
    [SerializeField] private Image _cardImage = null;

    private MCards _myModel;
    private PCards _presenter;
    public MCards MyModel
    {
        get { return _myModel; }
        set { _myModel = value; }
    }

    public PCards Presenter
    {
        get { return _presenter; }
        set { _presenter = value; }
    }

    public void UpgradeStat()
    {
        _cardImage.color = Color.white;
        _cardText.text = _myModel.MyType.ToString();
    }

    public void PointerEnter()
    {
        _cardImage.color = Color.green;
    }

    public void PointerExit()
    {
        _cardImage.color = Color.white;
    }

    public void PointerDown()
    {
        _presenter.Upgrade(this);
    }
}
