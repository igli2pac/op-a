using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Collections;
using PlayerIO.GameLibrary;
using System.Drawing;
//using System.Xml;
using ServersideGameCode;
//using System.Web;
//using System.Linq;

namespace MyGame
{
   
    public class Player : BasePlayer
    {
        public string Name;
        
    }

    [RoomType("MyCode")]
    public class MyCode : Game<Player>
    {
        // This method is called when an instance of your the game is created
        public override void GameStarted()
        {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine("Game is started: " + RoomId);

            // This is how you setup a timer
            AddTimer(delegate
            {
                // code here will code every 100th millisecond (ten times a second)
            }, 100);

            // Debug Example:
            // Sometimes, it can be very usefull to have a graphical representation
            // of the state of your game.
            // An easy way to accomplish this is to setup a timer to update the
            // debug view every 250th second (4 times a second).
            AddTimer(delegate
            {
                // This will cause the GenerateDebugImage() method to be called
                // so you can draw a grapical version of the game state.
                RefreshDebugView();
            }, 250);
        }

        // This method is called when the last player leaves the room, and it's closed down.
        public override void GameClosed()
        {
            Console.WriteLine("RoomId: " + RoomId);
        }

        // This method is called whenever a player joins the game
        public override void UserJoined(Player player)
        {
            // this is how you send a player a message
            player.Send("hello");

            // this is how you broadcast a message to all players connected to the game
            Broadcast("UserJoined", player.Id);
        }

        // This method is called when a player leaves the game
        public override void UserLeft(Player player)
        {
            Broadcast("UserLeft", player.Id);
        }

        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message)
        {
            switch (message.Type)
            {
                // This is how you would set a players name when they send in their name in a 
                // "MyNameIs" message
                case "MyNameIs":
                    player.Name = message.GetString(0);
                    break;
                case "BroadcastMessage":
                    Broadcast("BroadcastMessage", message.GetString(0));
                    break;
                case "enemy1":
                    sendMessageToAllOtherPlayers(player, message);
                    break;
            }
        }
        private void sendMessageToAllOtherPlayers(Player player, Message message)
        {
            foreach (Player p in Players)
            {
                if (p.Id != player.Id)
                {
                    p.Send(message);
                }
            }
        }


        Point debugPoint;

        // This method get's called whenever you trigger it by calling the RefreshDebugView() method.
        public override System.Drawing.Image GenerateDebugImage()
        {
            // we'll just draw 400 by 400 pixels image with the current time, but you can
            // use this to visualize just about anything.
            var image = new Bitmap(400, 400);
            using (var g = Graphics.FromImage(image))
            {
                // fill the background
                g.FillRectangle(Brushes.Blue, 0, 0, image.Width, image.Height);

                // draw the current time
                g.DrawString(DateTime.Now.ToString(), new Font("Verdana", 20F), Brushes.Orange, 10, 10);

                // draw a dot based on the DebugPoint variable
                g.FillRectangle(Brushes.Red, debugPoint.X, debugPoint.Y, 5, 5);
            }
            return image;
        }

        // During development, it's very usefull to be able to cause certain events
        // to occur in your serverside code. If you create a public method with no
        // arguments and add a [DebugAction] attribute like we've down below, a button
        // will be added to the development server. 
        // Whenever you click the button, your code will run.
        [DebugAction("Play", DebugAction.Icon.Play)]
        public void PlayNow()
        {
            Console.WriteLine("The play button was clicked!");
        }

