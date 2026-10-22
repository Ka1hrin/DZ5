using System;
using System.IO;


namespace DZ5
{
    class Laba5
    {

        static void Main()
        {
            //Laba5 - обозреватель кода - правая кнопка мыши на файл с текстом - свойства - копировать в выходной каталог: более позднюю версию - действие при сбоке: содержимое
            string filePath = "Текст для номера 6.1\\testFile.txt";

            //на 2 уровня выше экзешника
            //для задания с файлом

            Task1(filePath);
            Task2();
            Task3();

            HomeWork1(filePath);
            //Не успела(
            HomeWork3();
        }

        static void Task1(string filePath)
        {
            //Проверка файла на существование? 
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл не найден по пути: {filePath}");
                return;
            }
            var readedText = File.ReadAllText(filePath);
            char[] chars = readedText.ToCharArray();  //возвращает массив символов

            (int vowels, int consonants) = CountLetters(chars);

            Console.WriteLine($"Количество гласных: {vowels}\nКоличество согласных: {consonants}\n");

        }

        #region 
        ///<summary>
        ///Метод, который считает количество гласных и согласных в тексте и принимает массивы
        ///</summary>
        private static (int vowels, int consonants) CountLetters(char[] text)
        {
            var vowels = 0;
            var consonants = 0;

            //Создаю библиотеку значений гласных?
            var vowelsString = "уеыаёоэяию";

            //Проверяю каждую букву из текста 
            foreach (char lett in text)
            {
                //Создаю проверку символов, если это символ, то следующая проверка
                if (char.IsLetter(lett))
                {
                    //Нужно сделать проверку на гласность
                    if (vowelsString.Contains(char.ToLower(lett)))
                        vowels++;
                    else
                        consonants++;
                }
            }

            return (vowels, consonants);
        }
        #endregion

        static void Task2()
        {
            Console.WriteLine("Количество столбцов первой матрицы должно быть равно количеству строк второй");
            int str1 = Helper.ReadInt("Введите количество строк первой матрицы: ");
            int column1 = Helper.ReadInt("Введите количество столбцов первой матрицы: ");

            int str2 = Helper.ReadInt("Введите количество строк второй матрицы: ");
            int column2 = Helper.ReadInt("Введите количество столбцов второй матрицы: ");

            int min = Helper.ReadInt("Введите минимальное значение ячейки матрицы: ");
            int max = Helper.ReadInt("Введите максимальное значение ячейки матрицы: ");

            int[,] array1 = new int[str1, column1];
            int[,] array2 = new int[str2, column2];

            RandomMatrix(array1, str1, column1, min, max);
            RandomMatrix(array2, str2, column2, min, max);

            PrintMatrix("Первая матрица", array1);
            PrintMatrix("Вторая матрица", array2);

            int[,] result = MultMatrix(array1, array2);

            PrintMatrix("Произведение матриц", result);

        }

        #region
        ///<summary>
        ///Метод заполнения матрицы случайными числами и вывод
        ///</summary>
        private static void RandomMatrix(int[,] matrix, int str, int column, int min, int max)
        {
            Random random = new Random();

            for (int i = 0; i < str; i++) //заполняем цифру строки
            {
                for (int j = 0; j < column; j++) //заполняем цифру столбца
                {
                    matrix[i, j] = random.Next(min, max + 1);
                }
            }
        }

        /// <summary>
        /// Метод печати матрицы
        /// </summary>
        private static void PrintMatrix(string prompt, int[,] matrix)
        {
            Console.WriteLine($"\n{prompt}");
            for (int i = 0; i < matrix.GetLength(0); i++) //заполняем цифру строки, matrix.GetLength(0) - выдает количество строк
            {
                for (int j = 0; j < matrix.GetLength(1); j++) //заполняем цифру столбца
                {
                    Console.Write($"{matrix[i, j]} "); //необходимо, чтобы печатал в одну строку
                }
                Console.WriteLine(); //необходимо, чтобы переходил к следующей строке
            }
        }

