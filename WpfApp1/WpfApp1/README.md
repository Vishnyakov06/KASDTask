**Задача:** создать алгоритм, которой будет генерировать таблицу пересекающихся по вертикали и горизонтали слов, также необходима наибольшая связность. 

Опишем методы и поля, которые позволят нам выполнить эту задачу, а также оптимизировать отрисовку крисс-кросса.

**Начнем с описания полей:**
```
private const int rows = 20;
private const int cols = 20;
char[,] arrayChar = new char[rows, cols];
private bool isFirstClick = true;
List<Word> words = new List<Word>();
List<Tuple<int, int>> index = new List<Tuple<int, int>>();
bool[,] position = new bool[rows, cols];
```
1) words: список для хранения слов, которые ввел пользователь
2) rows: количество строк в поле для игры
3) cols: количество столбцов в поле для игры
4) arrayChar: массив для хранения символов, в веденном слове
5) index: массив пар для хранения координат введенного слова
6) position: булевский двумерный массив для определения занятых ячеек
**Дополнительный класс для описания слова**
```
public int len;//длина слова
public string letters;//само слово
public int x;//координаты слова
public int y;//координаты слова
public bool oriention;//ориентация слова
public Word(int len, string letters, int x, int y, bool oriention)
{
    this.len = len;
    this.letters = letters;
    this.x = x;
    this.y = y;
    this.oriention = oriention;
}
```
**Далее по очереди опишем методы и принципы их работы:**
1) AddWord
Данный метод добавляет слова в сетку крисс-кросс. Для каждого слова в словаре он создает ячейку и текстовый блок и размещает их в правильных координатах. Этот метод обрабатывает вертикальные и горизонтальные слова. Метод проверяет возможно ли добавить слово без наложений на другие слова. Возвращает True, если слово удалось добавить, False в противном случае.
*Приведем реализацию данного метода:*
```
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
```

2) CalculateConnectivity
Данный метод осуществляет подсчет "выгодности" добавления слова в определенной позиции. Метод возвращает целое число.
*Приведем реализацию данного метода:*
```
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
```

3) StartGame_Click
Данный метод является основным в программе, именно в нем создаются пробелы между словами, определяется куда может добавиться слово, а при вызове метода CalculateConnectivity, пытается его добавить с помощью метода AddWord. В методе описано добавление первого слово, так как оно считается уникальным. Метод либо позволит добавить слово и результат можно будет увидеть в пользовательской части программы, либо нет, и в пользовательской части программы появится сообщение о невозможности ввести слово.
*Приведем реализацию данного метода:*
```
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
```
**Код, по созданию пользовательского интерфейса в xaml**
Пользовательский интерфейс состоит из поля для записи слова, которое планируется ввести, из кнопки для добавления слова, message box для уведомления пользователя о невозможности добавить слово, а так же самой сетки - результата работы программы.
```
<Window x:Class="CrosswordApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Кроссворд" Height="700" Width="900">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>

        <TextBox x:Name="MyTextBox" Text="" Width="200" Height="30" Grid.Row="0" Margin="0,10,0,0" HorizontalAlignment="Left" VerticalAlignment="Top"/>
        <Grid x:Name="CrosswordGrid" Grid.Row="1" Grid.ColumnSpan="3" Margin="10" HorizontalAlignment="Center" VerticalAlignment="Center">
            <Grid.ColumnDefinitions>
                <!-- Здесь нужно динамически добавлять столбцы в коде C# -->
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <!-- Здесь нужно динамически добавлять строки в коде C# -->
            </Grid.RowDefinitions>

        </Grid>
        <Button Content="Добавить слово" Height="30" Click="StartGame_Click" Margin="0,0,24,0" VerticalAlignment="Center" Grid.Column="1" HorizontalAlignment="Right" Width="100"/>
    </Grid>
</Window>
```
![[Pasted image 20250113180259.png]]
Слова добавлялись в следующем порядке:
1) маргарин
2) марина
3) раковина
4) руки
5) резина
6) новизна
7) сон
8) осы
![[Pasted image 20250113180415.png]]
Приведен случай, когда к существующему слову "олени" нельзя добавить слово "рыба".


Таким образом, выше описан алгоритм, по которому cтроится головоломка крисс-кросс.