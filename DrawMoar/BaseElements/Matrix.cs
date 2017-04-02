using System;


namespace DrawMoar.BaseElements
{
    /// <summary>
    /// Писал Антон к 4-ой лабе
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T>
    {
        private T[,] matrix;

        public int Rows { get; }
        public int Columns { get; }

        public Matrix(int row, int column) {
            Rows = row;
            Columns = column;

            matrix = new T[row, column];
        }

        public Matrix(T[,] matrix) {
            Rows = matrix?.GetLength(0) ?? 0;
            Columns = matrix?.GetLength(1) ?? 0;
            this.matrix = new T[Rows, Columns];

            for (int row = 0; row < Rows; row++) {
                for (int column = 0; column < Columns; column++) {
                    this.matrix[row, column] = matrix[row, column];
                }
            }
        }

        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2) {
            if (m1.Columns != m2.Rows) {
                throw new InvalidOperationException(
                    "The number of columns in first matrix must be equal to the number of rows in second matrix");
            }

            var result = new Matrix<T>(m1.Rows, m2.Columns);

            for (int row = 0; row < m1.Rows; row++) {
                for (int column = 0; column < m2.Columns; column++) {
                    for (int n = 0; n < m1.Columns; n++) {
                        result[row, column] += (dynamic)m1[row, n] * m2[n, column];
                    }
                }
            }

            return result;
        }

        public static Matrix<T> Identity(int n) {
            var result = new T[n, n];
            for (int row = 0; row < n; row++) {
                for (int column = 0; column < n; column++) {
                    result[row, column] = (
                        row == column
                        ? default(T) + (dynamic)1
                        : default(T)
                    );
                }
            }

            return new Matrix<T>(result);
        }

        public T this[int row, int column] {
            get {
                return matrix[row, column];
            }
            set {
                if (value != null) {
                    matrix[row, column] = value;
                }
                else {
                    throw new NullReferenceException("Value cannot be null");
                }
            }
        }
    }
}
