using System;
using System.Collections.Generic;

namespace Characters
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int MaxHealth { get; set; }  // Salud máxima
        public int CurrentHealth { get; set; }  // Salud actual
        public int Experience { get; set; }
        public List<Attack> Attacks { get; set; }

        public Character(string name, int strength, int intelligence, int agility, int baseHealth)
        {
            Name = name;
            Level = 1;
            Strength = strength;
            Intelligence = intelligence;
            Agility = agility;
            MaxHealth = baseHealth + (Level * 10);  // Salud base + incremento por nivel
            CurrentHealth = MaxHealth;
            Experience = 0;
            Attacks = new List<Attack>();
        }

        public void LevelUp()
        {
            Level++;
            Strength += 5;
            Intelligence += 2;
            Agility += 3;
            MaxHealth += 10;  // Aumento de salud máxima al subir de nivel
            CurrentHealth = MaxHealth;  // Restaurar salud al máximo al subir de nivel
        }

        public void Attack(Character opponent, Attack attack)
        {
            Random rand = new Random();
            int attackSuccess = rand.Next(0, 100);

            if (attackSuccess < 70) // 70% chance to hit
            {
                Console.WriteLine($"\n{Name} ataca a {opponent.Name} con {attack.Name} por {attack.Damage} de daño!");
                opponent.TakeDamage(attack.Damage);
            }
            else
            {
                Console.WriteLine($"\n{Name} falló el ataque {attack.Name}!");
            }
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;
            Console.WriteLine($"\n{Name} recibe {damage} de daño! Salud restante: {CurrentHealth}");
        }

        public void ShowAttacks()
        {
            Console.WriteLine($"\n{Name} - Elige un ataque:");
            for (int i = 0; i < Attacks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Attacks[i].Name} (Daño: {Attacks[i].Damage})");
            }
        }

        public void GainExperience(int exp)
        {
            Experience += exp;
            if (Experience >= 100)
            {
                LevelUp();
                Experience = 0;
                Console.WriteLine($"{Name} ha subido de nivel!");
            }
        }

        public void ShowStats()
        {
            Console.WriteLine($"{Name} - Nivel: {Level}, Salud: {CurrentHealth}/{MaxHealth}, Fuerza: {Strength}, Inteligencia: {Intelligence}, Agilidad: {Agility}, Experiencia: {Experience}");
        }
    }
}
