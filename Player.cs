
using System;

namespace reaktionsspelet {
    public class Player {
        
        public string Name { get; set; }        
        public long Highscore { get { return _highscore; } set { UpdateHighscore(value); }}
        private long _highscore;

        public Player(bool login) {
            Player p;
            if (login) {                
                p = Data.UserLogin();                    
            } else {
                p = Data.CreateUser();
            }

            this.Name = p.Name;
            this._highscore = p.Highscore;
        }

        public Player(string name, int highscore) {            
            Name = name;
            _highscore = highscore;
        }

        private void UpdateHighscore(long newScore) {
            _highscore = newScore;
            Data.UpdateHighscore(this.Name, _highscore);
        }
    }
}