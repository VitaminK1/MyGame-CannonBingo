using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsoleApp8
{
    class Program
    {
        static string playerRow = "Z";
        static int pR= 9;
        static string playerColumn = "Z";
        static int pC= 9;
        static string cannonRow = "Z";
        static int cR= 9;
        static string cannonColumn = "Z";
        static int cC= 9;
        static string destroyedMark = "O";
        static string firstTurn = "O";
        static int turnCount = 0;
        static int roundCount = 0;
        const int boardSize = 5;

        //static List<string> history = new List<String> { };
        static string[,] board = new string[boardSize, boardSize]
        {
            {" "," "," "," "," " },
            {" "," "," "," "," " },
            {" "," "," "," "," " },
            {" "," "," "," "," " },
            {" "," "," "," "," " }
        };
        static void Main(string[] args)
        {           
            Introduction();
            while (!CheckWinner("O") && !CheckWinner("X") ) 
            {
                switch (destroyedMark)
                {
                    case "O":
                        firstTurn = "O";
                        break;
                    case "X":
                        firstTurn = "X";
                        break;
                    default:
                        break;
                }
                switch (firstTurn)
                {
                    case "O":
                        Turn("O");
                        RecoverTile();
                        Turn("X");
                        Cannon();
                        break;

                    case "X":
                        Turn("X");
                        RecoverTile();
                        Turn("O");
                        Cannon();
                        break;
                }
            }
            Congrats(); 
        }
        static void Introduction()
        {
            Console.Title = "Cannon Bingo made by SNUGDC VitaminK1";
            Console.WriteLine("                             ");
            Console.WriteLine("     ########################");
            Console.WriteLine("     #                      #");
            Console.WriteLine("     #     Cannon Bingo     #");
            Console.WriteLine("     #                      #");
            Console.WriteLine("     ########################");
            Console.WriteLine("                             ");
            Console.WriteLine("  > Press Any Key to Continue. <");
            Console.WriteLine("                             ");
            Console.ReadLine();
            Console.Clear();
        }
        /*
        static void Menu()
        {
        Console.WriteLine("                             ");
        Console.WriteLine("     ########################");
        Console.WriteLine("     #                      #");
        Console.WriteLine("     #      M  E  N  U      #");
        Console.WriteLine("     #                      #");
        Console.WriteLine("     ########################");
        Console.WriteLine("                             ");
        Console.WriteLine("   1. Game Start             ");
        Console.WriteLine("   2. Game Rule              ");
        Console.WriteLine("   3. Game Exit              ");
        Console.WriteLine("                             ");
        Console.WriteLine("   > Press Number ( 1 - 3 ) :");
        string menuSelect = Console.ReadLine();
        int menuNo = Int32.Parse(menuSelect);
        
        switch (menuNo)
        {
            case 1:
                break;
            case 2:
                Rule();
            case 3:
                Exit();
            default:
                Console.WriteLine("Please write one of 1, 2, 3 ");

        }
        }
        */ // Do Later
        static void Turn(string player)
        {
            turnCount++;
            //history.Insert(3 * ( turnCount + roundCount ) - 3, player);
            PrintBoard();

            Console.WriteLine("== Turn {0} == [ Player {1} ]", turnCount, player);
            if (!CheckFullBoard())
            {
                Console.Write("Player {0} : ' {0} '를 놓고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) >> ", player);
                playerRow = Console.ReadLine();

                while (!CheckNoErrorRow(playerRow))
                {
                    MessageBox.Show(" >> Please write 1, 2, 3, 4 or 5.", "Oops!");
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("== Turn {0} == [ Player {1} ]", turnCount, player);
                    Console.Write("Player {0} : ' {0} '를 놓고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) >> ", player);
                    playerRow = Console.ReadLine();
                }
                
                Console.Write("Player {0} : ' {0} '를 놓고 싶은 자리의 열번호를 입력해주세요. ( A - E ) >> ", player);
                playerColumn = Console.ReadLine();

                while (!CheckNoErrorColumn(playerColumn))
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("== Turn {0} == [ Player {1} ]", turnCount, player);
                    Console.WriteLine("Player {0} : ' {0} '를 놓고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) >> {1}", player,playerRow);
                    MessageBox.Show(" >> Please write A, B, C, D or E.", "Oops!");
                    Console.Write("Player {0} : ' {0} '를 놓고 싶은 자리의 열번호를 입력해주세요. ( A - E ) >> ", player);
                    playerColumn = Console.ReadLine();
                }

                //history.Insert(3 * (turnCount + roundCount) - 2, playerRow)
                //history.Insert(3 * (turnCount + roundCount) - 1, playerColumn);
                pR = Int32.Parse(playerRow) - 1;
                pC = StrToInt(playerColumn);

                Console.Clear();

                switch (board[pR, pC])
                {
                    case "O":
                        MessageBox.Show(" >> O is already on that tile. (" + playerRow + "," + playerColumn + ") Please select another tile.", "Oops!");
                        turnCount--;
                        Turn(player);
                        break;

                    case "X":
                        MessageBox.Show(" >> X is already on that tile. (" + playerRow + "," + playerColumn + ") Please select another tile.", "Oops!");
                        turnCount--;
                        Turn(player);
                        break;

                    case "V":
                        MessageBox.Show(" >> V is already on that tile. (" + playerRow + "," + playerColumn + ") Please select another tile.", "Oops!");
                        turnCount--;
                        Turn(player);
                        break;

                    case " ":
                        board[pR, pC] = player;
                        break;
                }
            }
            else
            {
                MessageBox.Show(" >> All tiles are filled with O, X or V. This turn is skipped.", "Oops!");
                Console.Clear();
            }


        }
        public static int StrToInt(string alphabet)
        {
            switch (alphabet)
            {
                case "A":
                case "a":
                    return 0;
                case "B":
                case "b":
                    return 1;
                case "C":
                case "c":
                    return 2;
                case "D":
                case "d":
                    return 3;
                case "E":
                case "e":
                    return 4;
                default:
                    return 9;
            }
        }
        static void RecoverTile()
        {
            for (int i = 0; i <= 4; i++)
            {
                for ( int j = 0; j <= 4; j++)
                {
                    if( board[i,j] == "V" )
                    {
                        board[i,j] = " ";
                    }
                }
            }
        }
        public static void PrintBoard()
        {
            Console.WriteLine("");
            Console.WriteLine("        A     B     C     D     E   ");
            Console.WriteLine("      _____________________________");
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("  1  |  {0}  |  {1}  |  {2}  |  {3}  |  {4}  |",board[0, 0],board[0, 1], board[0, 2], board[0, 3], board[0, 4]);
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("     |-----------------------------|");
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("  2  |  {0}  |  {1}  |  {2}  |  {3}  |  {4}  |", board[1, 0], board[1, 1], board[1, 2], board[1, 3], board[1, 4]);
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("     |-----------------------------|");
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("  3  |  {0}  |  {1}  |  {2}  |  {3}  |  {4}  |", board[2, 0], board[2, 1], board[2, 2], board[2, 3], board[2, 4]);
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("     |-----------------------------|");
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("  4  |  {0}  |  {1}  |  {2}  |  {3}  |  {4}  |", board[3, 0], board[3, 1], board[3, 2], board[3, 3], board[3, 4]);
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("     |-----------------------------|");
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("  5  |  {0}  |  {1}  |  {2}  |  {3}  |  {4}  |", board[4, 0], board[4, 1], board[4, 2], board[4, 3], board[4, 4]);
            Console.WriteLine("     |     |     |     |     |     |");
            Console.WriteLine("     |_____________________________|");
        }
        static void Cannon()
        {
            if (!CheckFullBoard())
            {
                roundCount++;
                //history.Insert(9*roundCount - 3, "V");
                PrintBoard();
                Console.WriteLine("== Round {0} == [ Cannon ]", roundCount);

                Console.Write($"Player O : 대포를 발사하고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) : ");
                cannonRow = Console.ReadLine();

                while (!CheckNoErrorRow(cannonRow))
                {
                    MessageBox.Show(" >> Please write 1, 2, 3, 4 or 5.", "Oops!");
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("== Round {0} == [ Cannon ]", roundCount);
                    Console.Write($"Player O : 대포를 발사하고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) : ");
                    cannonRow = Console.ReadLine();
                }


                Console.Clear();
                PrintBoard();
                Console.WriteLine("== Round {0} == [ Cannon ]", roundCount);
                Console.WriteLine($"Player O : 대포를 발사하고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) : DONE ");
                Console.Write($"Player X : 대포를 발사하고 싶은 자리의 열번호를 입력해주세요. ( A - E ) : ");
                cannonColumn = Console.ReadLine();

                while (!CheckNoErrorColumn(cannonColumn))
                {
                    MessageBox.Show(" >> Please write A, B, C, D or E.", "Oops!");
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("== Round {0} == [ Cannon ]", roundCount);
                    Console.WriteLine($"Player O : 대포를 발사하고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) : DONE ");
                    Console.Write($"Player X : 대포를 발사하고 싶은 자리의 열번호를 입력해주세요. ( A - E ) : ");
                    cannonColumn = Console.ReadLine();
                }

                //history.Insert(9 * roundCount - 2, "cannonRow");
                //history.Insert(9 * roundCount - 1, "cannonColumn");
                cR = Int32.Parse(cannonRow) - 1;
                cC = StrToInt(cannonColumn);

                switch (board[cR, cC])
                {
                    case "O":
                        board[cR, cC] = "V";
                        destroyedMark = "O";
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine(" >> O is destroyed. The crator by the cannon will be recovered after 1 turn.");
                        break;

                    case "X":
                        board[cR, cC] = "V";
                        destroyedMark = "X";
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine(" >> X is destroyed. The crator by the cannon will be recovered after 1 turn.");
                        break;
                    case " ":
                        board[cR, cC] = "V";
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine(" >> Nothing is destroyed. The crator by the cannon will be recovered after 1 turn.");
                        break;
                    default:
                        roundCount--;
                        Console.Clear();
                        MessageBox.Show(" >> ERROR");
                        Cannon();
                        break;
                }
            }
            else
            {
                while (!CheckWinner("O") && !CheckWinner("X"))
                {
                    DeathMatch();
                }
            }
        }
        public static bool CheckWinner(string player) 
        {
            //check row
            for (int i = 0; i < 5 ; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ( board[i,j] ==  player && j != 4 )
                    {
                        continue;
                    }
                    else if ( board[i,j] == player && j == 4 )
                    {
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //check column
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[j, i] == player && j != 4)
                    {
                        continue;
                    }
                    else if (board[j, i] == player && j == 4)
                    {
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //check diagonal
            for (int i = 0; i < 5; i++)
            {
                if ( board[i,i] == player && i != 4 )
                {
                    continue;
                }
                else if ( board[i,i] == player && i == 4 )
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (board[i, 4 - i] == player && i != 4)
                {
                    continue;
                }
                else if (board[i, 4 - i] == player && i == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static bool CheckFullBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (board[i, k] == " ")
                    {
                        return false;
                    }

                    else
                    {
                        continue;
                    }
                }
            }
            return true;
        }
        static void DeathMatch()
        {
            roundCount++;
            //history.Insert(9*roundCount-3,"D");
            PrintBoard();
            
            Console.WriteLine("== Round {0} == [ DEATHMATCH ]", roundCount);
            Console.Write($"Player O : 대포를 발사하고 싶은 자리의 행번호를 입력해주세요. ( 1 - 5 ) : ");
            cannonRow = Console.ReadLine();
            if (CheckNoErrorRow(cannonRow))
            {
                //history.Insert(9*roundCount-2,cannonRow);
                cR = Int32.Parse(cannonRow) - 1;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                MessageBox.Show(" >> Please write 1, 2, 3, 4 or 5.", "Oops!");
                roundCount--;
                Cannon();
            }

            Console.Write($"Player X : 대포를 발사하고 싶은 자리의 열번호를 입력해주세요. ( A - E ) : ");
            cannonColumn = Console.ReadLine();
            if (CheckNoErrorColumn(cannonColumn))
            {
                //history.Insert(9*roundCount-1,cannonColumn);
                cC = StrToInt(cannonColumn);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                MessageBox.Show(" >> Please write A, B, C, D or E.", "Oops!");
                roundCount--;
                Cannon();
            }

            switch (board[cR, cC])
            {
                case "O":
                    board[cR, cC] = "X";
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("O is destroyed. On DeathMatch, X is placed on that tile.");
                    break;

                case "X":
                    board[cR, cC] = "V";
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("X is destroyed. On DeathMatch, O is placed on that tile.");
                    break;
                default:
                    roundCount--;
                    Console.Clear();
                    MessageBox.Show("ERROR - Please enter right number and alphabet.");
                    DeathMatch();
                    break;
            }
        }
        static void Congrats()
        {
            Console.Clear();
            Console.WriteLine("     ########################");
            Console.WriteLine("     #                      #");
            if (CheckWinner("O") && CheckWinner("X"))
            {
                Console.WriteLine("     #      D  R  A  W      #");
            }
            else if (CheckWinner("O"))
            {
                Console.WriteLine("     #    PLAYER  O  WON    #");
            }
            else if (CheckWinner("X"))
            {
                Console.WriteLine("     #    PLAYER  X  WON    #");
            }
            else
            {
                Console.WriteLine("     #    error - noWinner  #");
            }
            Console.WriteLine("     #                      #");
            Console.WriteLine("     ########################");

            Console.WriteLine();
            Console.WriteLine(">> Turn Count : {0} ", turnCount);
            Console.WriteLine(">> Round Count : {0} ", roundCount);
            Console.WriteLine();
            Console.Write(">> Press any key to exit.");
            Console.ReadLine();
        }
        public static bool CheckNoErrorRow(string input)
        {
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    return true;
                default:
                    return false;
            }
        }
        public static bool CheckNoErrorColumn(string input)
        {
            switch (input)
            {
                case "A":
                case "a":
                case "B":
                case "b":
                case "C":
                case "c":
                case "D":
                case "d":
                case "E":
                case "e":
                    return true;
                default:
                    return false;
            }
        }
    }
}
