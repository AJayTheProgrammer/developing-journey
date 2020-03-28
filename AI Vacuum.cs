using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Anthony Jordan


namespace AI_Assignment01
{
    class VAgent
    {

        //Variables that the AI vacuum uses to decide if the current room is dirty
        public String vacuumLocation;
        public bool isADirty;
        public bool isBDirty;
        public bool isAChecked;
        public bool isBChecked;

        public VAgent()
        {
            vacuumLocation = "Orgin";
            isAChecked = false;
            isBChecked = false ;
        }


        // Method that makes checks to see whether or not the AI vacuum is Room 'A' or 'B' 
        // if the room is dirty, it cleans that current room and changes the state to cleaned
        // it then moves to the other room to see if it cleaned or not.
        public void DecideToSuck()
        {
            if(vacuumLocation =="Room A" && isADirty == true)
            {
                isADirty = false;
                Console.WriteLine("Room A has been cleaned.");
                isAChecked = true;
                MoveToOrgin();
                
            }

            if (vacuumLocation == "Room A" && isADirty == false)
            {
                Console.WriteLine("Room A was already clean.");
                isAChecked = true;
                MoveToOrgin();
                
            }

            if (vacuumLocation == "Room B" && isBDirty == true)
            {
                isBDirty = false;
                Console.WriteLine("Room B has been cleaned.");
                isBChecked = true;
                MoveToOrgin();
               
            }

            if (vacuumLocation == "Room B" && isBDirty == false)
            {
                Console.WriteLine("Room B was already clean.");
                isBChecked = true;
                MoveToOrgin();
                
            }

        }


        // This method moves the AI vacuum to the origin (start point)

        public void MoveToOrgin()
        {
            if (isAChecked ==  false)
            {
                MoveLeft();
            }

            if (isBChecked == false)
            {
                MoveRight();
            }

            vacuumLocation = "Orgin";
            Console.WriteLine("Vacuum has moved back to the orgin");
        }


        // This method randomizes the rooms current state to dirty or not 
        // using the Random.Next

        public void MakeRoomsDirty()
        {

            Random random = new Random();
            int dirtyLocation = random.Next(1,51);

            if (dirtyLocation > 30)
            {
                isADirty = true;
                isBDirty = false;
            }
            

            if (dirtyLocation < 30)
            {
                isBDirty = true;
                isADirty = false;
            }

            if (dirtyLocation > 40)
            {
                isBDirty = true;
                isADirty = true;
            }

        }

        // Moves the AI vaccuum right of the origin

        public void MoveRight()
        {
            Console.WriteLine("Vacuum is in Room B");
            vacuumLocation = "Room B";
            DecideToSuck();
        }
        // Moves the AI vaccuum left of the origin
        public void MoveLeft()
        {
            Console.WriteLine("Vacuum is in Room A");
            vacuumLocation = "Room A";
            DecideToSuck();

        }

        // This particular method makes the AI vacuum choose randomly which room to clean first.
        public void ChooseRoomToClean()
        {
            Random random = new Random();
            int randomChoice = random.Next(1,3);

            if(randomChoice == 1)
            {
                MoveLeft();
            }

            if (randomChoice == 2)
            {
                MoveRight();
            }
        }


        static void Main(string[] args)
        {
            VAgent vacuum = new VAgent();
            vacuum.MakeRoomsDirty();
            vacuum.ChooseRoomToClean();
        }
    }
}
