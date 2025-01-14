using System;
using System.Collections.Generic;
using Characters;
using Services;

namespace Services
{
    public static class MenuService
    {
        private static List<Character> characters = new List<Character>();

        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nBienvenido a DelosRPG");
                Console.WriteLine("\n1. Crear un nuevo personaje");
                Console.WriteLine("2. Ver personajes creados");
                Console.WriteLine("3. Seleccionar personajes para pelear");
                Console.WriteLine("4. Salir\n");
                int option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");

                if (option == 1)
                {
                    CreateAndAddCharacter();
                }
                else if (option == 2)
                {
                    ShowCharacter();
                }
                else if (option == 3)
                {
                    SelectCharactersForFight();
                }
                else if (option == 4)
                {
                    break;
                }
            }
        }

        private static void CreateAndAddCharacter()
        {
            Console.Write("Introduce el nombre del personaje: ");
            string name = Console.ReadLine();
            Character character = CreateCharacter(name);
            if (character != null)
            {
                characters.Add(character);
                Console.WriteLine($"{character.Name} ha sido creado.");
            }
        }

        private static Character CreateCharacter(string name)
        {
            Console.WriteLine("Elige una clase: ");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Paladin\n");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1) return new Warrior(name);
            if (choice == 2) return new Mage(name);
            if (choice == 3) return new Paladin(name);

            return null;
        }

        private static void ShowCharacter()
        {
            Console.WriteLine("Selecciona un personaje:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name}");
            }

            int characterIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            if (characterIndex >= 0 && characterIndex < characters.Count)
            {
                var selectedCharacter = characters[characterIndex];
                selectedCharacter.ShowStats();
            }
        }

        private static void SelectCharactersForFight()
        {
            if (characters.Count < 2) 
            {
                Console.WriteLine("\nSe necesitan al menos dos personajes para pelear.\n");
                return;
            }

            // Crear una lista temporal que no afecte a la lista original
            var tempCharacters = new List<Character>(characters);

            Console.WriteLine("\nSelecciona el primer personaje:");
            for (int i = 0; i < tempCharacters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tempCharacters[i].Name}");
            }

            int firstIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            Character player1 = tempCharacters[firstIndex];

            // Eliminar el personaje seleccionado de la lista temporal
            tempCharacters.RemoveAt(firstIndex);

            Console.WriteLine("\nSelecciona el segundo personaje:");
            for (int i = 0; i < tempCharacters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tempCharacters[i].Name}");
            }
            int secondIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            Character player2 = tempCharacters[secondIndex];

            if (player1 != player2)
            {
                CombatService.StartCombat(player1, player2);
            }
            else
            {
                Console.WriteLine("\nNo puedes seleccionar el mismo personaje.\n");
            }
        }

    }
}
