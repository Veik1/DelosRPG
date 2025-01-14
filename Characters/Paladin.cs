using System;
using System.Collections.Generic;

namespace Characters
{
    public class Paladin : Character
    {
        public Paladin(string name) : base(name, strength: 7, intelligence: 12, agility: 4, baseHealth: 430)
        {
            Attacks.Add(new Attack("Victory Slash", (30 * Intelligence) + Strength, false, 0));  // Mago tiene un ataque de bola de fuego de daño 30
            Attacks.Add(new Attack("Light Hand", 20 * Intelligence, false, 0));  // Mago tiene un ataque de bola de escarcha de daño 20
            Attacks.Add(new Attack("Holy Light", 0, true, 25 * Intelligence));
        }
    }
}
