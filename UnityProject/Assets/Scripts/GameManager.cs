namespace Assets.Scripts
{
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Scripts.Helpers;
    using Assets.Scripts.Units;

    using UnityEngine;
    public class GameManager : MonoBehaviour
    {
        public bool GameOver;
        public bool PlayersHaveToFight;
        public bool Victory;

		public AudioPlayer audioplayerRef; 


        public Unit Boss;
        public Unit[] Players;

        private bool _loggedVictory, _loggedFight, _loggedLost;

        private bool _checking;
        public int DelayToCheck = 2;

		public GameObject VictoryP1, VictoryP2, Defeat, PlayerAttackStage, pauseScreen;  

		public bool isPaused = false; 

        void Start()
        {
            this.Invoke("StartChecking", this.DelayToCheck);
        }

        private void StartChecking()
        {
            this._checking = true;
        }

        public void Update()
        {
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P)){
				if (isPaused == false){
					pauseScreen.SetActive (true);
					Time.timeScale = 0; 
					isPaused = true; 
				}else {
					pauseScreen.SetActive (false);
					Time.timeScale = 1; 
					isPaused = false; 
				}
			}

            if (!this._checking)
                return;
            if (this.CountPlayersAlive() == 1 && this.Boss.currentHealth == 0)
            {
                Unit wonPlayer = this.GetPlayersAlive()[0];
                if (!this._loggedVictory)
                {
                    this._loggedVictory = true;
                    LogHelper.Log(typeof(GameManager), wonPlayer + " has won the game");
                }
                this.Victory = true;
                this.PlayersHaveToFight = false;

                if (wonPlayer.transform.name == "Player1")
                {
					Invoke ("P1WinF", 1f); 

                }
                else if (wonPlayer.transform.name == "Player2")
                {
					Invoke ("P2WinF", 1f); 
                }
            }
            else if (this.Boss.currentHealth == 0 && this.CountPlayersAlive() == this.Players.Length)
            {
                if (!this._loggedFight)
                {
                    this._loggedFight = true;
                    LogHelper.Log(typeof(GameManager), "Boss died. Players now have to fight eachother!");
                }
                this.PlayersHaveToFight = true;
				PlayerAttackStage.SetActive (true); 
				audioplayerRef.PlayerFightMusicStartF (); 
            }
            else if (this.Boss.currentHealth > 0 && !this.PlayersHaveToFight && this.CountPlayersAlive() != this.Players.Length)
            {
                if (!this._loggedLost)
                {
                    this._loggedLost = true;
                    LogHelper.Log(typeof(GameManager), "You lose.");
                }
                this.GameOver = true;
				Invoke ("GameOverF", 1f); 
            }
        }
        
        private List<Unit> GetPlayersAlive()
        {
            return this.Players.Where(go => go.currentHealth > 0).ToList();
        }

        private int CountPlayersAlive()
        {
            return this.Players.Count(go => go.currentHealth > 0);
        }
   
		void P1WinF(){
			VictoryP1.SetActive (true);
			PlayerAttackStage.SetActive (false); 
			Time.timeScale = 0; 
			audioplayerRef.VictoryMusicF (); 
		}
		void P2WinF(){
			VictoryP2.SetActive (true); 
			PlayerAttackStage.SetActive (false); 
			Time.timeScale = 0; 
			audioplayerRef.VictoryMusicF (); 
		}
		void GameOverF(){
			this.GameOver = true;
			Defeat.SetActive (true); 
			Time.timeScale = 0; 
			audioplayerRef.GameOverMusicF (); 
		}


	
	}



}
