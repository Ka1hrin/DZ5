using System;
using System.IO;
using DZ5;

namespace PDF5
{
    class PDF5
    {
        static void Main()
        {
            string pathToImages = "images\\32_pink_jpgs";
            string filePath = "students.txt";

            Task1(pathToImages);
            Task2(filePath);
        }

        private static void Task1(string pathToImages)
        {
            if (!Directory.Exists(pathToImages))
            {
                Console.WriteLine($"\"Папка не найдена по пути: {pathToImages}");
                return;
            }

            string[] allFiles = Directory.GetFiles(pathToImages, "*.jpg");
            //Directory.GetFiles(pathToImages, "*.jpg") - метод, который читает файлы из папки из pathToImages с окончанием jpg

            List<string> images = new List<string>();

            foreach (string file in allFiles)
            {
                images.Add(file);
                images.Add(file);
            }

            PrintCards("Изначальная последовательность картинок", images);

            Random random = new Random();

            //цикл Фишера - Йетса
            for (int i = images.Count - 1; i > 0;i--) //Индексы начинаются с 0, поэтому -1
            {
                //Создаем случайный индекс
                int j = random.Next(i + 1);
                string temp = images[i];
                //Заменяем значение под одним индексом значением другого
                images[i] = images[j];
                images[j] = temp;
                //Таким образом мы поменяли 2 карты местами; повторяем этот процесс 64 раза
            }

            PrintCards("Итоговая последовательность картинок", images);
        }

        #region
        ///<summary>
        ///Метод, который выводит карточки 
        ///</summary>
        private static void PrintCards(string prompt, List<string> list )
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < list.Count; i++) //Count - количество элементов коллекции
            {
                Console.WriteLine($"{i + 1}: {Path.GetFileName(list[i])}"); //Path.GetFileName(images[i]) - оставляет от файла только название
            }
            Console.WriteLine();
        }
        #endregion

        private static void Task2(string filePath)
        {
            Dictionary<string, Student> students = new Dictionary<string, Student>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                // Разбиваем строку по точке с запятой
                string[] parts = line.Split(';');

                //Проверяем, что все 5 элементов есть
                if (parts.Length == 5)
                {
                    string lastName = parts[0];
                    string firstName = parts[1];
                    int birthYear = int.Parse(parts[2]);
                    string exam = parts[3];
                    int score = int.Parse(parts[4]);

                    Student student = new Student(lastName, firstName, birthYear, exam, score);

                    string key = $"{lastName} {firstName}";

                    students.Add(key, student);
                }
            }
            Console.WriteLine($"Загружено студентов из файла: {students.Count}");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"\nМеню:\n1. Новый студент\n2. Удалить\n3. Сортировать\n4. Выйти\n");

                string choice = Console.ReadLine()?.ToLower();

                switch (choice)
                {
                    case "1":
                    case "новый студент":
                        AddStudent(students);
                        break;

                    case "2":
                    case "удалить":
                        RemoveStudent(students);
                        break;

                    case "3":
                    case "сортировать":
                        SortStudents(students);
                        break;

                    case "4":
                    case "выйти":
                        isRunning = false;
                        Console.WriteLine("Выход из программы...");
                        break;

                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }

        }

        #region
        ///<summary>
        ///Метод выводв студента
        ///</summary>
        private static void PrintStudent(Student student)
        {
            Console.WriteLine($"Фамилия: {student.LastName}, Имя: {student.FirstName}, Год: {student.BirthYear}, Экзамен: {student.Exam}, Баллы: {student.Score}");
        }

        ///<summary>
        ///Метод добавления студента
        ///</summary>
        private static void AddStudent(Dictionary<string, Student> students)
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Ошибка: фамилия не может быть пустой!");
                return;
            }

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Ошибка: имя не может быть пустой!");
                return;
            }

            string key = $"{lastName} {firstName}";

            // Проверка, что такого студента еще нет
            if (students.ContainsKey(key))
            {
                Console.WriteLine("Ошибка: Студент с такой фамилией и именем уже существует!");
                return;
            }

            int birthYear = Helper.ReadInt("Введите дату рождения");
            Console.Write("Введите экзамен: ");
            string exam = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(exam))
            {
                Console.WriteLine("Ошибка: строка не может быть пустой!");
                return;
            }
            int score = Helper.ReadInt("Введите баллы");

            // Создаем и добавляем
            students.Add(key, new Student(lastName, firstName, birthYear, exam, score));
            Console.WriteLine("Студент успешно добавлен!");
        }

        ///<summary>
        ///Метод удаления студента
        ///</summary>
        private static void RemoveStudent(Dictionary<string, Student> students)
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Ошибка: строка не может быть пустой!");
                return;
            }
            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Ошибка: строка не может быть пустой!");
                return;
            }

            string key = $"{lastName} {firstName}";

            if (students.Remove(key))
                Console.WriteLine("Студент успешно удален!");
            else
                Console.WriteLine("Студент с такими данными не найден.");
        }

        ///<summary>
        ///Метод сортировки студентов
        ///</summary>
        private static void SortStudents(Dictionary<string, Student> students)
        {
            // Достаем всех студентов из словаря в список, чтобы отсортировать
            List<Student> studentList = new List<Student>(students.Values);

            // Сортируем список по баллам
            studentList.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));

            // Выводим отсортированный список
            foreach (Student s in studentList)
            {
                PrintStudent(s);
            }
        }
        #endregion
    }
}
