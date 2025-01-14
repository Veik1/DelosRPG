using System;
using System.Collections.Generic;

namespace Characters
{
    public class Warrior : Character
    {
        public Warrior(string name) : base(name, strength: 10, intelligence: 5, agility: 8, baseHealth: 400)
        {
            // Realizar el cálculo del daño correctamente sin interpolación de cadenas
            Attacks.Add(new Attack("Slash", 25 * Strength, false, 0));  // Guerrero tiene un ataque de corte
            Attacks.Add(new Attack("Power Strike", 40 * Strength, false, 0));  // Guerrero tiene un ataque de golpe poderoso
            Attacks.Add(new Attack("Fireball", 30 * Intelligence, false, 0));  // Guerrero tiene un ataque de bola de fuego (aunque esto es más para el mago)
        }
    }
}
