using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using System.Timers;

namespace ConsoleApplication1
{
    
    class Program
    {
        static int[] energies;
        static Dictionary<string, XElement> ArrayName = new Dictionary<string, XElement> { };
        static int physical = 0, df = 0, special = 0, weapon = 0, re = 0;
        static int oppphysical = 0, oppdf = 0, oppspecial = 0, oppweapon = 0, oppre = 0;

        static int plnumber = 3, oppnumber = 3, plnrcount = 3, oppnrcount = 3;
        static XElement player1skill1, player1skill2, player1skill3, player1skill4, player2skill1, player2skill2, player2skill3, player2skill4, player3skill1, player3skill2, player3skill3, player3skill4;
        static void Main(string[] args)
        {
            string lol = "HAHAHA";

            string pl1name = "luffy";
            string pl1type, pl1skill, class1, class2;


            var doc = XDocument.Load("..\\..\\..\\..\\Flash\\XML\\characters.xml");
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
               .Element("skill1");
            player1skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");
            player1skill4 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");


            player2skill1 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
                .Element("skill1");
            player2skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");
            player2skill3 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");
            player2skill4 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
                .Element("skill1");


            player3skill1 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");
            player3skill2 = doc.Descendants("character")
               .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
               .Element("skill1");
            player3skill3 = doc.Descendants("character")
                .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
                .Element("skill1");
            player3skill4 = doc.Descendants("character")
                 .Single(ch => ch.Attribute("name") != null && ch.Attribute("name").Value == pl1name)
                 .Element("skill1");


            // 2. Access the description of skill1 where name(att) is "MyChar1"
            // this is tricky because the description text is just floating among other tags
            // if description were wrapped in <description></description>, this would be simply
            var description = player1skill1.Element("description").Value;
            // here's the hacky way I found to get it in the current xml:

            // first get the full value (inner text) of the skill node (includes "Skill Name")
            var fullValue = player1skill1.Value;
            // then get the concatenated full values of all child nodes (= "Skill Name")
            var innerValues = string.Join("", player1skill1.Elements().Select(e => e.Value));
            // get the description by dropping off the trailing characters that are actually inner values
            // by limiting the length to the full length - the length of the non-description characters
            //var description = fullValue.Substring(0, length: fullValue.Length - innerValues.Length);
            Console.WriteLine(description + " asdasasdasasdasasdasasdasasdasasdas");

            ArrayName.Add("player1skill1", player1skill1);
            skillCheck(1, 1);
            int abc = 1;
            Console.WriteLine(ArrayName["player" + abc + "skill" + abc]);

            int energy1 = energy(4);
            Console.WriteLine(energy1);
            EnergySystem("Opponent");






            XmlDocument abc1 = new XmlDocument();
            abc1.Load("..\\..\\..\\..\\Flash\\XML\\characters.xml");
            XmlNode Node = abc1.DocumentElement;

            string aasdf = "luffy";
            XmlNode attrVal = Node.SelectSingleNode("character[@name='luffy']");

            XmlNode attrVal1 = Node.SelectSingleNode("character[@name='luffy']/skill1/damage");
            XmlNode attrVal2 = Node.SelectSingleNode("character[@name='"+aasdf+"']");
            if (attrVal1 == null)
            {
                Console.WriteLine("emptyzz");
                // worked  - you can see if it exists or not
            }
            Console.WriteLine(attrVal1.InnerText + "hah");
            Console.WriteLine(attrVal2.SelectSingleNode("skill1/damage" + "hah"));
                Console.WriteLine("^ this here");
                foreach (XmlNode Node2 in attrVal.ChildNodes)
                {
                    //Console.WriteLine(Node2.InnerText);
                }


                /*System.Timers.Timer aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 6000;
                aTimer.Enabled = true;

                Console.WriteLine("Press \'q\' to quit the sample.");
                while (Console.Read() == 'q')
                {
                    aTimer.Stop();
                    aTimer.Start();
                }*/
                for (int f = 0; f < 3; f++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        energies = new int[3];
                        energies[i] = generateRandom(3);
                    }
                    for (int i = 0; i < energies.Length; i++)
                    {
                        if (energies[i] == 0) { physical++; }
                        if (energies[i] == 1) { df++; }
                        if (energies[i] == 2) { special++; }
                        if (energies[i] == 3) { weapon++; }
                    }
                    re = physical + df + special + weapon;
                    Console.WriteLine("?" + re + physical + df + special + weapon);
                }



                //            Console.WriteLine(result.ToString());
                Console.ReadKey();
            
        }

        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
        }
         static void skillCheck(int player, int sk){
            var skill = ArrayName["player" + player + "skill" + sk];
            Console.WriteLine(skill);
            switch (player1skill1.Element("type").Value)
            {
                case "attack" :
                    Console.WriteLine("dafuq");
                    if (player1skill1.Element("increase").Value == "")
                    {
                        Console.WriteLine("ROFL");
                    }
                break;
            }
        
        }
        static int energy(int max)
         {
            Random r = new Random();
            int randnum = r.Next(0, max);
            return randnum;
         }
        private static void EnergySystem(string player)
        {
            switch (player)
            {
                case "Player":
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
                    break;
                case "Opponent":
                    energies = new int[oppnumber];
                    for (int i = 0; i < oppnumber; i++)
                    {
                        
                        energies[i] = generateRandom(4);
                        Console.WriteLine(energies[i] + "Test1 " + i);
                    }
                    for (int j = 0; j < energies.Length; j++)
                    {
                        if (energies[j] == 0) { oppphysical++; }
                        if (energies[j] == 1) { oppdf++; }
                        if (energies[j] == 2) { oppspecial++; }
                        if (energies[j] == 3) { oppweapon++; }
                        Console.WriteLine(energies[j] + "Test2 " + j);
                    }
                    Console.WriteLine("Dafuq"+oppphysical + oppdf + oppspecial + oppweapon + oppre);
                    break;
            }
        }

        static Random r = new Random();
        static int generateRandom(int max)
        {
            int randnum = r.Next(0, max);
            return randnum;
        }
        

    }
}
