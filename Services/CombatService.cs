using System;
using System.Linq;
using Characters;
using System.Collections.Generic;

namespace Services
{
    public static class CombatService
    {
        public static void StartCombat(Character player1, Character player2)
        {
            Console.WriteLine($"\n¡La pelea entre {player1.Name} y {player2.Name} ha comenzado!\n");

            while (player1.CurrentHealth > 0 && player2.CurrentHealth > 0)
            {
                // Turno del jugador 1
                TakeTurn(player1, player2);
                if (player2.CurrentHealth <= 0) break;

                // Turno del jugador 2
                TakeTurn(player2, player1);
                if (player1.CurrentHealth <= 0) break;
            }

            // Determinar ganador
            if (player1.CurrentHealth > 0)
            {
                Console.WriteLine($"{player1.Name} ha ganado la pelea!");
                player1.GainExperience(50, 100);
                player2.GainExperience(20, 50);
            }
            else
            {
                Console.WriteLine($"{player2.Name} ha ganado la pelea!");
                player2.GainExperience(50, 100);
                player1.GainExperience(20, 50);
            }

            // Restaurar salud al final del combate
            player1.CurrentHealth = player1.MaxHealth;
            player2.CurrentHealth = player2.MaxHealth;
            Console.WriteLine("\nLa pelea ha terminado.\n");
        }

        private static void TakeTurn(Character attacker, Character defender)
        {
            Console.WriteLine($"\n{attacker.Name}, es tu turno. Elige un ataque:");

            // Mostrar solo ataques disponibles (no stats completos)
            attacker.ShowAttacks();
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < attacker.Attacks.Count)
            {
                var selectedAttack = attacker.Attacks[choice];
                attacker.Attack(defender, selectedAttack);
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }

            // Mostrar solo la salud restante al final de cada turno
            Console.WriteLine($"{attacker.Name} tiene {attacker.CurrentHealth}/{attacker.MaxHealth} de salud restante.");
            Console.WriteLine($"{defender.Name} tiene {defender.CurrentHealth}/{defender.MaxHealth} de salud restante.");
        }
    }
}
