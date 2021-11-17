using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private Color[] DangerColors = new Color[8];
    [SerializeField] private Image Danger;

    private TMP_Text _textDisplay;
    private Button _button;
    private Action<Box> _changeCallback;

    public int RowIndex { get; private set; }
    public int ColumnIndex { get; private set; }
    public int ID { get; private set; }
    public int DangerNearby { get; private set; }
    public bool IsDangerous { get; private set; }
    public bool IsActive { get { return _button != null && _button.interactable; } }

    public void Setup(int id, int row, int column)
    {
        ID = id;
        RowIndex = row;
        ColumnIndex = column;
    }

    public void Charge(int dangerNearby, bool danger, Action<Box> onChange)
    {
        _changeCallback = onChange;
        DangerNearby = dangerNearby;
        IsDangerous = danger;
        ResetState();
    }

    public void Reveal()
    {
        if (_button != null)
        {
            _button.interactable = false;
        }

        if (_textDisplay != null)
        {
            _textDisplay.enabled = true;
        }
    }

    public void StandDown()
    {
        if (_button != null)
        {
            _button.interactable = false;
        }

        if (Danger != null)
        {
            Danger.enabled = false;
        }

        if (_textDisplay != null)
        {
            _textDisplay.enabled = false;
        }
    }

    public void OnClick()
    {
        if(_button != null)
        {
            _button.interactable = false;
        }

        if(IsDangerous && Danger != null)
        {
            Danger.enabled = true;
        }
        else if(_textDisplay != null)
        {
            _textDisplay.enabled = true;
        }

        _changeCallback?.Invoke(this);
    }

    private void Awake()
    {
        _textDisplay = GetComponentInChildren<TMP_Text>(true);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        ResetState();
    }

    private void ResetState()
    {
        if (Danger != null)
        {
            Danger.enabled = false;
        }

        if (_textDisplay != null)
        {
            if (DangerNearby > 0)
            {
                _textDisplay.text = DangerNearby.ToString("D");
                _textDisplay.color = DangerColors[DangerNearby-1];
            }
            else
            {
                _textDisplay.text = string.Empty;
            }

            _textDisplay.enabled = false;
        }

        if (_button != null)
        {
            _button.interactable = true;
        }
    }
}
