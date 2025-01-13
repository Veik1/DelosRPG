using System;
using System.Collections.Generic;
using System.Linq;
using Characters;

namespace Services
{
    public class MenuService
    {
        private static List<Character> characters = new List<Character>();

        // Mostrar el menú principal
        public static void ShowMainMenu()
        {
            Console.WriteLine("\nBienvenido a DelosRPG");

            while (true)
            {
                // Opciones del menú
                Console.WriteLine("\n¿Qué deseas hacer?");
                Console.WriteLine("1. Crear un nuevo personaje");
                Console.WriteLine("2. Ver personajes creados y sus ataques");
                Console.WriteLine("3. Seleccionar personajes para pelear");
                Console.WriteLine("4. Salir\n");

                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    // Crear un nuevo personaje
                    CreateAndAddCharacter();
                }
                else if (option == 2)
                {
                    // Mostrar personajes creados y sus ataques
                    ShowCharacterAttacks();
                }
                else if (option == 3)
                {
                    // Selección de personajes para pelear
                    SelectCharactersForFight();
                }
                else if (option == 4)
                {
                    break;
                }
            }
        }

        // Crear y añadir un personaje
        public static void CreateAndAddCharacter()
        {
            Console.Write("\nIntroduce el nombre del personaje: ");
            string name = Console.ReadLine();
            Character character = CreateCharacter(name);
            if (character != null)
            {
                characters.Add(character);
                Console.WriteLine($"\nPersonaje {character.Name} creado con éxito.");
            }
        }

        // Crear un personaje según la clase seleccionada
        public static Character CreateCharacter(string name)
        {
            Console.WriteLine("\nElige una clase: ");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage\n");
            int choice = Convert.ToInt32(Console.ReadLine());

            Character character = null;

            if (choice == 1)
            {
                character = new Warrior(name);
            }
            else if (choice == 2)
            {
                character = new Mage(name);
            }
            else
            {
                Console.WriteLine("\nError, opción incorrecta.\n");
                return null;
            }

            return character;
        }

        // Mostrar los ataques de un personaje
        public static void ShowCharacterAttacks()
        {
            Console.WriteLine("\nSelecciona un personaje para ver sus ataques:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].GetType().Name})");
            }

            int characterIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            if (characterIndex >= 0 && characterIndex < characters.Count)
            {
                Character selectedCharacter = characters[characterIndex];
                selectedCharacter.ShowAttacks();  // Mostrar ataques del personaje seleccionado
            }
            else
            {
                Console.WriteLine("Selección no válida.");
            }
        }

        // Seleccionar personajes para pelear
        public static void SelectCharactersForFight()
        {
            // Filtrar personajes vivos (con salud > 0)
            List<Character> availableCharacters = characters.Where(c => c.CurrentHealth > 0).ToList();

            if (availableCharacters.Count < 2)
            {
                Console.WriteLine("No hay suficientes personajes vivos para pelear.");
                return;
            }

            Character player1 = SelectFirstCharacterForFight(ref availableCharacters);
            if (player1 == null) return;

            Character player2 = SelectSecondCharacterForFight(ref availableCharacters);
            if (player2 == null) return;

            if (player1 != player2)
            {
                // Inicia el combate
                StartCombat(player1, player2);
            }
            else
            {
                Console.WriteLine("No puedes seleccionar el mismo personaje para pelear.");
            }
        }

        // Función para verificar que los personajes seleccionados están vivos
        private static Character SelectFirstCharacterForFight(ref List<Character> availableCharacters)
        {
            Console.WriteLine("\nSelecciona un personaje para pelear:");

            for (int i = 0; i < availableCharacters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableCharacters[i].Name} ({availableCharacters[i].GetType().Name})");
            }

            int characterIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (characterIndex >= 0 && characterIndex < availableCharacters.Count)
            {
                // Obtener el personaje seleccionado
                Character selectedCharacter = availableCharacters[characterIndex];

                // Eliminarlo de la lista para que no aparezca de nuevo en la segunda selección
                availableCharacters.RemoveAt(characterIndex);

                return selectedCharacter;
            }
            else
            {
                Console.WriteLine("Selección no válida.");
                return null;
            }
        }

        // Función para seleccionar el segundo personaje para pelear
        private static Character SelectSecondCharacterForFight(ref List<Character> availableCharacters)
        {
            Console.WriteLine("\nSelecciona un segundo personaje para pelear:");

            for (int i = 0; i < availableCharacters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableCharacters[i].Name} ({availableCharacters[i].GetType().Name})");
            }

            int characterIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (characterIndex >= 0 && characterIndex < availableCharacters.Count)
            {
                // Obtener el personaje seleccionado
                Character selectedCharacter = availableCharacters[characterIndex];
                return selectedCharacter;
            }
            else
            {
                Console.WriteLine("Selección no válida.");
                return null;
            }
        }


        // Iniciar el combate entre dos personajes
        public static void StartCombat(Character player1, Character player2)
        {
            Console.WriteLine($"\n¡Se viene, se viene, la pelea entre {player1.Name} y {player2.Name} ha comenzado chavales!\n");

            // Lógica de combate
            while (player1.CurrentHealth > 0 && player2.CurrentHealth > 0)
            {
                // Mostrar estadísticas de los personajes
                Console.WriteLine($"\n{player1.Name}: Nivel {player1.Level} - Salud: {player1.CurrentHealth}/{player1.MaxHealth}");
                Console.WriteLine($"{player2.Name}: Nivel {player2.Level} - Salud: {player2.CurrentHealth}/{player2.MaxHealth}");

                // Turno del jugador 1
                player1.ShowAttacks();
                int attackChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                if (attackChoice >= 0 && attackChoice < player1.Attacks.Count)
                {
                    player1.Attack(player2, player1.Attacks[attackChoice]);
                }

                // Comprobar si el jugador 2 ha sido derrotado
                if (player2.CurrentHealth <= 0)
                {
                    Console.WriteLine($"\n{player2.Name} ha sido derrotado.");
                    break;
                }

                // Turno del jugador 2
                player2.ShowAttacks();
                attackChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                if (attackChoice >= 0 && attackChoice < player2.Attacks.Count)
                {
                    player2.Attack(player1, player2.Attacks[attackChoice]);
                }

                // Comprobar si el jugador 1 ha sido derrotado
                if (player1.CurrentHealth <= 0)
                {
                    Console.WriteLine($"{player1.Name} ha sido derrotado.");
                }
            }

            // Restablecer salud a un nivel mínimo después de la pelea (Ejemplo: 50% de la salud máxima)
            player1.CurrentHealth = player1.MaxHealth;
            player2.CurrentHealth = player2.MaxHealth;

            Console.WriteLine($"\nLa pelea ha terminado chaval, gravias vuelva prontos!");
        }


    }
}