        ///<summary>
        ///Метод умножения матрицы
        ///</summary>
        private static int[,] MultMatrix(int[,] matrix1, int[,] matrix2)
        {
            //эту матрицу заполняем заначениями и возвращаем
            //Важно: новая матрица имеет количество строк 1 матрицы и количество столбцов 2
            int[,] matrix = new int[matrix1.GetLength(0), matrix2.GetLength(1)];

            if (matrix1.GetLength(1) != matrix2.GetLength(0))
                throw new ArgumentException("Число столбцов первой матрицы должно быть равно числу строк второй!");


            //Умножение по принципу: строка на столбец
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    int sum = 0;

                    //Создадим цикл, который будет считать значения в каждой ячейке (здесь k - это количество столбцов первой и строк второй)
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    matrix[i, j] = sum;
                }

            }
            return matrix;
        }
        #endregion

        private static void Task3()
        {
            int[,] temperature = new int[12, 30];
            RandomMatrix(temperature, 12, 30, -30, 30);
            PrintMatrix("Таблица температур", temperature);

            double[] array = AverageTemp(temperature);

            Console.WriteLine($"\nСредняя температура месяцев:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"Месяц {i + 1}: {array[i]:F1} градусов");
            }

            Array.Sort(array); //метод, сортирующий массив по возрастанию
            
            foreach (var item in array)
            {
                Console.Write($"\n{item:F1} ");
            }
        }

        #region
        ///<summary>
        ///Метод, который считает среднюю температуру месяца
        ///</summary>
        private static double[] AverageTemp(int[,] matrix)
        {
            double[] array = new double[12];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int sum = 0;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sum += matrix[i, j];
                }
                array[i] = (double)sum / matrix.GetLength(1);

            }
            return array;
        }
        #endregion

        private static void HomeWork1(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл не найден по пути: {filePath}");
                return;
            }
            var readedText = File.ReadAllText(filePath);

            List<char> list = new List<char>();

            foreach (var item in readedText)
            {
                list.Add(item);
            }

            (int vowels, int consonants) = CountLetters(list);

            Console.WriteLine($"\n\nКоличество гласных: {vowels}\nКоличество согласных: {consonants}\n");
        }

        #region
        ///<summary>
        ///Метод, который считает гласные и согласные и принимает коллекции
        ///перегрузка
        ///</summary>
        private static (int vowels, int consonants) CountLetters(List<char> list)
        {
            var vowels = 0;
            var consonants = 0;

            var vowelsString = "уеыаёоэяию";

            foreach (char lett in list)
            {
                if (char.IsLetter(lett))
                {
                    if (vowelsString.Contains(char.ToLower(lett)))
                        vowels++;
                    else
                        consonants++;
                }
            }

            return (vowels, consonants);
        }

        #endregion



        private static void HomeWork3()
        {
            Dictionary<string, int[]> temperatures = new Dictionary<string, int[]>();

            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            Random random = new Random();

            //Проходим по каждому месяцу и заполняем дни температурами
            foreach (string month in months)
            {
                //Создаем массив на 30 дней
                int[] days = new int[30];

                //Заполняем его температурами
                for (int i = 0; i < days.Length; i++)
                {
                    days[i] = random.Next(-30, 30);
                }

                //Добавляем массив температур за месяц к массиву месяцев
                temperatures.Add(month, days);
            }

            Dictionary<string, double> average = AverageTemp(temperatures);
            PrintSortedAverageTemp(average);
        }

        #region
        ///<summary>
        ///Метод, который считает среднюю температуру и возвращает коллекцию 
        ///</summary>
        private static Dictionary<string, double> AverageTemp(Dictionary<string, int[]> temperatures)
        {
            Dictionary<string, double> averageTemp = new Dictionary<string, double>();

            //Проходим по паре: ключ + значение
            foreach (var pair in temperatures)
            {
                string monthName = pair.Key;
                int[] monthTemp = pair.Value;

                int sum = 0;

                foreach (var temp in monthTemp)
                {
                    sum += temp;
                }

                double average = (double)sum / monthTemp.Length;

                averageTemp.Add(monthName, average);
            }

            return averageTemp;
        }

        ///<summary>
        ///Метод, который сортирует и выводит коллекцию
        ///</summary>
        private static void PrintSortedAverageTemp(Dictionary<string, double> average)
        {
            Console.WriteLine("Средняя температура по месяцам:");

            foreach (var pair in average)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value:F1}°C");
            }

            //Превращаем коллекцию в массив
            double[] temps = average.Values.ToArray();

            Array.Sort(temps);

            Console.WriteLine($"\nСредние температуры по возрастанию:");
            foreach (double temp in temps)
            {
                Console.WriteLine($"{temp:F1}");
            }
        }
        #endregion
    }
}