        // If you use the [DebugAction] attribute on a method with
        // two int arguments, the action will be triggered via the
        // debug view when you click the debug view on a running game.
        [DebugAction("Set Debug Point", DebugAction.Icon.Green)]
        public void SetDebugPoint(int x, int y)
        {
            debugPoint = new Point(x, y);
        }
    }
    [RoomType("matchmaking")]
    public class matchmakingCode : Game<Player>
    {
        public override void GameStarted() {
        }
        public override void GameClosed() { }

        public override void UserJoined(Player player){

            /*HttpResponse abd;
            Dictionary<string, string> post = new Dictionary<string, string> { };
            post.Add("testing", "testing");
            string url = "http://dcrypt.it/decrypt/paste";
            PlayerIO.Web.Post(url, post, delegate(HttpResponse response) {
                Broadcast("HELLO", response.Text);
            });*/

            /*}
            public override void GotMessage(Player player, Message message)
            {
                switch (message.Type)
                {
                    case"search":
                        searchOpp(player);
                    break;
                }
            }
            private void searchOpp(Player player)
            {
                 Broadcast("UserJoined", player.Id);*/
            Player opponent = null;
            if (PlayerCount >= 2)
            {
                foreach (Player p in Players)
                {
                    if (p != player)
                    {
                        opponent = p;
                        break;
                    }
                }
            }

            if (opponent != null)
            {
                string id = randomString(20);

                opponent.Send("gameFound", id);
                player.Send("gameFound", id);

                opponent.Disconnect();
                player.Disconnect();
            }
        }

        public override void UserLeft(Player player) { }


        private string randomString(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
    [RoomType("Game")]
    public class GameCode : Game<Player>
    {
        //XmlDocument xdoc;
        Player pl, opp;
        int[] energies;
        //Dictionary<string, XmlNode> ArrayName = new Dictionary<string, XmlNode> { };
         //XElement player1skill1, player1skill2, player1skill3, player1skill4, player2skill1, player2skill2, player2skill3, player2skill4, player3skill1, player3skill2, player3skill3, player3skill4;
         //XElement opponent1skill1, opponent1skill2, opponent1skill3, opponent1skill4, opponent2skill1, opponent2skill2, opponent2skill3, opponent2skill4, opponent3skill1, opponent3skill2, opponent3skill3, opponent3skill4;
         //XDocument doc = XDocument.Load("..\\..\\..\\..\\Flash\\XML\\characters.xml");
         //XmlNode player1, player2, player3;
         string pl1name, pl2name, pl3name, opp1name, opp2name, opp3name;
         int physical = 0, df = 0, special = 0, weapon = 0, re = 0;
         int oppphysical = 0, oppdf = 0, oppspecial = 0, oppweapon = 0, oppre = 0;
         public int turn = 1, turncount = 1;
         Boolean turnt = true, turnf = true;
         Boolean turnalreadysent = false;
         int plnumber = 3, oppnumber = 3, plnrcount = 3, oppnrcount = 3;
         Dictionary<int, string> plname = new Dictionary<int, string>();
         Dictionary<string, int> whichEnergies = new Dictionary<string, int>();
         Dictionary<int, int> HPs = new Dictionary<int, int>();
         int pl1hp = 100, pl2hp = 100, pl3hp = 100, pl4hp = 100, pl5hp = 100, pl6hp = 100;
         string ploropp;
         public PlayerIO.GameLibrary.Timer aTimer;

        //XmlDocument abc1 = new XmlDocument();
        //XmlNode Node;

        /*public class GameTimer
         {
             //private bool isStopped = false;
             private PlayerIO.GameLibrary.Timer timer;

             public void Stop()
             {
                 //if (isStopped == false)
                 //{
                     //isStopped = true;
                 try
                 {
                     timer.Stop();
                 }
                  catch(Exception ex)
                 {
                  }
                 //}
             }

             public static GameTimer GetGameTimer(PlayerIO.GameLibrary.Timer timer)
             {
                 GameTimer gt = new GameTimer();
                 gt.timer = timer;
                 return gt;
             }
         }


        public void startTimer()
        {
            aTimer = GameTimer.GetGameTimer(AddTimer(delegate
            {
                turn++;
                turnalreadysent = false;
                turns();
            }, 15000));
            timerstarted = true;
            timercount++;
        }

        public bool timerstarted = false, canBeReseted = false;
        public int timercount = 0, resetcount = 1;
        public void resetTimer()
            {
                if ((timerstarted == true) && (resetcount == timercount))
                {
                    aTimer.Stop();
                    startTimer();
                    resetcount++;
                }
            }*/
                

        public void startTimer()
        {
            aTimer = AddTimer(delegate
            {
                turn++;
                turnalreadysent = false;
                turns();
            }, 15000);
            timerstarted = true;
            timercount++;
        }

        public bool timerstarted = false, canBeReseted = false;
        public int timercount = 0, resetcount = 1;
        public void resetTimer()
            {
                if ((timerstarted == true) && (resetcount == timercount))
                {
                    try
                    {
                        aTimer.Stop();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Caught one!");
                    }
                    startTimer();
                    resetcount++;
                }
            }

         int attack, heal;
         string htype, atttype;
        
        public override void GameStarted() 
        {
            startTimer();
            

            //abc1.Load("..\\..\\..\\..\\Flash\\XML\\characters.xml");
            //Node = abc1.DocumentElement;
            re = physical + df + special + weapon;
            oppre = oppphysical + oppdf + oppspecial + oppweapon;
            /*if (turncount != turn)
            {
                turncount = turn;
                if (turn % 2 == 0)
                {
                    pl.Send("YourTurn", turnt);
                    opp.Send("TurnEnd", turnf);
                    EnergySystem("Player");
                    ploropp = "pl";
                }
                else
                {
                    opp.Send("YourTurn", turnt);
                    pl.Send("TurnEnd", turnf);
                    EnergySystem("Opponent");
                    ploropp = "opp";
                }
            }*/


            if (plnrcount != plnumber)
            {

            }
            /* Not NEEDED -iG.
            AddTimer(delegate
            {
                
            }, 500);*/

        }

        public override void GameClosed() { }

        public override void UserJoined(Player player) {
            if (PlayerCount == 2)
            {
                Player opponent = null;
                foreach (Player p in Players)
                {
                    if (p != player)
                    {
                        opponent = p;
                        break;
                    }
                    
                }
                opp = opponent;
                pl = player;
                if (opp != null && pl != null)
                {
                    player.Send("Abc", "Both players are estabilished");
                }
                else { player.Send("Abc", "It worked?");
                }
            }
            
            
        }
        public override void GotMessage(Player player, Message message)
        {
            switch (message.Type)
            {
                case "BroadcastMessage":
                    Broadcast("BroadcastMessage", message.GetString(0));
                    break;
                case "username":
                    sendMessageToAllOtherPlayers(player, "Enemyusername", message.GetString(0));
                    break;
                case "players":
                    if (player == pl)
                    {
                        pl1name = message.GetString(0);
                        pl2name = message.GetString(1);
                        pl3name = message.GetString(2);
                        opp.Send("enemies", pl1name, pl2name, pl3name);
                        opp.Send("HP", pl4hp, pl5hp, pl6hp, pl1hp, pl2hp, pl3hp);
                    }
                    else
                    {
                        opp1name = message.GetString(0);
                        opp2name = message.GetString(1);
                        opp3name = message.GetString(2);
                        if (pl != null)
                        {
                            pl.Send("enemies", opp1name, opp2name, opp3name);
                            pl.Send("HP", pl1hp, pl2hp, pl3hp, pl4hp, pl5hp, pl6hp);
                        }
                    }
                    if((pl1name != "")&&(opp1name != "")){
                        loadXML();
                        turns();
                        initPlayers();
                    }
                    /*if ((turn % 2 == 0) && pl != null && opp!= null)
                    {
                        pl.Send("YourTurn", turn);
                        ploropp = "pl";
                        opp.Send("OppTurn", turn);
                    }
                    if ((!(turn % 2 == 0)) && pl != null && opp != null)
                    {
                        opp.Send("YourTurn", turn);
                        ploropp = "opp";
                        pl.Send("OppTurn", turn);
                    }*/
                    break;
                case "EndTurn":
                    string checkifPlayerblabla;
                    if (player == pl) { checkifPlayerblabla = "pl"; }
                    else { checkifPlayerblabla = "opp"; }
                    if (checkifPlayerblabla == ploropp)
                    {
                        turn++;
                        turnalreadysent = false;
                        turns();
                    }
                    break;
                case "attack":
                        attack = message.GetInt(0);
                        atttype = message.GetString(1);
                    break;
                case "heal":
                        heal = message.GetInt(0);
                        htype = message.GetString(1);
                    break;
                case "PlayerPerformedSkill":
                        int playerperformer = message.GetInt(0);
                        int skillofplayer =  message.GetInt(1);
                        int targetofskill = message.GetInt(2);
                        skillCheck(playerperformer, skillofplayer, targetofskill, player);
                    break;
            }
        }
        private void sendMessageToAllOtherPlayers(Player player, string type, string message)
        {
            foreach (Player p in Players)
            {
                if (p.Id != player.Id)
                {

                    p.Send(type, message);
                }
            }
        }



        private void turns()
        {
            if (pl != null && opp != null)
            {
                if (turnalreadysent)
                {
                    return;
                }
                aESactivated = false;
                if (turn % 2 == 0)
                {
                    pl.Send("YourTurn", turnt);
                    opp.Send("TurnEnd", turnf);
                    EnergySystem("Player");
                    ploropp = "pl";
                    opp.Send("OppTurn", turn);
                }
                else
                {
                    opp.Send("YourTurn", turnt);
                    pl.Send("TurnEnd", turnf);
                    EnergySystem("Opponent");
                    ploropp = "opp";
                    pl.Send("OppTurn", turn);
                }
                aESactivated = true;  //for EnergySystem once
                resetTimer();
                turnalreadysent = true;
            }
        }
        private void loadXML()
        {
            //XmlNode attrVal = Node.SelectSingleNode("character[@name='luffy']");
            //XmlNode attrVal1 = Node.SelectSingleNode("character[@name='luffy']/skill1/adasd");
            //player1 = Node.SelectSingleNode("character[@name='"+pl1name+"']");
            //player2 = Node.SelectSingleNode("character[@name='"+pl2name+"']");
            //player3 = Node.SelectSingleNode("character[@name='"+ pl3name +"']");
             //if (attrVal1 == null) { 
             //   Console.WriteLine("emptyzz"); 
             //    // worked  - you can see if it exists or not
             //   }
             // Console.WriteLine(attrVal1.InnerText+"hah");
             // Console.WriteLine("^ this here");
             // foreach (XmlNode Node2 in attrVal.ChildNodes)
             // {
             //        //Console.WriteLine(Node2.InnerText);
             //  }




            //OLD METHOD, DIDNT WORK ZZZZZZZZZZZZZZZZ
            /*
            player1skill1 = doc.Descendants("character")
                // then take the single one with an attribute named "value"
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
                // and take that element's child element of type skill1 
               .Element("skill1");
            // this will print <skill1 ... /skill1>. However, this is an XElement object, not a string
            // so you can continue to access inner text, attributes, children etc.
            //Console.WriteLine(player1skill1);
            player1skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill2");
            player1skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill3");
            player1skill4 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill4");


            player2skill1 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl2name)
                .Element("skill1");
            player2skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl2name)
               .Element("skill2");
            player2skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl2name)
               .Element("skill3");
            player2skill4 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl2name)
                .Element("skill4");


            player3skill1 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl3name)
               .Element("skill1");
            player3skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl3name)
               .Element("skill2");
            player3skill3 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl3name)
                .Element("skill3");
            player3skill4 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl3name)
                .Element("skill4");


            opponent1skill1 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp1name)
               .Element("skill1");
            opponent1skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp1name)
               .Element("skill2");
            opponent1skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp1name)
               .Element("skill3");
            opponent1skill4 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp1name)
               .Element("skill4");


            opponent2skill1 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp2name)
                .Element("skill1");
            opponent2skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp2name)
               .Element("skill2");
            opponent2skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp2name)
               .Element("skill3");
            opponent2skill4 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp2name)
                .Element("skill4");


            opponent3skill1 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp3name)
               .Element("skill1");
            opponent3skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp3name)
               .Element("skill2");
            opponent3skill3 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp3name)
                .Element("skill3");
            opponent3skill4 = doc.Descendants("character")
                 .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == opp3name)
                 .Element("skill4");


            var description = player1skill1.Element("description").Value;
            //Console.WriteLine(description);
             * */
            
            //ArrayName.Add("player1skill1", player1skill1);
            //ArrayName.Add("player1skill2", player1skill2);
            //ArrayName.Add("player1skill3", player1skill3);
            //ArrayName.Add("player1skill4", player1skill4);
            //ArrayName.Add("player2skill1", player2skill1);
            //ArrayName.Add("player2skill2", player2skill2);
            //ArrayName.Add("player2skill3", player2skill3);
            //ArrayName.Add("player2skill4", player2skill4);
            //ArrayName.Add("player3skill1", player3skill1);
            //ArrayName.Add("player3skill2", player3skill2);
            //ArrayName.Add("player3skill3", player3skill3);
            //ArrayName.Add("player3skill4", player3skill4);

            //ArrayName.Add("player1", player1);
            //ArrayName.Add("player2", player2);
            //ArrayName.Add("player3", player3);


            //skillCheck(1, 1, 4);
        }

        bool plinitialized = false;

        private void updateDict()
        {
            physical = whichEnergies["physicalpl"];
            weapon = whichEnergies["weaponpl"];
            df = whichEnergies["dfpl"];
            special = whichEnergies["specialpl"];
            oppphysical = whichEnergies["physicalopp"];
            oppweapon = whichEnergies["weaponopp"];
            oppdf = whichEnergies["dfopp"];
            oppspecial = whichEnergies["specialopp"];
            re = whichEnergies["repl"];
            oppre = whichEnergies["reopp"];
        }

        private void initPlayers()
        {
            if (plinitialized == false)
            {
                plname[1] = pl1name;
                //plname.Add(1, pl1name);
                plname[2] = pl2name;
                plname[3] = pl3name;
                plname[4] = opp1name;
                plname[5] = opp2name;
                plname[6] = opp3name;

                HPs[1] = pl1hp;
                HPs[2] = pl2hp;
                HPs[3] = pl3hp;
                HPs[4] = pl4hp;
                HPs[5] = pl5hp;
                HPs[6] = pl6hp;
                plinitialized = true;

            }
            whichEnergies["physicalpl"] = physical;
            whichEnergies["weaponpl"] = weapon;
            whichEnergies["dfpl"] = df;
            whichEnergies["specialpl"] = special;
            whichEnergies["physicalopp"] = oppphysical;
            whichEnergies["weaponopp"] = oppweapon;
            whichEnergies["dfopp"] = oppdf;
            whichEnergies["specialopp"] = oppspecial;
            whichEnergies["repl"] = re;
            whichEnergies["reopp"] = oppre;

        

}
        private void sendHPs()
        {
            pl.Send("HP", HPs[1], HPs[2], HPs[3], HPs[4], HPs[5], HPs[6]);
            opp.Send("HP", HPs[4], HPs[5], HPs[6], HPs[1], HPs[2], HPs[3]);
        }

        private Player getploropp()
        {
            Player x = null;
            if(ploropp == "opp"){
                x = opp; 
            }
            else if (ploropp == "pl"){
                x = pl;
            }
            return x;
        }

        private void skillCheck(int player, int sk, int target, Player whichPlayer)
        {
            initPlayers();
            Characters ch;
            Console.WriteLine(plname[1] + " 2" + plname[2] + " 3" + plname[3] + " 4" + plname[4] + " 5" + plname[5] + " 6" + plname[6]);
            Console.WriteLine("Skillcheck... player: " + player + " skillNr: " + sk + "target: " + target);
            Console.WriteLine("plname[player]: " + plname[player] + "      plname[player+3] :" + plname[player + 3]);
            //per mas me ba kot naj gabim, me e ba 1 ploropp tjeter ;)
            String ploropp2 = "";
            if (whichPlayer == opp) { ch = new Characters(plname[player + 3], sk); ploropp2 = "opp"; }
            else { ch = new Characters(plname[player], sk); ploropp2 = "pl"; }
            bool skillwasperformed = false;
            Console.WriteLine("Plname :" + plname[player] + " plname[1]:" + plname[1] + " plname[4]:" + plname[4] + " ch.skilltype:" + ch.skilltype + " target" + target + " hps[target]" + HPs[target] + " playernr" + player + " ploropp?" + ploropp + " ploropp2?" + ploropp2);
            //Plname : ch.skilltype: target4

            if (whichPlayer == opp)
                Console.WriteLine("HELOOOOOOOOOO whichEnergies[physical + ploropp2]:" + whichEnergies["df" + ploropp2] + " ch.df : " + ch.df);
            else
                Console.WriteLine("HELOOOOOOOOOO whichEnergies[physical + ploropp2]:" + whichEnergies["df" + ploropp2] + " ch.df : " + ch.df);
            if (!(whichEnergies["physical" + ploropp2] - ch.physical >= 0)) { return; }
            whichEnergies["physical" + ploropp2] -= ch.physical;
            if (!(whichEnergies["weapon" + ploropp2] - ch.weapon >= 0)) { return; }
            whichEnergies["weapon" + ploropp2] -= ch.weapon;
            if (!(whichEnergies["df" + ploropp2] - ch.df >= 0)) { return; }
            whichEnergies["df" + ploropp2] -= ch.df;
            if (!(whichEnergies["special" + ploropp2] - ch.special >= 0)) { return; }
            whichEnergies["special" + ploropp2] -= ch.special;
            //whichEnergies["re" + ploropp2] = whichEnergies["physical" + ploropp2] + whichEnergies["weapon" + ploropp2] + whichEnergies["df" + ploropp2] + whichEnergies["special" + ploropp2];
            if (!(whichEnergies["re" + ploropp2] - ch.random /*(ch.physical + ch.weapon + ch.df + ch.special)*/ >= 0)) { return; }
            whichEnergies["re" + ploropp2] -= ch.random;
            Console.WriteLine("1");
            if (ch.skilltype == "attack")
            {
                Console.WriteLine("2");
                if (ch.targetnumber == 1)
                {
                    Console.WriteLine("3");
                    if (ploropp2 == "opp") { target -= 3; }
                    Console.WriteLine("4");
                    //+3 apo vtm HPs[target] ?
                    HPs[target] -= ch.damage;
                    Console.WriteLine("Plname :" + plname[player] + " ch.skilltype:" + ch.skilltype + " target" + target + " hps[target]" + HPs[target] + " playernr" + player);
                    skillwasperformed = true;
                }
            }


            if (skillwasperformed)
            {
                Console.WriteLine("Skill was performed!");
                sendHPs();
                updateDict();
                sendEnergies();
            }

            /*var skillNode = ArrayName["player" + player];
            // Example : Console.WriteLine(attrVal2.SelectSingleNode("skill1/damage" + "hah");
            var skill = skillNode.SelectSingleNode("skill" + sk);

            //Old Method zzzzzzzzzzzzzzz
            //var skill = ArrayName["player" + player + "skill" + sk];
            if (!(physical - int.Parse(skill.SelectSingleNode("physical").Value) >= 0)) { return; }
            if (!(weapon - int.Parse(skill.SelectSingleNode("weapon").Value) >= 0)) { return; }
            if (!(df - int.Parse(skill.SelectSingleNode("devilfruit").Value) >= 0)) { return; }
            if (!(special - int.Parse(skill.SelectSingleNode("special").Value) >= 0)) { return; }
            re = physical + weapon + df + special;
            if (!(re - int.Parse(skill.SelectSingleNode("random").Value) >= 0)) { return; }

            switch (skill.SelectSingleNode("type").Value)
            {
                case "attack":
                    Console.WriteLine("dafuq");

                    break;
            }*/

        }

        Random r = new Random();
         int generateRandom(int max)
        {
            
            int randnum = r.Next(0, max);
            return randnum;
        }

         public bool aESactivated = false;

         private void sendEnergies()
         {
             pl.Send("energies", physical, df, special, weapon, re); 
             opp.Send("energies", oppphysical, oppdf, oppspecial, oppweapon, oppre);
         }
        private void EnergySystem(string player)
        {
            if (aESactivated == false)
            {
                if (player == "Player")
                {
                    opp.Send("Abc", "plEnergies");
                    energies = new int[plnumber];
                        for (int i = 0; i < plnumber; i++)
                        {
                            
                            energies[i] = generateRandom(4);
                        }
                        for (int i = 0; i < energies.Length; i++)
                        {
                            if (energies[i] == 0) { physical++; }
                            if (energies[i] == 1) { df++; }
                            if (energies[i] == 2) { special++; }
                            if (energies[i] == 3) { weapon++; }
                        }
                        re = physical + df + special + weapon;
                        pl.Send("energies", physical, df, special, weapon, re);
                      }

                   else if(player == "Opponent"){
                       Console.WriteLine("Sup.");
                       opp.Send("Abc", "OppEnergies");
                       energies = new int[oppnumber];
                        for (int i = 0; i < oppnumber; i++)
                        {
                            energies[i] = generateRandom(4);
                        }
                        for (int i = 0; i < energies.Length; i++)
                        {
                            if (energies[i] == 0) { oppphysical++; }
                            if (energies[i] == 1) { oppdf++; }
                            if (energies[i] == 2) { oppspecial++; }
                            if (energies[i] == 3) { oppweapon++; }
                        }
                        oppre = oppphysical + oppdf + oppspecial + oppweapon;
                        opp.Send("energies", oppphysical, oppdf, oppspecial, oppweapon, oppre);
                   }
                }
                aESactivated = true;
            }
        

        public override void UserLeft(Player player) { }
    }
}