using System;
using System.Collections.Generic;

namespace Characters
{
    public class Mage : Character
    {
        public Mage(string name) : base(name, strength: 3, intelligence: 10, agility: 7, baseHealth: 300)
        {
            Attacks.Add(new Attack("Fireball", 30 * Intelligence, false, 0));  // Mago tiene un ataque de bola de fuego de daño 30
            Attacks.Add(new Attack("Frostbolt", 20 * Intelligence, false, 0));  // Mago tiene un ataque de bola de escarcha de daño 20
            Attacks.Add(new Attack("Slash", 25 * Strength, false, 0));
        }
    }
}
