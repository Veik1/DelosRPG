namespace Characters
{
    public class Attack
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public bool IsHealing { get; set; }
        public int HealingAmount { get; set; }

        public Attack(string name, int damage, bool isHealing, int healingAmount = 0)
        {
            Name = name;
            Damage = damage;
            IsHealing = isHealing;
            HealingAmount = healingAmount;
        }
    }
}
