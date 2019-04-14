using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonsterHeroClassApp1
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();
            Character monster = new Character();
            monster.Name = "Monster";
            Dice dice = new Dice();
            gameLoop(hero, monster, dice);
        }

        private void gameLoop(Character hero, Character monster, Dice dice)
        {
            int count = 0;
            do
            {
                if (StartBattle(hero, monster, dice, count) == false) break;
                count += 1;
                if (gameOver(hero, monster)) break;
            } while (hero.Health > 0 && monster.Health > 0);
        }

        private bool StartBattle(Character hero, Character monster, Dice dice, int count)
        {
            if (count == 0) bonusAttacks(hero, monster, dice); //determine bonus only once
            monstersAttack(hero, monster, dice);

            if (gameOver(hero, monster)) return false; //StartBattle is false when Game is Over
            herosAttack(hero, monster, dice);

            Display(hero, monster);
            if (gameOver(hero, monster)) return false;
            return true;
        }

        private void monstersAttack(Character hero, Character monster, Dice dice)
        {
            int damage = monster.Attack(dice);
            hero.Defend(damage);
        }

        private void herosAttack(Character hero, Character monster, Dice dice)
        {
            int damage = hero.Attack(dice);
            monster.Defend(damage);
        }

        private bool gameOver(Character hero, Character monster)
        {
            if (hero.Health <= 0 || monster.Health <= 0)
            {
                testLabel.Text += "Game Over<br />";
                if (hero.Health > 0) testLabel.Text += "The Hero delivered a fatal blow and wins!";
                else testLabel.Text += "The Monster delivered a fatal blow and wins!";
                return true;
            }
            else
                return false;
        }

        private void Display(Character hero, Character Monster)
        {
            string result = String.Format("The battle resulted in the Hero with {0} health and " +
                "the Monster with {1} health remaining.<br /><br />", hero.Health, Monster.Health);
            testLabel.Text += result;
        }

        private void bonusAttacks(Character hero, Character monster, Dice dice)
        {
            hero.Bonus();
            if (hero.AttackBonus)
                heroBonus(hero, monster, dice);

            monster.Bonus();
            if (monster.AttackBonus)
                monsterBonus(hero, monster, dice);
        }

        private void heroBonus(Character hero, Character monster, Dice dice)
        {
            int damage = hero.Attack(dice);
            monster.Defend(damage);
            testLabel.Text = "The Hero is awarded a bonus attack!<br /><br />";
            Display(hero, monster);
        }

        private void monsterBonus(Character hero, Character monster, Dice dice)
        {
            int damage = monster.Attack(dice);
            hero.Defend(damage);
            testLabel.Text += "The Monster is awarded a bonus attack!<br /><br />";
            Display(hero, monster);
        }

        class Character
        {
            public string Name = "Hero";
            public int Health { get; set; } = 100;
            public int DamageMaximum { get; set; } = 30;
            public bool AttackBonus { get; set; }

            public int Attack(Dice dice)
            {
                dice.sides = this.DamageMaximum;
                return dice.Roll();
            }
            
            public void Defend(int damage)
            {
                this.Health -= damage;
            }

            public void Bonus()
            {
                Random bonus = new Random();
                Thread.Sleep(1000);
                int num = bonus.Next(0, 2);
                this.AttackBonus = (num == 1) ? true : false;
            }
        }

        class Dice
        {
            public int sides { get; set; }
            Random random = new Random();
            public int Roll()
            {
                return random.Next(1, this.sides);
            }
        }

        protected void battleButton_Click(object sender, EventArgs e)
        {
        }
    }
}