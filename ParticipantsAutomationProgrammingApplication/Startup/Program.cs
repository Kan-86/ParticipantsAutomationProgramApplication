// See https://aka.ms/new-console-template for more information

using ParticipantsAutomationProgrammingApplication.Services;

ParticipantService participantService = new ParticipantService();

Console.WriteLine("Welcome to dating application \n" +
    "Please look at the options \n" +
    "Type the number to select the option. \n \n");

Console.WriteLine("\t 1. Look at all Participants.");
Console.WriteLine("\t 2. Create a participant.");
Console.WriteLine("\t 3. Match participant.");
Console.WriteLine("\t 4. Find Participant.");


Console.WriteLine("\n\n\n");

var options = Console.ReadLine();

switch (options)
{
    case "1":
        Console.Clear();
        Console.WriteLine("These are the Participants:");
        participantService.GetAllParticipants();
        break;
    case "2":
        Console.Clear();
        Console.WriteLine("Participant Creation Screen:");
        break;
    case "3":
        Console.Clear();
        Console.WriteLine("Participant Match Screen:");
        break;
    case "4":
        Console.Clear();
        Console.WriteLine("Participant Screen:");
        break;
    default:
        break;
}



