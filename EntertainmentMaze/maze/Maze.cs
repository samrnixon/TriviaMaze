﻿using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EntertainmentMaze.maze
{
    [DataContract]
    public class Maze
    {
        private enum Location
        {
            Row = 0,
            Column = 1
        }

        private Room[,] _Rooms;
        [DataMember]
        public Player _Player { get; set; }
        [DataMember]
        private int[] PlayerLocation = new int[2];
        [DataMember]
        public int Rows { get; set; }
        [DataMember]
        public int Columns { get; set; }
        [DataMember]
        private Room[][] surrogateArray;

        private int lastTraversedRow;
        private int lastTraversedCol;
        //private bool[,] _Traversed;
        private List<(int, int)> _Traversed;

        private Room GetHeroLocation() => (_Rooms[PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column]]);
        private Room ExitLocationOfMaze() => (_Rooms[Rows - 1, Columns - 1]);

        private int MoveRowUp() => PlayerLocation[(int)Location.Row] - 1;
        private int MoveRowDown() => PlayerLocation[(int)Location.Row] + 1;
        private int MoveColumnLeft() => PlayerLocation[(int)Location.Column] - 1;
        private int MoveColumnRight() => PlayerLocation[(int)Location.Column] + 1;
        private int SameRow() => PlayerLocation[(int)Location.Row];
        private int SameColumn() => PlayerLocation[(int)Location.Column];

        public Maze() { }

        public void CompleteBuild()
        {
            BuildRooms();
            SetHeroLocation(0, 0);
        }

        private void BuildRooms()
        {
            _Rooms = new Room[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _Rooms[i, j] = new Room(i, j, Rows, Columns);
                }
            }
        }

        public Room GetLocation()
        {
            return GetHeroLocation();
        }

        internal List<(int,int)> IsSolvable()
        {
            _Traversed = new List<(int, int)>(Rows*Columns);
            _Traversed.Add((SameRow(), SameColumn()));

            if(!FindPath(SameRow(), SameColumn()))
            {
                _Traversed.Remove((_Traversed.Capacity-1,_Traversed.Capacity-1));
            }

            return _Traversed;
        }
        private bool FindPath(int playerRowLocation, int playerColumnLocation)
        {
/*            if (playerRowLocation < 0 || playerRowLocation >= Rows) return false;
            if (playerColumnLocation < 0 || playerColumnLocation >= Columns) return false;
            if (_Rooms[playerRowLocation, playerColumnLocation] == ExitLocationOfMaze()) return true;*/

            for(int x=0; x<4; x++)
            { 
                if(x==0)
                {
                    if(canTraverse(playerColumnLocation - 1, playerColumnLocation,x))
                    {
                        _Traversed.Add((playerColumnLocation - 1, playerColumnLocation));

                    }
                }
                if (x == 1)
                {
                    if (canTraverse(playerColumnLocation, playerColumnLocation + 1,x))
                    {

                    }
                }
                if (x == 2)
                {
                    if (canTraverse(playerColumnLocation + 1, playerColumnLocation,x))
                    {

                    }
                }
                if (x == 3)
                {
                    if (canTraverse(playerColumnLocation, playerColumnLocation - 1,x))
                    {

                    }
                }
            }



            return false;







/*            if (!(_Rooms[playerRowLocation, playerColumnLocation].NorthDoor is null))
            {
                if ((_Rooms[playerRowLocation, playerColumnLocation].NorthDoor.GetDoorStatus() is false))
                {
                    if (playerRowLocation - 1 == lastTraversedRow && playerColumnLocation == lastTraversedCol)
                    {
                        
                    }

                    lastTraversedRow = playerRowLocation;
                    lastTraversedCol = playerColumnLocation;
                    if (FindPath(playerRowLocation - 1, playerColumnLocation))
                    {
                        return true;
                    }
                }

            }
            if (!(_Rooms[playerRowLocation, playerColumnLocation].EastDoor is null))
            {
                if ((_Rooms[playerRowLocation, playerColumnLocation].EastDoor.GetDoorStatus()) is false)
                {
                    if (playerRowLocation == lastTraversedRow && playerColumnLocation + 1 == lastTraversedCol)
                    {

                    }
                    lastTraversedRow = playerRowLocation;
                    lastTraversedCol = playerColumnLocation;
                    if (FindPath(playerRowLocation, playerColumnLocation + 1))
                    {
                        return true;
                    }
                }

            }
            if (!(_Rooms[playerRowLocation, playerColumnLocation].SouthDoor is null))
            {

                if ((_Rooms[playerRowLocation, playerColumnLocation].SouthDoor.GetDoorStatus()) is false)
                {
                    if (playerRowLocation + 1 == lastTraversedRow && playerColumnLocation == lastTraversedCol)
                    {

                    }
                    lastTraversedRow = playerRowLocation;
                    lastTraversedCol = playerColumnLocation;
                    if (FindPath(playerRowLocation + 1, playerColumnLocation))
                    {
                        return true;
                    }
                }

            }
            if (!(_Rooms[playerRowLocation, playerColumnLocation].WestDoor is null))
            {
                if ((_Rooms[playerRowLocation, playerColumnLocation].WestDoor.GetDoorStatus()) is false)
                {
                    if (playerRowLocation == lastTraversedRow && playerColumnLocation - 1 == lastTraversedCol)
                    {

                    }
                    lastTraversedRow = playerRowLocation;
                    lastTraversedCol = playerColumnLocation;
                    if (FindPath(playerRowLocation, playerColumnLocation - 1))
                    {
                        return true;
                    }
                }

            }

            int lTR = lastTraversedRow;
            int lTC = lastTraversedCol;
            lastTraversedRow = playerRowLocation;
            lastTraversedCol = playerColumnLocation;
            traversable[playerRowLocation, playerColumnLocation] = false;
            FindPath(lTR, lTC);

            return false;*/
        }

        private bool canTraverse(int r, int c, int x)
        {
            if (r < 0 || r >= Rows) return false;
            if (c < 0 || c >= Columns) return false;

            if (x == 0)
            {
                return ((!(_Rooms[r, c].NorthDoor is null)) && ((_Rooms[r, c].NorthDoor.GetDoorStatus() is false)));
            }
            if (x == 1)
            {
                return ((!(_Rooms[r, c].EastDoor is null)) && ((_Rooms[r, c].EastDoor.GetDoorStatus() is false)));
            }
            if (x == 2)
            {
                return ((!(_Rooms[r, c].SouthDoor is null)) && ((_Rooms[r, c].SouthDoor.GetDoorStatus() is false)));
            }
            if (x == 3)
            {
                return ((!(_Rooms[r, c].WestDoor is null)) && ((_Rooms[r, c].WestDoor.GetDoorStatus() is false)));
            }
            return false;
        }

        public void SetCheatLocation()
        {
            _Rooms[PlayerLocation[0], PlayerLocation[1]].RemovePreviousPlayerLocation();
            PlayerLocation[0] = 4;
            PlayerLocation[1] = 3;
            _Rooms[PlayerLocation[0], PlayerLocation[1]].SetPlayerInRoom();
        }

        public Room GetExitLocationOfMaze() => ExitLocationOfMaze();

        private void SetHeroLocation(int rowLocation, int columnLocation)
        {
            PlayerLocation[(int)Location.Row] = rowLocation;
            PlayerLocation[(int)Location.Column] = columnLocation;
            GetHeroLocation().SetPlayerInRoom();
        }

        public void MoveHero(String direction)
        {

            switch (direction)
            {
                case "N":
                    if (!(GetHeroLocation().NorthDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(MoveRowUp(), SameColumn());
                    }
                    return;
                case "E":
                    if (!(GetHeroLocation().EastDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(SameRow(), MoveColumnRight());
                    }
                    return;
                case "S":
                    if (!(GetHeroLocation().SouthDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(MoveRowDown(), SameColumn());
                    }
                    return;
                case "W":
                    if (!(GetHeroLocation().WestDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(SameRow(), MoveColumnLeft());
                    }
                    return;
                default:
                    return;
            }
        }

        internal void DisplayHeroLocation()
        {
            Console.WriteLine($"{(PlayerLocation[(int)Location.Row] + 1).ToString()}, {(PlayerLocation[(int)Location.Column] + 1).ToString()}");
        }

        public string PrintMaze()
        {
            String entireDungeon = "";
            int j;

            for (int i = 0; i < Rows; i++)
            {

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetTopOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetMiddleOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetBottomOfRoom();
                }

                entireDungeon += "\n";

            }

            return entireDungeon;
        }

        //BeforeSerializing() and AfterSerializing() from 
        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/ff233917-eabf-47a3-8127-55fac4188b94/define-double-as-datamember?forum=wcf

        [OnSerializing]
        public void BeforeSerializing(StreamingContext ctx)
        {
            int rows = _Rooms.GetLength(0);
            int cols = _Rooms.GetLength(1);
            this.surrogateArray = new Room[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.surrogateArray[i] = new Room[cols];
                for (int j = 0; j < cols; j++)
                {
                    this.surrogateArray[i][j] = _Rooms[i, j];
                }
            }
        }

        [OnDeserialized]
        public void AfterDeserializing(StreamingContext ctx)
        {
            if (this.surrogateArray == null)
            {
                _Rooms = null;
            }
            else
            {
                int rows = this.surrogateArray.Length;
                if (rows == 0)
                {
                    _Rooms = new Room[0, 0];
                }
                else
                {
                    int cols = this.surrogateArray[0].Length;
                    for (int i = 1; i < rows; i++)
                    {
                        if (this.surrogateArray[i].Length != cols)
                        {
                            throw new InvalidOperationException("Surrogate array does not correspond to the original");
                        }
                    }

                    _Rooms = new Room[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            _Rooms[i, j] = this.surrogateArray[i][j];
                        }
                    }
                }
            }
        }
    }
}
