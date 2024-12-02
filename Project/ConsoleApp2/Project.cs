using System;
using System.Collections.Generic;

class Game
{
    static void Main()
    {
        Game game = new Game(); //Creates new instance of Game Class
        game.Welcome(); //Call this method 
    }
    Player player = new Player(); //Creates a new instance of Player class
    Stack<Room> roomHistory = new Stack<Room>(); //Set up stack for room navigation
    Room? currentRoom; //Reference for keeping track of rooms
    public void Welcome()
    {
        Console.Clear(); //Clears Console so game begins on fresh screen
        Console.WriteLine("Welcome to the Woods!"); //Prints in Console
        Console.WriteLine("Please enter your name!"); //Prints in Console
        player.Name = Console.ReadLine()!; //Gets players name from console
        Console.WriteLine($"Hello, {player.Name}! Let's Begin!"); //Prints in Console
        Console.WriteLine("Type Up, Left or Right to progress forward. Type Back to return to the previous room. Type Yes or No to pick up objects."); //Prints in Console
        roomHistory.Clear(); //Clears the stack to ensure game starts fresh
        currentRoom = new Room1(); //Declares the current room and creates new instance of room 1
        roomHistory.Push(currentRoom); //Push current room on to the stack when moving
        currentRoom.Path(); //Calls path method from the current room
        while (true)
        {
            currentRoom = currentRoom.ChooseNextRoom(player, roomHistory); //Updates the current room
            if (currentRoom == null)
            {
                Exit(); //Calls exit method
                break; //Break for insurance
            }
        }
    }
    public static void Exit()
    {
        Console.WriteLine("Do you wish to play again?"); //Prints in Console
        Console.WriteLine("Type Start to play again and Exit to exit!"); //Prints in Console
        string option = Console.ReadLine()!; //Stores Users choice
        if (String.Equals(option, "Start", StringComparison.OrdinalIgnoreCase))
        {
            Game game = new Game(); //Creates new instance of game
            game.Welcome(); //Call welcome method
        }
        else if (String.Equals(option, "Exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear(); //Clears console
            Console.WriteLine("Thanks for playing!"); //Prints in Console
            Environment.Exit(0); //Exits the program
        }
        else
        {
            Console.WriteLine("Invalid input! Use Start or Exit!"); //Prints in Console
            Exit(); //Calls the exit method

        }
    }
}
class Player
{
    public string? Name { get; set; } //String to hold player name
    public int Health { get; set; } = 100; //Variable to hold health
    public bool Key { get; set; } = false; //Bool for key
    public bool Bow { get; set; } = false; //Bool for bow
    public bool Sword { get; set; } = false; //Bool for Sword
    public bool Stone { get; set; } = false; //Bool for Stone
    public bool Sheers { get; set; } = false; //Bool for sheers
    public bool wolves { get; set; } = false; //Bool for wolves
}
abstract class Room
{
    public abstract void Path(); //Setup abstract void Path for setting up each room
    public abstract Room ChooseNextRoom(Player player, Stack<Room> roomHistory); //Setup public abstract of room for storing rooms
    protected void DeductHealth(Player player, int amount)
    {
        player.Health -= amount; //Player health decrease
        if (player.Health <= 0)
        {
            Console.Clear(); //Clears console
            Console.WriteLine($"{player.Name} died!"); //Prints in Console
            Console.WriteLine("Game Over"); //Prints in Console
            Game.Exit(); //Calls exit method
        }
        else
        {
            Console.WriteLine($"You have {player.Health} health remaining!"); //Prints in Console
        }
    }
}
class Room1 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("You wake up in the forest. Looking around you see an opening in the trees(Left), some foliage you can slip through(Right) and a rock you can climb!(Up)"); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        Console.WriteLine("Which path would you like to take?"); //Prints in Console
        Console.WriteLine("You are at the starting room!"); //Prints in Console
        string option = Console.ReadLine()!; //Waits for user input
        if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You choose the wrong path and take damage!"); //Prints in Console
            DeductHealth(player, 20); //Decrease players health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Up", StringComparison.OrdinalIgnoreCase))
        {
            Room2 room2 = new Room2(); //Creates new instance of room
            roomHistory.Push(room2); //Pushes room on to the stack
            room2.Path(); //Calls path method in room
            return room2; //Returns value for room 2
        } 
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You choose the wrong path and take damage!"); //Prints in Console
            DeductHealth(player, 20); //Decrease players health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You can't go back from here"); //Prints in Console
            return this; //Returns value for this room
        }
        else
        {
            Console.WriteLine("Invalid input! Please Use Up, Back, Left and Right"); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room2 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("You climb over the rock and it leads you to an opening in the forest. There is a path continuing ahead, a gate to your right and a path to your left.");
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in Room 2!"); //Prints in Console
        string option = Console.ReadLine()!; //Hold users input
        if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase) && player.Key)
        {
            Room8 room8 = new Room8(); //Creates new instance of room
            roomHistory.Push(room8); //Pushes room on to the stack
            room8.Path(); //Calls path method in room
            return room8; //Returns value for room 8
        }
        else if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase) && !player.Key)
        {
            Console.WriteLine("You need the key to open the gate."); //Prints in Console
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Up", StringComparison.OrdinalIgnoreCase))
        {
            Room3 room3 = new Room3(); //Creates new instance of room
            roomHistory.Push(room3); //Pushes room on to the stack
            room3.Path(); //Calls path method in room
            return room3; //Returns value for room 3
        }
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase))
        {
            Room6 room6 = new Room6(); //Creates new instance of room
            roomHistory.Push(room6); //Pushes room on to the stack
            room6.Path(); //Calls path method in room
            return room6; //Returns value for room 6
        }
        else if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! Please Use Up, Back, Left and Right."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room3 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("You continue forward reaching an area with heavy foliage. Three paths are before you."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        string option; //Set variable to hold player option
        if (!player.Bow)
        {
            Console.WriteLine("You notice a bow on the ground."); //Prints in Console
            Console.WriteLine("Would you like to pick up the bow?"); //Prints in Console
            option = Console.ReadLine()!;  //Holds user input
            if (String.Equals(option, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                player.Bow = true; //Set Player's bow bool to true
            }
            else if(String.Equals(option, "No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You leave the Bow."); //Prints in Console
            }
            else{
                Console.WriteLine("Invalid input! Type Yes or No"); //Prints in Console
                return this; //Returns value for this room
            }
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the Bow Room!"); //Prints in Console
        option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You choose the wrong path and take some damage!"); //Prints in Console
            DeductHealth(player, 20); //Decrease the player's health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Up", StringComparison.OrdinalIgnoreCase))
        {
            Room5 room5 = new Room5(); //Creates new instance of room
            roomHistory.Push(room5); //Pushes room on to the stack
            room5.Path(); //Calls path method in room
            return room5; //Returns value for room 5
        }
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase) && player.Sheers)
        {
            Room4 room4 = new Room4(); //Creates new instance of room
            roomHistory.Push(room4); //Pushes room on to the stack
            room4.Path(); //Calls path method in room
            return room4; //Returns value for room 4
        }
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase) && !player.Sheers)
        {
            Console.WriteLine("You need something to cut the thicket."); //Prints in Console
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! Please Use Up, Back, Left and Right."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room4 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("As you continue walking, you enter an area with huge shrubs that block off every direction except the one you came from."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        string option; //Variable for player input
        if (!player.Stone)
        {
            Console.WriteLine("You notice a glowing stone on the ground."); //Prints in Console
            Console.WriteLine("Would you like to pick it up?"); //Prints in Console
            option = Console.ReadLine()!; //Holds user input
            if (String.Equals(option, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                player.Stone = true; //Set Players stone variable to true
            }
            else if(String.Equals(option, "No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You leave the stone."); //Prints in Console
            }
            else{
                Console.WriteLine("Invalid input! Type Yes or No"); //Prints in Console
                return this; //Returns value for this room
            }
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the Stone Room!"); //Prints in Console
        option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! You can only go Back from here.");
            return this; //Returns value for this room
        }
    }
}
class Room5 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("You enter the bosses room!"); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        if (player.Sword)
        {
            Console.Clear(); //Clears Console
            Console.WriteLine("You enter the bosses room!"); //Prints in Console
            Console.WriteLine("With the Holy Sword you slay the boss!"); //Prints in Console
            Console.WriteLine("Congratulation you beat the game!"); //Prints in Console
            return null!; //Returns value null
        }
        else if (player.Bow)
        {
            Console.WriteLine("The boss chases you out of the area!"); //Prints in Console
            DeductHealth(player, 40); //Decrease the player's health
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            DeductHealth(player, 100); //Decrease the player's health
            return null!; //Returns value null
        }
    }

}
class Room6 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("You continue down the path before it splits into three directions."); //Prints in Console

    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        if (!player.wolves)
        {
            Console.WriteLine("Before you have the chance to decide what to do, you are attacked by wolves!"); //Prints in Console
            if (!player.Bow)
            {
                Console.WriteLine("You barely manage to fend off the wolves!"); //Prints in Console
                DeductHealth(player, 20); //Decrease the player's health
            }
            else if (player.Bow)
            {
                Console.WriteLine("You quickly dispatch the wolves using your bow!"); //Prints in Console
            }
            player.wolves = true; //Set wolf attack variable to true
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the Wolf Room!"); //Prints in Console
        string option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You choose the wrong path and took some damage."); //Prints in Console
            DeductHealth(player, 20); //Decrease the player's health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Up", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You choose the wrong path and took some damage."); //Prints in Console
            DeductHealth(player, 20); //Decrease the player's health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase))
        {
            Room7 room7 = new Room7(); //Creates new instance of room
            roomHistory.Push(room7); //Pushes room on to the stack
            room7.Path(); //Calls path method in room
            return room7; //Returns value for room 7
        }
        else if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! Please Use Up, Back, Left and Right."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room7 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("As you enter this area you look around to see you are surround by thorns, You can only turn back."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        string option; //Setup variable for user input
        if (!player.Key)
        {
            Console.WriteLine("Before you turn back, you notice a key on the ground."); //Prints in Console
            Console.WriteLine("Would you like to pick them up?"); //Prints in Console
            option = Console.ReadLine()!; //Holds user input
            if (String.Equals(option, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                player.Key = true; //Set Player's key variable to true
            }
            else if(String.Equals(option, "No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You leave the keys on the ground."); //Prints in Console
            }
            else{
                Console.WriteLine("Invalid input! Type Yes or No"); //Prints in Console
                return this; //Returns value for this room
            }
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the Key Room!"); //Prints in Console
        option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! You can only go Back from here."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room8 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("As you open the gates it reveals a circular garden area. You notice a shed on one side and a drawing on a stone wall on the other."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the garden!"); //Prints in Console
        string option = Console.ReadLine()!; //Holds users input
        if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase) && player.Stone)
        {
            Room10 room10 = new Room10(); //Creates new instance of room
            roomHistory.Push(room10); //Pushes room on to the stack
            room10.Path(); //Calls path method in room
            return room10; //Returns value for room 10
        }
        else if (String.Equals(option, "Left", StringComparison.OrdinalIgnoreCase) && !player.Stone)
        {
            Console.WriteLine("You maybe able to find something to put in the wall!"); //Prints in Console
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Up", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You fail to find a path and hurt yourself."); //Prints in Console
            DeductHealth(player, 20); //Decrease the player's health
            return this; //Returns value for this room
        }
        else if (String.Equals(option, "Right", StringComparison.OrdinalIgnoreCase))
        {
            Room9 room9 = new Room9(); //Creates new instance of room
            roomHistory.Push(room9); //Pushes room on to the stack
            room9.Path(); //Calls path method in room
            return room9; //Returns value for room 9
        }
        else if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! Please Use Up, Back, Left and Right."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room9 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("As you enter the shed you notice its pretty empty."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        string option; //Setup variable for user input
        if (!player.Sheers)
        {
            Console.WriteLine("You notice a pair of sheers in the corner."); //Prints in Console
            Console.WriteLine("Would you like to pick them up?"); //Prints in Console
            option = Console.ReadLine()!; //Holds users input
            if (String.Equals(option, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                player.Sheers = true; //Sets Player's Sheers to true
            }
            else if(String.Equals(option, "No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You leave the sheers."); //Prints in Console
            }
            else{
                Console.WriteLine("Invalid input! Type Yes or No"); //Prints in Console
                return this; //Returns value for this room
            }
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the shed!"); //Prints in Console
        option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! You can only go Back from here."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}
class Room10 : Room
{
    public override void Path() //Overwrite the method for path
    {
        Console.WriteLine("As you put the stone in the wall in reveal a hidden room in the ground."); //Prints in Console
    }
    public override Room ChooseNextRoom(Player player, Stack<Room> roomHistory) //Overwrite the class for room
    {
        string option; //Setup Variable for user input
        if (!player.Sword)
        {
            Console.WriteLine("In the center of room is a beautiful sword stuck in a pedestal."); //Prints in Console
            Console.WriteLine("Would you like to pull out the sword?"); //Prints in Console
            option = Console.ReadLine()!; //Holds user input
            if (String.Equals(option, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                player.Sword = true; //Sets Player's Sword to true
            }
            else if(String.Equals(option, "No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You leave the sword."); //Prints in Console
            }
            else{
                Console.WriteLine("Invalid input! Type Yes or No"); //Prints in Console
                return this; //Returns value for this room
            }
        }
        Console.WriteLine("Which direction would you like to go?"); //Prints in Console
        Console.WriteLine("You are in the sword room!"); //Prints in Console
        option = Console.ReadLine()!; //Holds user input
        if (String.Equals(option, "Back", StringComparison.OrdinalIgnoreCase) && roomHistory.Count > 1)
        {
            roomHistory.Pop(); //Pops the last room off the stack
            return roomHistory.Peek(); //Returns value of last room on stack
        }
        else
        {
            Console.WriteLine("Invalid input! You can only go Back from here."); //Prints in Console
            return this; //Returns value for this room
        }
    }
}