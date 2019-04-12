using System;
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

            hero.Name = "Hero";
            hero.Health = 100;
            hero.DamageMaximum = 30;
            hero.AttackBonus = true;

            monster.Name = "Monster";
            monster.Health = 100;
            monster.DamageMaximum = 40;
            monster.AttackBonus = false;

            StartBattle(hero, monster);

            Display(hero, monster);
        }

        private void StartBattle(Character hero, Character monster)
        {
            int damage = hero.Attack();
            monster.Defend(damage);

            damage = monster.Attack();
            hero.Defend(damage);

            
        }

        private void Display(Character hero, Character Monster)
        {
            string result = String.Format("The battle resulted in the Hero with {0} health and " +
                "the Monster with {1} health remaining.", hero.Health, Monster.Health);

            testLabel.Text = result;
        }

        class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int DamageMaximum { get; set; }
            public bool AttackBonus { get; set; }

            public int Attack()
            {
                Random random = new Random();
                int damage = random.Next(this.DamageMaximum);
                return damage;
            }

            public void Defend(int damage)
            {
                this.Health -= damage;
            }
        }

        protected void battleButton_Click(object sender, EventArgs e)
        {
        }
    }
}