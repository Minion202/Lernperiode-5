using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        private List<Button> _buttons;
        private List<string> _colors;
        private Button _firstClickedButton;
        private Button _secondClickedButton;
        private bool _isChecking = false;
        private bool[] _isRevealed;
        private int _score;
        private int _player1Score;
        private int _player2Score;
        private bool _isPlayer1Turn = true;
        private TextBlock _scoreText;
        private Grid _gameGrid;
        private Button _onePlayerButton;
        private Button _twoPlayerButton;
        private Button _resetButton;
        private StackPanel _playerScoresPanel;
        private TextBlock _player1ScoreText;
        private TextBlock _player2ScoreText;
        private TextBlock _playerTurnText;
        private SqliteConnection _connection;
        private string _player1Name = "Spieler 1";
        private string _player2Name = "Spieler 2";


        public MainWindow()
        {
            InitializeComponent();
            _scoreText = this.FindControl<TextBlock>("ScoreText");
            _gameGrid = this.FindControl<Grid>("GameGrid");
            _onePlayerButton = this.FindControl<Button>("OnePlayerButton");
            _twoPlayerButton = this.FindControl<Button>("TwoPlayerButton");
            _resetButton = this.FindControl<Button>("ResetButton");
            _playerScoresPanel = this.FindControl<StackPanel>("PlayerScores");
            _player1ScoreText = this.FindControl<TextBlock>("Player1Score");
            _player2ScoreText = this.FindControl<TextBlock>("Player2Score");
            _playerTurnText = this.FindControl<TextBlock>("PlayerTurnText");

            _onePlayerButton.Click += (s, e) => StartGame(false);
            _twoPlayerButton.Click += (s, e) => StartGame(true);
            _resetButton.Click += (s, e) => ResetGame();

            _connection = new SqliteConnection("Data Source=/Users/mina/RiderProjects/MemoryGame/identifier.sqlite");
            _connection.Open();
            
            var newPlayerButton = this.FindControl<Button>("NewPlayerButton");
            var existingPlayerButton = this.FindControl<Button>("ExistingPlayerButton");
            var newPlayerPanel = this.FindControl<StackPanel>("NewPlayerPanel");
            var existingPlayerPanel = this.FindControl<StackPanel>("ExistingPlayerPanel");
            var newPlayerName = this.FindControl<TextBox>("NewPlayerName");
            var saveNewPlayerButton = this.FindControl<Button>("SaveNewPlayerButton");
            var existingPlayersComboBox = this.FindControl<ComboBox>("ExistingPlayersComboBox");
            var selectExistingPlayerButton = this.FindControl<Button>("SelectExistingPlayerButton");
            var playerNameInputPanel = this.FindControl<StackPanel>("PlayerNameInputPanel");
            var player1NameBox = this.FindControl<TextBox>("Player1NameBox");
            var player2NameBox = this.FindControl<TextBox>("Player2NameBox");
            var startTwoPlayerGameButton = this.FindControl<Button>("StartTwoPlayerGameButton");

            _twoPlayerButton.Click += (s, e) =>
            {
                _onePlayerButton.IsVisible = false;
                _twoPlayerButton.IsVisible = false;
                playerNameInputPanel.IsVisible = true;
            };

            startTwoPlayerGameButton.Click += (s, e) =>
            {
                _player1Name = string.IsNullOrWhiteSpace(player1NameBox.Text) ? "Spieler 1" : player1NameBox.Text;
                _player2Name = string.IsNullOrWhiteSpace(player2NameBox.Text) ? "Spieler 2" : player2NameBox.Text;
    
                playerNameInputPanel.IsVisible = false;
                StartGame(true);
            };

            
            _buttons = new List<Button>
            {
                this.FindControl<Button>("Button1"), this.FindControl<Button>("Button2"),
                this.FindControl<Button>("Button3"), this.FindControl<Button>("Button4"),
                this.FindControl<Button>("Button5"), this.FindControl<Button>("Button6"),
                this.FindControl<Button>("Button7"), this.FindControl<Button>("Button8"),
                this.FindControl<Button>("Button9"), this.FindControl<Button>("Button10"),
                this.FindControl<Button>("Button11"), this.FindControl<Button>("Button12"),
            };

            _colors = new List<string>
            {
                "#FFB3BA", "#FFB3BA", "#FFDFBA", "#FFDFBA", "#FFFFBA", "#FFFFBA",
                "#BAFFB3", "#BAFFB3", "#BAE1FF", "#BAE1FF", "#D8BAFF", "#D8BAFF"
            };

            _isRevealed = new bool[_buttons.Count];
            foreach (var button in _buttons)
            {
                button.Click += OnButtonClick;
            }

            _gameGrid.IsVisible = false;
            _scoreText.IsVisible = false;
            _resetButton.IsVisible = false;
            _playerScoresPanel.IsVisible = false;
            _playerTurnText.IsVisible = false;
        }
        private void InitPlayers()
        {
            using (var cmd = new SqliteCommand("INSERT OR IGNORE INTO players (name, wins) VALUES ('Player 1', 0), ('Player 2', 0);", _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


        private void StartGame(bool isTwoPlayer)
        {
            _scoreText.IsVisible = true;
            _gameGrid.IsVisible = true;
            _onePlayerButton.IsVisible = false;
            _twoPlayerButton.IsVisible = false;
            _resetButton.IsVisible = true;
            _playerScoresPanel.IsVisible = isTwoPlayer;
            _playerTurnText.IsVisible = isTwoPlayer;

            _player1Score = 0;
            _player2Score = 0;
            _player1ScoreText.Text = $"{_player1Name}: {_player1Score}";
            _player2ScoreText.Text = $"{_player2Name}: {_player2Score}";

            _isPlayer1Turn = true;
            _playerTurnText.Text = $"{_player1Name} ist am Zug";

            Random rand = new Random();
            _colors = _colors.OrderBy(x => rand.Next()).ToList();

            if (!isTwoPlayer)
            {
                _scoreText.Text = "Karten aufdecken";
            }
        }

        private async void OnButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    if (_isChecking) return;

    if (sender is not Button clickedButton) return;

    int buttonIndex = _buttons.IndexOf(clickedButton);
    if (_isRevealed[buttonIndex]) return;

    await HandleButtonClickAsync(clickedButton, buttonIndex);
}

private async Task HandleButtonClickAsync(Button clickedButton, int buttonIndex)
{
    clickedButton.Background = new SolidColorBrush(Color.Parse(_colors[buttonIndex]));
    _isRevealed[buttonIndex] = true;

    if (_firstClickedButton == null)
    {
        _firstClickedButton = clickedButton;
        return;
    }

    _secondClickedButton = clickedButton;
    _isChecking = true;

    await HandleSecondClickAsync();
}

private async Task HandleSecondClickAsync()
{
    bool isMatch = _colors[_buttons.IndexOf(_firstClickedButton)] == _colors[_buttons.IndexOf(_secondClickedButton)];

    if (!_playerScoresPanel.IsVisible)
    {
        if (isMatch)
        {
            ResetClickState();
        }
        else
        {
            await HideUnmatchedButtonsAsync();
        }
        return;
    }

    if (isMatch)
    {
        ProcessWinner();
    }
    else
    {
        await HideUnmatchedButtonsAsync();
        _player1ScoreText.Text = $"{_player1Name}: {_player1Score}";
        _player2ScoreText.Text = $"{_player2Name}: {_player2Score}";
        _playerTurnText.Text = _isPlayer1Turn ? $"{_player1Name} ist am Zug" : $"{_player2Name} ist am Zug";


    }
}

private async Task HideUnmatchedButtonsAsync()
{
    await Task.Delay(1300);
    _firstClickedButton.Background = Brushes.CadetBlue;
    _secondClickedButton.Background = Brushes.CadetBlue;
    _isRevealed[_buttons.IndexOf(_firstClickedButton)] = false;
    _isRevealed[_buttons.IndexOf(_secondClickedButton)] = false;
    ResetClickState();
}

private void ResetClickState()
{
    _firstClickedButton = null;
    _secondClickedButton = null;
    _isChecking = false;
}

private void InitPlayerScore(string playerName)
{
    var checkCmd = new SqliteCommand($"SELECT COUNT(*) FROM players WHERE name = '{playerName}';", _connection);
    long count = (long)checkCmd.ExecuteScalar();
    if (count == 0)
    {
        var insertCmd = new SqliteCommand($"INSERT INTO players (name, wins) VALUES ('{playerName}', 0);", _connection);
        insertCmd.ExecuteNonQuery();
    }
}

private int GetScore(string playerName)
{
    var cmd = new SqliteCommand($"SELECT wins FROM players WHERE name = '{playerName}';", _connection);
    return Convert.ToInt32(cmd.ExecuteScalar());
}
private void UpdateScores()
{
    _player1Score = GetScore("Player1");
    _player2Score = GetScore("Player2");

    _player1ScoreText.Text = $"Player 1 Wins: {_player1Score}";
    _player2ScoreText.Text = $"Player 2 Wins: {_player2Score}";
}
private void RecordGameResult(string winnerName)
{
    int playerId = GetPlayerId(winnerName);
    
    using (var cmd = new SqliteCommand($"UPDATE players SET wins = wins + 1 WHERE id = {playerId};", _connection))
    {
        cmd.ExecuteNonQuery();
    }
    
    using (var cmd = new SqliteCommand("INSERT INTO memory_game_results (player_id, result) VALUES (@playerId, 'Win');", _connection))
    {
        cmd.Parameters.AddWithValue("@playerId", playerId);
        cmd.ExecuteNonQuery();
    }

    UpdateScores();
}

private int GetPlayerId(string playerName)
{
    using (var cmd = new SqliteCommand($"SELECT id FROM players WHERE name = '{playerName}'", _connection))
    {
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

private void IncrementScore(string playerName)
{
    var cmd = new SqliteCommand($"UPDATE players SET wins = wins + 1 WHERE name = '{playerName}';", _connection);
    cmd.ExecuteNonQuery();
    string currentPlayer = _isPlayer1Turn ? "Player1" : "Player2";
IncrementScore(currentPlayer);
UpdateScores();

}

    private void ProcessWinner()
        {
            if (_isPlayer1Turn)
            {
                _player1Score++;
                _player1ScoreText.Text = $"Spieler 1: {_player1Score}";
            }
            else
            {
                _player2Score++;
                _player2ScoreText.Text = $"Spieler 2: {_player2Score}";
            }

            _firstClickedButton = null;
            _secondClickedButton = null;
            _isChecking = false;
            _playerTurnText.Text = _isPlayer1Turn ? "Spieler 1 ist am Zug" : "Spieler 2 ist am Zug";
            string currentPlayer = _isPlayer1Turn ? "Player 1" : "Player 2";
            RecordGameResult(currentPlayer);
            
            _player1ScoreText.Text = $"Player 1 Wins: {GetScore("Player 1")}";
            _player2ScoreText.Text = $"Player 2 Wins: {GetScore("Player 2")}";
            _playerTurnText.Text = _isPlayer1Turn ? "Player 1 is playing" : "Player 2 is playing";
        }
        
        private int GetTotalGamesPlayed()
        {
            using (var cmd = new SqliteCommand("SELECT COUNT(*) FROM gamesplayed", _connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        

        private void ResetGame()
        {
            _scoreText.Text = "Karten aufdecken";
            _isRevealed = new bool[_buttons.Count];

            foreach (var button in _buttons)
            {
                button.Background = Brushes.CadetBlue;
            }

            Random rand = new Random();
            _colors = _colors.OrderBy(x => rand.Next()).ToList();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
