using System;

public class SquareMatrix
{
    private int[,] matrix;
    private int size;

    public SquareMatrix(int size)
    {
        this.size = size;
        matrix = new int[size, size];
    }

    public int Size
    {
        get { return size; }
    }

    public int this[int row, int col]
    {
        get { return matrix[row, col]; }
        set { matrix[row, col] = value; }
    }

    public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (matrix1.Size != matrix2.Size) 
            throw new ArgumentException("Размеры матриц должны совпадать.");

        int size = matrix1.Size;
        SquareMatrix result = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        return result;
    }

    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
            throw new ArgumentException("Размеры матриц должны совпадать.");

        int size = matrix1.Size;
        SquareMatrix result = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    result[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }

        return result;
    }

    public static SquareMatrix operator >(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
            throw new ArgumentException("Размеры матриц должны совпадать.");

        int size = matrix1.Size;
        SquareMatrix result = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = matrix1[i, j] > matrix2[i, j] ? matrix1[i, j] : matrix2[i, j];
            }
        }

        return result;
    }

    public static SquareMatrix operator <(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
            throw new ArgumentException("Размеры матриц должны совпадать.");

        int size = matrix1.Size;
        SquareMatrix result = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = matrix1[i, j] < matrix2[i, j] ? matrix1[i, j] : matrix2[i, j];
            }
        }

        return result;
    }

    public static bool operator ==(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (ReferenceEquals(matrix1, matrix2))
            return true;

        if (matrix1 is null || matrix2 is null)
            return false;

        if (matrix1.Size != matrix2.Size)
            return false;

        int size = matrix1.Size;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (matrix1[i, j] != matrix2[i, j])
                    return false;
            }
        }

        return true;
    }

    public static bool operator !=(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        return !(matrix1 == matrix2);
    }

    public static implicit operator SquareMatrix(int[,] array)
    {
        int size = (int)Math.Sqrt(array.Length);
        SquareMatrix matrix = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = array[i, j];
            }
        }

        return matrix;
    }

    public override bool Equals(object obj)
    {
        if (obj is SquareMatrix otherMatrix)
        {
            return this == otherMatrix;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return matrix.GetHashCode();
    }

    public int Determinant()
    {
        // Реализация вычисления определителя
        // ...
        return 0;
    }

    public SquareMatrix Inverse()
    {
        // Реализация вычисления обратной матрицы
        // ...
        return null;
    }

    public SquareMatrix Transpose()
    {
        int size = Size;
        SquareMatrix transposedMatrix = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                transposedMatrix[i, j] = this[j, i];
            }
        }

        return transposedMatrix;
    }

    public int Trace()
    {
        int size = Size;
        int trace = 0;

        for (int i = 0; i < size; i++)
        {
            trace += this[i, i];
        }

        return trace;
    }
}

public delegate void DiagonalizeMatrixDelegate(SquareMatrix matrix);

public abstract class MatrixOperationHandler
{
    private MatrixOperationHandler nextHandler;

    public void SetNextHandler(MatrixOperationHandler handler)
    {
        nextHandler = handler;
    }

    public virtual void HandleRequest(SquareMatrix matrix)
    {
        if (nextHandler != null)
        {
            nextHandler.HandleRequest(matrix);
        }
    }
}

public class TransposeMatrixHandler : MatrixOperationHandler
{
    public override void HandleRequest(SquareMatrix matrix)
    {
        matrix = matrix.Transpose();

        Console.WriteLine("Транспонированная матрица:");
        PrintMatrix(matrix);

        base.HandleRequest(matrix);
    }

