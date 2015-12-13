using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServersideGameCode
{

    class Characters
    {
        public int physical = 0;
        public int df = 0;
        public int weapon = 0;
        public int special = 0;
        public int random = 0;
        public string skilltype = "";
        public string attacktype = "";
        public int damage = 0;
        public int turns = 1;
        public int targetnumber = 1;
        public bool targetnotinvulnerable = false;
        public int turnnotinvulnerable = 0;
        public bool invulnerableAgainstMeele = false;
        public int invulnerableAgainstMeeleTurn = 0;
        public bool invulnerableAgainstRange = false;
        public int invulnerableAgainstRangeTurn = 0;
        public bool specialSkill = false;
        public bool invulnerable = false;
        public int invulnerableTurn = 0;

        List<string> skillproperties = new List<string>();
        public Characters(){
            Console.WriteLine("Hi!");
        }
        public Characters(string charname, int skillNumber)
        {
            switch (charname)
            {
                case "sanji":
                    if (skillNumber == 1)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 2)
                    {

                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        physical = 1;
                        targetnumber = 1;
                        //testing
                        /*
                        skilltype = "attack";
                        damage = 20;
                        invulnerableAgainstMeele = true;
                        invulnerableAgainstMeeleTurn = 1;*/
                    }
                    if (skillNumber == 3)
                    {
                        skilltype = "attack";
                        damage = 30;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        physical = 1;
                        random = 2;
                        targetnumber = 1;
                        //testing
                        /*
                        skilltype = "attack";
                        specialSkill = true;
                        damage = 15;
                        targetnumber = 3;*/
                    }
                    if (skillNumber == 4)
                    {
                        skilltype = "invulnerable";
                        invulnerable = true;
                        invulnerableTurn = 1;
                    }
                    break;
                case "luffy":
                    if (skillNumber == 1)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 2)
                    {

                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        physical = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 3)
                    {

                        skilltype = "attack";
                        damage = 30;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        physical = 1;
                        random = 2;
                        targetnumber = 1;
                        //testing
                    }
                    if (skillNumber == 4)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    break;
                case "brook":
                    if (skillNumber == 1)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 2)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 3)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    if (skillNumber == 4)
                    {
                        skilltype = "attack";
                        damage = 15;
                        targetnotinvulnerable = true;
                        turnnotinvulnerable = 1;
                        df = 1;
                        targetnumber = 1;
                    }
                    break;
                case "readytocopy":
                    if (skillNumber == 1)
                    {

                    }
                    if (skillNumber == 2)
                    {

                    }
                    if (skillNumber == 3)
                    {

                    }
                    if (skillNumber == 4)
                    {

                    }
                    break;

            }
        }
        
    }
}

//

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public static class Consts
//{
//    public const string SkillType_Attack = "attack";
//    public const string SkillType_Defense = "defense";
//}

//public class Skill
//{
//    public string type;

//    public Skill()
//    {
//        //This is the default const for skill type at data creation
//        type = Consts.SkillType_Attack;
//    }
//}

//public class Character
//{
//    public string name;
//    public List<Skill> skillList = new List<Skill>();

//    public Character(string name)
//    {
//        this.name = name;
//    }

//    public Character()
//    {
//        //Default character name at data creation
//        name = "No Name";
//    }
//}
