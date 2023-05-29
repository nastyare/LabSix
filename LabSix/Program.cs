using System;

namespace LabSix
{
    public static class MatrixExtensions
    {
        //Транспортация 
        public static CreationOfMatrix MatrixTransposition(this CreationOfMatrix A)
        {
            for (int IndexColumn = 0; IndexColumn < A.Dimension; IndexColumn++)
            {
                for (int IndexRow = 0; IndexRow < A.Dimension; IndexRow++)
                {
                    A.Matrix[IndexColumn, IndexRow] = A.Matrix[IndexRow, IndexColumn];
                }
            }
            return A;
        }
        public static double MatrixTrace(this CreationOfMatrix matrix)
        {
            double Result = 0;
            for (int IndexColumn = 0; IndexColumn < matrix.Dimension; ++IndexColumn)
            {
                for (int IndexRow = 0; IndexRow < matrix.Dimension; ++IndexRow)
                {
                    if (IndexColumn == IndexRow)
                    {
                        Result += matrix.Matrix[IndexColumn, IndexRow];
                    }
                }
            }
            return Result;
        }
    }
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
            //Console.WriteLine($"Первая матрица: \n{FirstMatrix}");

            var SecondMatrix = new CreationOfMatrix(3);
            for (int RowIndex = 0; RowIndex < 3; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 3; ++ColumnIndex)
                {
                    SecondMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"Вторая матрица: \n{SecondMatrix}");

            void ConvertToDiagonal(CreationOfMatrix FirstMatrix)
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

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("\n1. Генерация Матриц\n2. Вычисление 2х матриц\n3. Найти Определитель матрицы А\n4. Обратная матрица А\n5. Транспортирование матрицы А\n6. Сумма элементов диагонали Матрицы А\n7.Диагональный вид матрицы А\n8. Вывести матрицу\n9.Выход");
                Console.WriteLine("\nВыберите действие:");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        //CreationOfMatrix(FirstMatrix)
                        FillMatrix(FirstMatrix, SecondMatrix);
                        break;
                    case "2":
                        CalculationsMenu(FirstMatrix, SecondMatrix);
                        break;
                    case "3":
                        Console.WriteLine($"Determinant of MatrixA: {FirstMatrix.Determinant()}");
                        break;
                    case "4":
                        //MatrixInf inverseA = MatrixA.Inverse(MatrixA);
                        //Console.WriteLine($"Инверсия Матрицы:\n{inverseA}");
                        try
                        {
                            var InverseA = FirstMatrix.Inversion();
                            Console.WriteLine($"Обратная матрица А:\n{InverseA}");
                        }
                        catch (NotInvertible ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "5":
                        var transposedMatrix = FirstMatrix.MatrixTransposition();
                        Console.WriteLine("Транспонированная матрица:");
                        Console.WriteLine(transposedMatrix);
                        break;
                    case "6":
                        double trace = FirstMatrix.MatrixTrace();
                        Console.WriteLine($"След матрицы: {trace}\n");
                        break;
                    case "7":
                        ConvertToDiagonal(FirstMatrix);
                        break;
                    case "8":
                        Console.WriteLine($"Первая матрица: \n{FirstMatrix}");
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.\n");
                        break;
                }
            }

            void CalculationsMenu(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
            {
                Console.WriteLine("Выберите действие\n1. Сложение\n2. Вычитание\n3. Умножение\n4. Деление\n5. Проверить на равность\n6. Сравнение >\n7. Сравнение <\n8. Сравнение >=\n9. Сравнение <=");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"MatrixA + MatrixB \n{FirstMatrix + SecondMatrix}");
                        break;
                    case "2":
                        Console.WriteLine($"MatrixA - MatrixB \n{FirstMatrix - SecondMatrix}");
                        break;
                    case "3":
                        Console.WriteLine($"MatrixA * MatrixB \n{FirstMatrix * SecondMatrix}");
                        break;
                    case "4":
                        Console.WriteLine($"FLkdhl;skfhd");
                        //Console.WriteLine($"MatrixA / MatrixB \n{FirstMatrix / SecondMatrix}");
                        break;
                    case "5":
                        Console.WriteLine($"MatrixA == MatrixB: {FirstMatrix == SecondMatrix}");
                        break;
                    case "6":
                        Console.WriteLine($"MatrixA > MatrixB: {FirstMatrix > SecondMatrix}");
                        break;
                    case "7":
                        Console.WriteLine($"MatrixA < MatrixB: {FirstMatrix < SecondMatrix}");
                        break;
                    case "8":
                        Console.WriteLine($"MatrixA >= MatrixB: {FirstMatrix >= SecondMatrix}");
                        break;
                    case "9":
                        Console.WriteLine($"MatrixA <= MatrixB: {FirstMatrix <= SecondMatrix}");
                        break;
                    default:
                        Console.WriteLine("Нету такого выбора!");
                        break;
                }
            }
            void FillMatrix(CreationOfMatrix FirstMatrix, CreationOfMatrix Secondmatrix)
            {
                Console.WriteLine("Заполненые матрицы:");
                Console.WriteLine(FirstMatrix.ToString());
                Console.WriteLine(SecondMatrix.ToString());
            }


            /*Console.WriteLine($"Сложение: \n{FirstMatrix + SecondMatrix}");

            Console.WriteLine($"Вычитание: \n{FirstMatrix - SecondMatrix}");

            Console.WriteLine($"Произведение: \n{FirstMatrix * SecondMatrix}");

            Console.WriteLine($"Матрица А > Матрица Б: {FirstMatrix > SecondMatrix}");
            Console.WriteLine($"Матрица А >= Матрица Б: {FirstMatrix >= SecondMatrix}");
            Console.WriteLine($"Матрица А <= Матрица Б: {FirstMatrix <= SecondMatrix}");
            Console.WriteLine($"Матрица А < Матрица Б: {FirstMatrix < SecondMatrix}");
            Console.WriteLine($"Матрица А == Матрица Б: {FirstMatrix == SecondMatrix}");
            Console.WriteLine($"Матрица А != Матрица Б: {FirstMatrix != SecondMatrix} \n");

            Console.WriteLine($"Детерминант матрицы А: {FirstMatrix.Determinant()} \n");

            void ConvertToDiagonal(CreationOfMatrix FirstMatrix)
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
                Console.WriteLine("Матрица приведена к диагональному виду.\n");
            }
            ConvertToDiagonal(FirstMatrix);
            Console.WriteLine($"Первая матрица: \n{FirstMatrix}");


            try
            {
                var InverseA = FirstMatrix.Inversion();
                Console.WriteLine($"Обратная матрица А:\n{InverseA}");
            }
            catch (NotInvertible ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }
    }
}