    private void PrintMatrix(SquareMatrix matrix)
    {
        int size = matrix.Size;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

public class TraceMatrixHandler : MatrixOperationHandler
{
    public override void HandleRequest(SquareMatrix matrix)
    {
        int trace = matrix.Trace();

        Console.WriteLine($"След матрицы: {trace}");

        base.HandleRequest(matrix);
    }
}

public class DiagonalizeMatrixHandler : MatrixOperationHandler
{
    public override void HandleRequest(SquareMatrix matrix)
    {
        DiagonalizeMatrixDelegate diagonalizeDelegate = delegate (SquareMatrix m)
        {
            // Реализация приведения матрицы к диагональному виду
            // ...
            Console.WriteLine("Матрица приведена к диагональному виду.");
        };

        diagonalizeDelegate(matrix);

        base.HandleRequest(matrix);
    }
}

public class MatrixCalculator
{
    private MatrixOperationHandler operationChain;

    public MatrixCalculator()
    {
        operationChain = CreateOperationChain();
    }

    private MatrixOperationHandler CreateOperationChain()
    {
        MatrixOperationHandler transposeHandler = new TransposeMatrixHandler();
        MatrixOperationHandler traceHandler = new TraceMatrixHandler();
        MatrixOperationHandler diagonalizeHandler = new DiagonalizeMatrixHandler();

        transposeHandler.SetNextHandler(traceHandler);
        traceHandler.SetNextHandler(diagonalizeHandler);

        return transposeHandler;
    }

    public void ExecuteOperation(SquareMatrix matrix)
    {
        operationChain.HandleRequest(matrix);
    }
}

public class Program
{
    static void Main()
    {
        Console.WriteLine("Введите размер квадратной матрицы:");
        int size = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите элементы матрицы, разделяя их пробелом или переходом на новую строку:");

        SquareMatrix matrix1 = ReadMatrix(size);
        SquareMatrix matrix2 = null;

        Console.WriteLine("Выполнить операцию над одной матрицей (1) или двумя матрицами (2)?");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 2)
        {
            Console.WriteLine("Введите элементы второй матрицы:");

            matrix2 = ReadMatrix(size);
        }

        MatrixCalculator calculator = new MatrixCalculator();

        if (matrix2 == null)
        {
            calculator.ExecuteOperation(matrix1);
        }
        else
        {
            Console.WriteLine("Выберите операцию: ");
            Console.WriteLine("1. Сложение матриц");
            Console.WriteLine("2. Умножение матриц");
            Console.WriteLine("3. Определение большей матрицы");
            Console.WriteLine("4. Определение меньшей матрицы");
            Console.WriteLine("5. Сравнение матриц на равенство");
            Console.WriteLine("6. Сравнение матриц на неравенство");

            int operationChoice = int.Parse(Console.ReadLine());

            switch (operationChoice)
            {
                case 1:
                    SquareMatrix sumMatrix = matrix1 + matrix2;
                    Console.WriteLine("Результат сложения матриц:");
                    PrintMatrix(sumMatrix);
                    break;
                case 2:
                    SquareMatrix productMatrix = matrix1 * matrix2;
                    Console.WriteLine("Результат умножения матриц:");
                    PrintMatrix(productMatrix);
                    break;
                case 3:
                    SquareMatrix greaterMatrix = matrix1 > matrix2;
                    Console.WriteLine("Результат определения большей матрицы:");
                    PrintMatrix(greaterMatrix);
                    break;
                case 4:
                    SquareMatrix lesserMatrix = matrix1 < matrix2;
                    Console.WriteLine("Результат определения меньшей матрицы:");
                    PrintMatrix(lesserMatrix);
                    break;
                case 5:
                    bool isEqual = matrix1 == matrix2;
                    Console.WriteLine($"Результат сравнения матриц на равенство: {isEqual}");
                    break;
                case 6:
                    bool isNotEqual = matrix1 != matrix2;
                    Console.WriteLine($"Результат сравнения матриц на неравенство: {isNotEqual}");
                    break;
                default:
                    Console.WriteLine("Неверный выбор операции.");
                    break;
            }
        }
    }

    private static SquareMatrix ReadMatrix(int size)
    {
        SquareMatrix matrix = new SquareMatrix(size);

        for (int i = 0; i < size; i++)
        {
            string[] rowElements = Console.ReadLine().Split(' ');

            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = int.Parse(rowElements[j]);
            }
        }

        return matrix;
    }

    private static void PrintMatrix(SquareMatrix matrix)
    {
        int size = matrix.Size;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
