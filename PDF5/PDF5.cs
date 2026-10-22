using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace PDF5
{
    class PDF5
    {
        static void Main()
        {
            string pathToImages = "images\\32_pink_jpgs";

            Task1(pathToImages);
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
    }
}
