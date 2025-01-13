using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace CrosswordApp
{
    public partial class MainWindow : Window
    {
        private const int rows = 20; // Количество строк
        private const int cols = 20; // Количество столбцов
        private TextBox[,] textBoxes = new TextBox[rows, cols];
        char[,] arrayChar = new char[rows, cols];
        private bool isFirstClick = true;
        List<Word> words = new List<Word>();
        List<Tuple<int, int>> index = new List<Tuple<int, int>>();
        bool[,] position = new bool[rows, cols];
        public class Word
        {
            public int len;
            public string letters;
            public int x;
            public int y;
            public bool oriention;//true horiz false vertic
            public Word(int len, string letters, int x, int y, bool oriention)
            {
                this.len = len;
                this.letters = letters;
                this.x = x;
                this.y = y;
                this.oriention = oriention;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            InitializeEmptyCrossword(17, 17);
        }
        private void InitializeEmptyCrossword(int rows, int cols)
        {
            CrosswordGrid.RowDefinitions.Clear();
            CrosswordGrid.ColumnDefinitions.Clear();
            CrosswordGrid.Children.Clear();
            // Добавление строк и столбцов
            for (int i = 0; i < rows; i++)
                CrosswordGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            for (int j = 0; j < cols; j++)
                CrosswordGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        private bool AddWord(Word word)
        {
            int flag = 1;
            for (int i = 0; i < word.letters.Length; i++)
            {
                if (word.oriention)
                {
                    if (flag == 1)
                    {
                        for (int j = 0; j < word.letters.Length; j++)
                        {
                            flag = 0;
                            if (arrayChar[word.x, word.y + j] != default(char) && arrayChar[word.x, word.y + j] != word.letters[j]) return false;
                        }
                    }
                    if (arrayChar[word.x, word.y + i] != default(char) && arrayChar[word.x + i, word.y] == word.letters[i])
                        continue;
                    else
                    {
                        // Создаем боковой элемент для каждого символа
                        Border border = new Border
                        {
                            Width = 40,
                            Height = 40,
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness(1),
                            Margin = new Thickness(0)
                        };
                        var textBox = new System.Windows.Controls.Label
                        {
                            Content = word.letters[i].ToString(),
                            Width = 40,
                            Height = 40,
                            FontSize = 24,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        // Устанавливаем позицию для каждой буквы
                        Grid.SetRow(border, word.x);
                        Grid.SetColumn(border, word.y + i);
                        arrayChar[word.x, word.y + i] = word.letters[i];
                        border.Child = textBox;
                        CrosswordGrid.Children.Add(border);
                    }
                }
                else
                {
                    if (flag == 1)
                    {
                        for (int j = 0; j < word.letters.Length; j++)
                        {
                            flag = 0;
                            if (arrayChar[word.x + j, word.y] != 0 && arrayChar[word.x + j, word.y] != word.letters[j]) return false;
                        }
                    }
                    if (arrayChar[word.x + i, word.y] != default(char) && arrayChar[word.x + i, word.y] == word.letters[i])
                        continue;
                    else
                    {
                        // Создаем боковой элемент для вертикального расположения
                        Border border = new Border
                        {
                            Width = 40,
                            Height = 40,
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness(1),
                            Margin = new Thickness(0)
                        };
                        var textBox = new System.Windows.Controls.Label
                        {
                            Content = word.letters[i].ToString(),
                            Width = 40,
                            Height = 40,
                            FontSize = 24,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        // Устанавливаем позицию для каждой буквы
                        Grid.SetRow(border, word.x + i);
                        Grid.SetColumn(border, word.y);
                        arrayChar[word.x + i, word.y] = word.letters[i];
                        border.Child = textBox;
                        CrosswordGrid.Children.Add(border);
                    }
                }
            }
            return true;
        }
        private int CalculateConnectivity(int row, int col, bool orientation, string inputWord)
        {
            int connectivity = 0;
            for (int i = 0; i < inputWord.Length; i++)
            {
                int checkRow, checkCol;
                if (orientation)
                {
                    checkRow = row + i;
                    checkCol = col;
                }
                else
                {
                    checkRow = row;
                    checkCol = col + i;
                }
                if (checkRow < 0 || checkRow >= 17 || checkCol < 0 || checkCol >= 17) continue;
                foreach (Word existingWord in words)
                {
                    if (existingWord.oriention)
                    {
                        if (checkRow == existingWord.x && checkCol >= existingWord.y && checkCol < existingWord.y + existingWord.len)
                        {
                            connectivity++;
                            break;
                        }
                    }
                    else
                    {
                        if (checkCol == existingWord.y && checkRow >= existingWord.x && checkRow < existingWord.x + existingWord.len)
                        {
                            connectivity++;
                            break;
                        }
                    }
                }
            }
            return connectivity;
        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            string inputWord = MyTextBox.Text;
            if (isFirstClick)
            {
                Word word = new Word(inputWord.Length, inputWord, 7, 7, true);
                isFirstClick = false;
                AddWord(word);
                index.Add(new Tuple<int, int>(7, 7));
                for (int f = 0; f < inputWord.Length; f++)
                    position[7 - 1, 7 + f] = true;
                position[7 - 1, 7 + inputWord.Length+1] = true;
                for (int f = 0; f < inputWord.Length; f++)
                    position[7 + 1, 7 + f] = true;
                words.Add(word);
                MyTextBox.Clear();
            }
            else
            {
                List<(int row, int col, bool orientation, int connectivity)> bestPositions = new List<(int row, int col, bool orientation, int connectivity)>();
                for (int i = 0; i < words.Count; i++)
                {
                    for (int j = 0; j < words[i].len; j++)
                    {
                        for (int k = 0; k < inputWord.Length; k++)
                        {
                            if ((inputWord[k] == words[i].letters[j] && !position[index[i].Item1, index[i].Item2 + j] && words[i].oriention) || (inputWord[k] == words[i].letters[j] && !position[index[i].Item1 + j, index[i].Item2] && !words[i].oriention))
                            {
                                if (words[i].oriention)
                                {
                                    int connectivity = CalculateConnectivity(index[i].Item1 - k, index[i].Item2 + j, false, inputWord);
                                    bestPositions.Add((index[i].Item1 - k, index[i].Item2 + j, false, connectivity));
                                }
                                else
                                {
                                    int connectivity = CalculateConnectivity(index[i].Item1 + j, index[i].Item2 - k, true, inputWord);
                                    bestPositions.Add((index[i].Item1 + j, index[i].Item2 - k, true, connectivity));
                                }
                            }
                        }
                    }
                }
                if (bestPositions.Count > 0)
                {
                    bestPositions = bestPositions.OrderByDescending(p => p.connectivity).ToList();
                    foreach (var bestPosition in bestPositions)
                    {
                        Word word = new Word(inputWord.Length, inputWord, bestPosition.row, bestPosition.col, bestPosition.orientation);
                        if (AddWord(word))
                        {
                            index.Add(new Tuple<int, int>(bestPosition.row, bestPosition.col));
                            words.Add(word);
                            MyTextBox.Clear();
                            if (bestPosition.orientation)
                            {
                                for (int f = 0; f < inputWord.Length; f++)
                                {
                                    position[bestPosition.row - 1, bestPosition.col + f] = true; 
                                    if (f == inputWord.Length - 1) position[bestPosition.row, bestPosition.col + f+1] = true;

                                }
                                position[bestPosition.row, bestPosition.col-1] = true;
                                for (int f = 0; f < inputWord.Length; f++)
                                    position[bestPosition.row + 1, bestPosition.col + f] = true;

                            }
                            else
                            {
                                for (int f = 0; f < inputWord.Length; f++)
                                {
                                    position[bestPosition.row + f, bestPosition.col + 1] = true;
                                    if (f == inputWord.Length - 1) position[bestPosition.row+f+1, bestPosition.col] = true;
                                }
                                position[bestPosition.row-1, bestPosition.col] = true;
                                for (int f = 0; f < inputWord.Length; f++)
                                    position[bestPosition.row + f, bestPosition.col - 1] = true;
                            }
                            return;
                        }
                    }   
                }
                MessageBox.Show("Слово не может быть добавлено.");
                MyTextBox.Clear();
            }
        }
    }
}