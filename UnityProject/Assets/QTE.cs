using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Assets.Scripts.Units;

public class QTE : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;
    public Boss boss;
    public int DamageTakenSyncFailed = 20;
    public int DamageToBossSyncSuccess = 30;

    public enum XboxButtons
    {
        None,
        A,
        B,
        X,
        Y,
        R1,
        R2,
        L1,
        L2
    }

    public float MaxDelayBetweenSync = 0.5f;
    private float _firstPlayerPressed;

    public XboxButtons currentButton;
    private XboxButtons _player1Button = XboxButtons.None;
    private XboxButtons _player2Button = XboxButtons.None;

    public Transform buttonUITransform;
    public List<GameObject> buttons;
    public float activeTime = 1f; // qte active time in seconds
    public bool activate = false;
    public new Camera camera;

    private bool QTEWasActive = false;
    private float timer = 0f;
    private bool doneSync = false;

    // Use this for initialization
    void Start()
    {
        buttons.ForEach(x => x.SetActive(false));
        // testomg
        //CheckPlayerButtons(XboxButtons.A, XboxButtons.A, XboxButtons.B);
        //this.QuickTimeEventFailed(this.trigger1.GetComponent<QTETrigger>().playerOne);
    }

    // Update is called once per frame
    void Update()
    {
        var buttonName = currentButton.ToString();
        var button = buttons.FindLast(x => x.name.Equals(buttonName));

        var triggerClass1 = trigger1.GetComponent<QTETrigger>();
        var triggerClass2 = trigger2.GetComponent<QTETrigger>();

        var buttonPos = (trigger1.transform.position + trigger2.transform.position) / 2f;
        var pos = camera.WorldToScreenPoint(buttonPos);




        if (triggerClass1.triggeredByPlayer && triggerClass2.triggeredByPlayer && !QTEWasActive)
        {
            QTEWasActive = true;
            Activate();
        }

        if (activate)
        {
            ReadPlayerInput();

            if (Time.timeSinceLevelLoad - this._firstPlayerPressed > this.MaxDelayBetweenSync)
            {
                // players were too late. Als QTE nog lang leeft moet hier nog gecheckt worden
                //print("Failed sync, too late");
            }
            else if (this._player1Button != XboxButtons.None && this._player2Button != XboxButtons.None && !doneSync)
            {
                CheckPlayerButtons(currentButton, _player1Button, _player2Button);
            }
            if (!button.activeSelf)
            {
                button.transform.position = pos;
                button.SetActive(true);
            }

            timer += Time.deltaTime;

            // QTE still active
            if (timer < activeTime)
            {

            }
            // disable qte
            else
            {
                activate = false;
                timer = 0f;
            }
        }
        else
        {
            if (button.activeSelf)
            {
                button.SetActive(false);
            }
        }
    }

    private void ReadPlayerInput()
    {
        //print("Reading player input");
        if (this._player1Button == XboxButtons.None)
        {
            if (Input.GetButtonDown("P1_A"))
            {
                print("P1 A PRESSED");
                this._player1Button = XboxButtons.A;

                // other player hasn't pressed yet
                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P1_B"))
            {
                print("P1 B PRESSED");
                this._player1Button = XboxButtons.B;

                // other player hasn't pressed yet
                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P1_X"))
            {
                print("P1 X PRESSED");
                this._player1Button = XboxButtons.X;

                // other player hasn't pressed yet
                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P1_Y"))
            {
                print("P1 Y PRESSED");
                this._player1Button = XboxButtons.Y;

                // other player hasn't pressed yet
                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
        }
        if (this._player2Button == XboxButtons.None)
        {
            if (Input.GetButtonDown("P2_A"))
            {
                this._player2Button = XboxButtons.A;
                print("P2 A PRESSED");

                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P2_B"))
            {
                this._player2Button = XboxButtons.B;
                print("P2 B PRESSED");

                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P2_X"))
            {
                this._player2Button = XboxButtons.X;
                print("P2 X PRESSED");

                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
            else if (Input.GetButtonDown("P2_Y"))
            {
                this._player2Button = XboxButtons.Y;
                print("P2 Y PRESSED");

                if (this._firstPlayerPressed == 0)
                    this._firstPlayerPressed = Time.timeSinceLevelLoad;
            }
        }
    }

    public void Activate()
    {
        if (!activate)
        {
            //Array values = Enum.GetValues(typeof(XboxButtons));
            //System.Random random = new System.Random();
            //currentButton = (XboxButtons)values.GetValue(random.Next(values.Length));

            activate = true;
        }
    }

    public void CheckPlayerButtons(XboxButtons correctButton, XboxButtons player1Button, XboxButtons player2Button)
    {
        if (player1Button == correctButton && player2Button == correctButton)
        {
            // damage golem.
            print("SYNC SUCCESSFUL, DAMAGED GOLEM (nope)");
            boss.TakeDamage(DamageToBossSyncSuccess, false);
        }
        else if (player1Button == correctButton && player2Button != correctButton)
        {
            // player2 fucks over player 1
            QuickTimeEventFailed(this.trigger1.GetComponent<QTETrigger>().playerOne);
            print("player2 fucks over player 1");
        }
        else if (player2Button == correctButton && player1Button != correctButton)
        {
            // player1 fucks over player 2
            QuickTimeEventFailed(this.trigger1.GetComponent<QTETrigger>().playerTwo);
            print("player1 fucks over player 2");
        }
        else
        {
            // they failed, damage both players.
            QuickTimeEventFailed(this.trigger1.GetComponent<QTETrigger>().playerOne);
            QuickTimeEventFailed(this.trigger1.GetComponent<QTETrigger>().playerTwo);
        }

        doneSync = true;
    }

    private void QuickTimeEventFailed(GameObject losingPlayer)
    {
        Unit u = losingPlayer.GetComponent<Unit>();
        u.TakeQTEDamage(DamageTakenSyncFailed);
    }

    public void Reset()
    {
        Invoke("SetWasActiveFalse", 1f);
    }

    void SetWasActiveFalse()
    {
        QTEWasActive = false;
        _player1Button = XboxButtons.None;
        _player2Button = XboxButtons.None;

        _firstPlayerPressed = 0f;
        doneSync = false;

        print("Reset buttons and attack");
    }
    //public void ActivateIfTriggered()
    //{
    //    var triggerClass1 = trigger1.GetComponent<QTETrigger>();
    //    var triggerClass2 = trigger2.GetComponent<QTETrigger>();

    //    if (triggerClass1.triggeredByPlayer && triggerClass2.triggeredByPlayer && !activate && !QTEWasActive)
    //    {
    //        activate = true;
    //    }
    //}
}
