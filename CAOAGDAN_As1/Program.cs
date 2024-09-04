using System;
using System.Collections.Generic;

enum Kind { Dog, Cat, Lizard, Bird }

enum Gender { Male, Female }

interface IAnimal
{
    string Name { get; set; }
    Gender Gender { get; set; }
    string Owner { get; set; }
    void MakeSound();
}

abstract class Pet : IAnimal
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string Owner { get; set; }

    public abstract void MakeSound();

    public override string ToString()
    {
        return $"{Name} ({Gender}), Owner: {Owner}";
    }
}

class Dog : Pet
{
    public string Breed { get; set; }

    public override void MakeSound()
    {
        Console.WriteLine("Woof! Woof!");
    }

    public override string ToString()
    {
        return $"Dog - {base.ToString()}, Breed: {Breed}";
    }
}

class Cat : Pet
{
    public bool IsLonghaired { get; set; }

    public override void MakeSound()
    {
        Console.WriteLine("Meow! Meow!");
    }

    public override string ToString()
    {
        return $"Cat - {base.ToString()}, Hair Type: {(IsLonghaired ? "Longhaired" : "Shorthair")}";
    }
}

class Lizard : Pet
{
    public bool CanFly { get; set; }

    public override void MakeSound()
    {
        Console.WriteLine("...");
    }

    public override string ToString()
    {
        return $"Lizard - {base.ToString()}, Can Fly: {(CanFly ? "Yes" : "No")}";
    }
}

class Bird : Pet
{
    public bool CanFly { get; set; }

    public override void MakeSound()
    {
        Console.WriteLine("Tweet! Tweet!");
    }

    public override string ToString()
    {
        return $"Bird - {base.ToString()}, Can Fly: {(CanFly ? "Yes" : "No")}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Pet> pets = new List<Pet>();
        bool addMorePets = true;

        Console.WriteLine("Welcome to the Pet Inventory!");

        while (addMorePets)
        {
            Console.WriteLine($"\nPet {pets.Count + 1}:");

            Kind kind = PromptKind();
            string name = PromptInput("Name: ");
            Gender gender = PromptGender();
            string owner = PromptInput("Owner: ");

            Pet newPet = null;

            switch (kind)
            {
                case Kind.Dog:
                    string breed = PromptInput("Breed: ");
                    newPet = new Dog { Name = name, Gender = gender, Owner = owner, Breed = breed };
                    break;
                case Kind.Cat:
                    bool isLonghaired = PromptYesNo("Is Longhaired? (y/n): ");
                    newPet = new Cat { Name = name, Gender = gender, Owner = owner, IsLonghaired = isLonghaired };
                    break;
                case Kind.Lizard:
                    bool canFlyLizard = PromptYesNo("Can Fly? (y/n): ");
                    newPet = new Lizard { Name = name, Gender = gender, Owner = owner, CanFly = canFlyLizard };
                    break;
                case Kind.Bird:
                    bool canFlyBird = PromptYesNo("Can Fly? (y/n): ");
                    newPet = new Bird { Name = name, Gender = gender, Owner = owner, CanFly = canFlyBird };
                    break;
            }

            if (newPet != null)
            {
                pets.Add(newPet);
            }

            addMorePets = PromptYesNo("Add another pet? (y/n): ");
        }

        Console.WriteLine("\nWhich type of animal would you like to list? (Dog, Cat, Lizard, Bird, or 'All'):");
        string filterKind = Console.ReadLine();

        Console.WriteLine("\nAll pets in the inventory:");
        foreach (var pet in pets)
        {
            if (filterKind.Equals("All", StringComparison.OrdinalIgnoreCase) ||
                pet.GetType().Name.Equals(filterKind, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("* " + pet.ToString());
            }
        }
    }

    static Kind PromptKind()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Kind (Dog, Cat, Lizard, Bird):");
                string input = Console.ReadLine();
                return Enum.Parse<Kind>(input, true);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input. Please enter a valid kind (Dog, Cat, Lizard, Bird).");
            }
        }
    }

    static Gender PromptGender()
    {
        while (true)
        {
            Console.WriteLine("Gender (M/F):");
            string input = Console.ReadLine().ToUpper();

            if (input == "M")
            {
                return Gender.Male;
            }
            else if (input == "F")
            {
                return Gender.Female;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'M' for Male or 'F' for Female.");
            }
        }
    }

    static bool PromptYesNo(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' for Yes or 'n' for No.");
            }
        }
    }

    static string PromptInput(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be empty. " + message);
            input = Console.ReadLine();
        }

        return input;
    }
}
