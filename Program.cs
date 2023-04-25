using Pariveda_Challenge;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;
using System.Threading.Tasks;

RunMainMenu();

static void RunMainMenu(){
    Console.Clear();
    string prompt = "MAIN MENU:";
    string[] options = {"Student Portal", "Admin Portal", "Exit"};
    Menu mainMenu = new Menu(prompt, options);
    int selectedIndex = mainMenu.MakeChoice();

    switch (selectedIndex){
        case 0:
            RunStudentPortalMenu();
            break;
        case 1:
            Login();
            break;
        case 2:
            ExitProgram();
            break;
    }
}

static void ExitProgram(){
    Environment.Exit(0);
}

static void GoBack(){

}

static void RunStudentPortalMenu(){
    Console.Clear();
    string prompt = "STUDENT PORTAL MENU:";
    string[] options = {"Add RSVP", "Edit RSVP", "Cancel RSVP","Exit"};
    Menu RSVPMenu = new Menu(prompt, options);
    int selectedIndex = RSVPMenu.MakeChoice();
    
    switch (selectedIndex){
        case 0:
            AddRSVP();
            break;
        case 1:
            EditRSVP();
            break;
        case 2:
            CancelRSVP();
            break;
        case 3:
            GoBack();
            break;
    }
    RunMainMenu();
}

static void AddRSVP(){
    // System.Console.WriteLine("Placeholder for add rsvp...Press any key");
    // Console.ReadKey();
    Event[] events = new Event[500];
    RSVP[] rsvps = new RSVP[500];
    eventUtility eUtility = new eventUtility(events);
    rsvpUtility rUtility = new rsvpUtility(rsvps,events);
    rUtility.AddRSVP();
    RunStudentPortalMenu();
}

static void EditRSVP(){
    Event[] events = new Event[500];
    RSVP[] rsvps = new RSVP[500];
    eventUtility eUtility = new eventUtility(events);
    rsvpUtility rUtility = new rsvpUtility(rsvps,events);
    rUtility.EditRSVP();
    // System.Console.WriteLine("Placeholder for edit rsvp...Press any key");
    // Console.ReadKey();
    RunStudentPortalMenu();
}

static void CancelRSVP(){
    Event[] events = new Event[500];
    RSVP[] rsvps = new RSVP[500];
    eventUtility eUtility = new eventUtility(events);
    rsvpUtility rUtility = new rsvpUtility(rsvps,events);
    System.Console.WriteLine("Placeholder for cancel rsvp...Press any key");
    Console.ReadKey();
    rUtility.CancelRSVP();
    RunStudentPortalMenu();
}
static void Login(){
    System.Console.Write("Enter Username: ");
    string username = Console.ReadLine();
    System.Console.Write("Enter Password: ");
    string password = Console.ReadLine();
    Admin[] admins = new Admin[10];
    adminUtility aUtility = new adminUtility(admins);
    aUtility.GetAllAdminsFromFile();
    if(aUtility.VerifyAdmin(username, password)){
        System.Console.WriteLine("Login Successfully. Press any key to continue...");
        Console.ReadKey();
        RunAdminPortalMenu();
    } else{
        System.Console.WriteLine("Wrong username/password. Press any key to exit...");
        Console.ReadKey();
    }
    RunMainMenu();
}
static void RunAdminPortalMenu(){
    Console.Clear();
    string prompt = "ADMIN PORTAL MENU:";
    string[] options = {"Add Event", "Edit Event", "Cancel Event","Exit"};
    Menu ManageEventMenu = new Menu(prompt, options);
    int selectedIndex = ManageEventMenu.MakeChoice();
    
    switch (selectedIndex){
        case 0:
            AddEvent();
            break;
        case 1:
            EditEvent();
            break;
        case 2:
            CancelEvent();
            break;
        case 3:
            GoBack();
            break;
    }
    RunMainMenu();
} 
static void AddEvent(){
    Event[] events = new Event[500];
    eventUtility eUtility = new eventUtility(events);
    eUtility.AddEvent();
    RunAdminPortalMenu();
}
static void EditEvent(){
    Event[] events = new Event[500];
    eventUtility eUtility = new eventUtility(events);
    eUtility.EditEvent();
    RunAdminPortalMenu();
}
static void CancelEvent(){
    Event[] events = new Event[500];
    eventUtility eUtility = new eventUtility(events);
    eUtility.CancelEvent();
    RunAdminPortalMenu();
}


