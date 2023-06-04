using System;

namespace LabSix
{
    public class CreationOfMatrix
    {
        public double[,] Matrix;

        public int Dimension { get; }

        public CreationOfMatrix(int dimension)
        {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
        }

        public CreationOfMatrix(int dimension, double MinValue, double MaxValue)
        {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
            var Random = new Random();
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    Matrix[RowIndex, ColumnIndex] = Random.NextDouble() * (MaxValue - MinValue) + MinValue;
                }
            }
        }

        public double this[int RowIndex, int ColumnIndex]
        {
            get { return Matrix[RowIndex, ColumnIndex]; }
            set { Matrix[RowIndex, ColumnIndex] = value; }
        }

        public static CreationOfMatrix operator +(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одинакового размера.");

            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex]
                                                                    + SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }

        public static CreationOfMatrix operator -(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {

            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex]
                                                                    - SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }
        public static CreationOfMatrix operator *(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    double Sum = 0;
                    for (int Index = 0; Index < Result.Dimension; ++Index)
                    {
                        Sum += FirstMatrix[RowIndex, Index] * SecondMatrix[Index, ColumnIndex];
                    }
                    Result[RowIndex, ColumnIndex] = Sum;
                }
            }
            return Result;
        }

        public static bool operator >(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] <= SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator <(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] >= SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator >=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int RowCounter = 0; RowCounter < FirstMatrix.Dimension; ++RowCounter)
                {
                    if (FirstMatrix[RowIndex, RowCounter] < SecondMatrix[RowIndex, RowCounter])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator <=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] > SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator ==(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix is null)
            {
                return SecondMatrix is null;
            }

            if (SecondMatrix is null || FirstMatrix.Dimension != SecondMatrix.Dimension)
            {
                return false;
            }

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int CloumnIndex = 0; CloumnIndex < FirstMatrix.Dimension; ++CloumnIndex)
                {
                    if (FirstMatrix[RowIndex, CloumnIndex] != SecondMatrix[RowIndex, CloumnIndex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            return !(FirstMatrix == SecondMatrix.Clone());
        }

        public static explicit operator bool(CreationOfMatrix Matrix)
        {
            return Matrix != null && Matrix.Dimension > 0;
        }

        public double Determinant()
        {
            if (Dimension == 1)
            {
                return Matrix[0, 0];
            }
            else if (Dimension == 2)
            {
                return Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];
            }
            else
            {
                double Result = 0;
                int Sign = 1;
                for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
                {
                    var subMatrix = SubMatrix(RowIndex, 0);
                    Result += Sign * Matrix[RowIndex, 0] * subMatrix.Determinant();
                    Sign = -Sign;
                }
                return Result;
            }
        }

        public CreationOfMatrix Inversion()
        {
            var determinant = Determinant();
            if (determinant == 0)
            {
                throw new InvalidOperationException("Эта матрица не может быть обратной.");
            }
            var Result = new CreationOfMatrix(Dimension);

            int Sign = 1;
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    var subMatrix = SubMatrix(RowIndex, ColumnIndex);
                    Result[ColumnIndex, RowIndex] = Sign * subMatrix.Determinant() / determinant;
                    Sign = -Sign;
                }
            }
            return Result;
        }

        private CreationOfMatrix SubMatrix(int RowToRemove, int ColumnToRemove)
        {
            var SubMatrix = new CreationOfMatrix(Dimension - 1);

            int SubRow = 0;
            for (int Row = 0; Row < Dimension; ++Row)
            {
                if (Row == RowToRemove)
                    continue;

                int SubColumn = 0;
                for (int Column = 0; Column < Dimension; ++Column)
                {
                    if (Column == ColumnToRemove)
                        continue;

                    SubMatrix[SubRow, SubColumn] = Matrix[Row, Column];
                    ++SubColumn;
                }
                ++SubRow;
            }
            return SubMatrix;
        }

        public void ConvertToDiagonal(Action<CreationOfMatrix> convertDelegate)
        {
            convertDelegate(this);
        }

        public override string ToString()
        {
            string Result = "";
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    Result += $"{Matrix[RowIndex, ColumnIndex]} ";
                }
                Result += "\n";
            }
            return Result;
        }

        public int CompareTo(CreationOfMatrix other)
        {
            if (other is null)
                return -1;

            if (Dimension != other.Dimension)
                return Dimension.CompareTo(other.Dimension);

            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    int Compare = Matrix[RowIndex, ColumnIndex].CompareTo(other.Matrix[RowIndex, ColumnIndex]);
                    if (Compare != 0)
                        return Compare;
                }
            }
            return 0;
        }

        public bool Equals(CreationOfMatrix SquareMatrix)
        {
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    if (Matrix[RowIndex, ColumnIndex] != SquareMatrix.Matrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Matrix.GetHashCode();
        }

        public CreationOfMatrix Clone()
        {
            var Clone = new CreationOfMatrix(Dimension);
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    Clone[RowIndex, ColumnIndex] = Matrix[RowIndex, ColumnIndex];
                }
            }
            return Clone;
        }
    }

    public class NotInvertible : Exception
    {
        public NotInvertible() : base("Матрицу нельзя обратить.")
        {
        }
    }


    public static class MatrixExtensions //расширяющие методы
    { 
        public static CreationOfMatrix MatrixTransposition(this CreationOfMatrix FirstMatrix)
        {
            for (int IndexColumn = 0; IndexColumn < FirstMatrix.Dimension; ++IndexColumn)
            {
                for (int IndexRow = 0; IndexRow < FirstMatrix.Dimension; ++IndexRow)
                {
                    FirstMatrix.Matrix[IndexColumn, IndexRow] = FirstMatrix.Matrix[IndexRow, IndexColumn];
                }
            }
            return FirstMatrix;
        }
        public static double MatrixTrace(this CreationOfMatrix Matrix) //след матрицы
        {
            double Result = 0;
            for (int IndexColumn = 0; IndexColumn < Matrix.Dimension; ++IndexColumn)
            {
                for (int IndexRow = 0; IndexRow < Matrix.Dimension; ++IndexRow)
                {
                    if (IndexColumn == IndexRow)
                    {
                        Result += Matrix.Matrix[IndexColumn, IndexRow];
                    }
                }
            }
            return Result;
        }
    }
    
    class Operations
    {
        static void Main(string[] args)
        {
            var Random = new Random();

            var FirstMatrix = new CreationOfMatrix(3);
            for (int RowIndex = 0; RowIndex < 3; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 3; ++ColumnIndex)
                {
                    FirstMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }

            var SecondMatrix = new CreationOfMatrix(3);
            for (int RowIndex = 0; RowIndex < 3; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 3; ++ColumnIndex)
                {
                    SecondMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            
            Console.WriteLine("Меню:");
            Console.WriteLine("\n1. Генерация Матриц\n2. Сумма\n3. Разность \n4. Произведение\n" +
                              "5. Детерминант\n6. Обратная матрица А\n7.Диагональный вид матрицы А\n" +
                              "8.Выход");
            Console.WriteLine("\nВыберите действие:");
            int Choice = int.Parse(Console.ReadLine());

            Start Hand = new Start();

            Hand.HandleRequest(FirstMatrix, SecondMatrix, Choice); // запуск цепочки обязанностей

            Console.ReadKey();

                      
            void FillMatrix(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
            {
                Console.WriteLine("Матрицы:");
                Console.WriteLine(FirstMatrix.ToString());
                Console.WriteLine(SecondMatrix.ToString());
            }
            
        }
        
        public static void ConvertToDiagonal(CreationOfMatrix FirstMatrix)
        {
            Action<CreationOfMatrix> convertDelegate = delegate (CreationOfMatrix matrix) {
                for (int Column = 0; Column < matrix.Dimension; Column++)
                {
                    for (int Row = 0; Row < matrix.Dimension; Row++)
                    {
                        if (Column != Row)
                            matrix.Matrix[Column, Row] = 0;
                    }
                }
            };

            FirstMatrix.ConvertToDiagonal(convertDelegate);
            Console.WriteLine($"Матрица приведена к диагональному виду.\n {FirstMatrix}");
        }        
    }
    
    public delegate void HandleDelegate(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice);
    public abstract class Handler
    {
        public HandleDelegate HandleRequest;
        public Handler Successor { get; set; }
        public abstract void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice);
    }

    public class Start : Handler
    {
        public Start()
        {
            Successor = new Fill();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
        }
    }

    public class Fill : Handler
    {
        public Fill()
        {
            Successor = new Plus();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            if (Choice == 1)
            {
                FillMatrix(FirstMatrix, SecondMatrix);
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }
    
    public class Plus : Handler
    {
        public Plus()
        {
            Successor = new Minus();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            if (Choice == 2)
            {
                Console.WriteLine($"Сумма:\n{FirstMatrix + SecondMatrix}");
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }

    public class Minus : Handler
    {

        public Minus()
        {
            Successor = new Minus();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            if (Choice == 3)
            {
                Console.WriteLine($"Разность:\n{FirstMatrix - SecondMatrix}");
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }

    public class Mult : Handler
    {
        public Mult()
        {
            Successor = new Det();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {   
            if (Choice == 4)
            {
                Console.WriteLine($"Произведение:\n{FirstMatrix * SecondMatrix}");
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }

    public class Det : Handler
    {
        public Det()
        {
            Successor = new Inverse();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            if (Choice == 5)
            {
                Console.WriteLine($"Детерминант матрицы равен: {FirstMatrix.Determinant()}");
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }

    public class Inverse : Handler
    {
        public Inverse()
        {
            Successor = new Diagonal();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            if (Choice == 6)
            {
                try
                {
                    var InverseOfMatrix = FirstMatrix.Inversion();
                    Console.WriteLine($"Обратная матрица А:\n{InverseOfMatrix}");
                }
                catch (NotInvertible ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
            }
        }
    }


    public class Diagonal : Handler
    {
        public Diagonal()
        {
            Successor = new Back();
            HandleRequest = HandlerRequest;
        }

        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix Matrix2, int UserChoice)
        {
            if (UserChoice == 7)
            {
                Operations.ConvertToDiagonal(FirstMatrix);               
                Console.WriteLine($"Матрица диагонального вида:{FirstMatrix}\n");
            }
            else
            {
                Successor.HandleRequest(FirstMatrix, Matrix2, UserChoice);
            }
        }
    }

    public class Back : Handler
    {
        public Back()
        {
            HandleRequest = HandlerRequest;
        }
        public override void HandlerRequest(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix, int Choice)
        {
            Console.WriteLine("Такого нет");
            Successor = new Plus();

            Console.Write("Введите номер пункта из списка: ");
            Choice = int.Parse(Console.ReadLine());
            Successor.HandleRequest(FirstMatrix, SecondMatrix, Choice);
        }
    }
}
