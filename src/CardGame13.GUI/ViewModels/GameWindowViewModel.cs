using CardGame13.Game;
using CardGame13.GUI.Commands;
using CardGame13.Network;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGame13.GUI.ViewModels
{
    public class GameWindowViewModel : BaseViewModel
    {
        private Player Player { get; }

        private Player _CurrentPlayer;
        public Player CurrentPlayer
        {
            get => _CurrentPlayer;
            set => SetProperty(ref _CurrentPlayer, value);
        }

        public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();

        private string _CurrentCategory = "";

        public string CurrentCategory
        {
            get => _CurrentCategory;
            set => SetProperty(ref _CurrentCategory, value);
        }

        private Client Client { get; }

        public ObservableCollection<Card> Cards { get; } = new ObservableCollection<Card>();

        public ObservableCollection<Card> Pile { get; } = new ObservableCollection<Card>();

        private bool _CanSubmit = false;
        public bool CanSubmit
        {
            get => _CanSubmit;
            set => SetProperty(ref _CanSubmit, value);
        }

        private bool _CanPass = false;
        public bool CanPass
        {
            get => _CanPass;
            set => SetProperty(ref _CanPass, value);
        }

        public ICommand SendCommand { get; }

        public ICommand PassCommand { get; }

        public GameWindowViewModel(Client client, NetworkMessage message)
        {
            SendCommand = new ActionCommand(OnSend);
            PassCommand = new RelayCommand(OnPass);
            Player = message.Player;
            Client = client;
            foreach (Card card in message.Hand) Cards.Add(card);
            WaitForMessages();
        }

        private async void WaitForMessages()
        {
            while (Players.Count < 4) await WaitForPlayers();
            if (IsFirstTurn()) CanSubmit = true;
            CurrentCategory = "First";
            WaitForPlays();
        }

        private async Task WaitForPlayers()
        {
            var incomingMessage = await Client.ReceiveMessage().ConfigureAwait(false);
            if (Players.Count == 0)
            {
                foreach (var player in incomingMessage.Players) Players.Add(player);
            }
            else
            {
                App.Current.Dispatcher.Invoke(() => Players.Add(incomingMessage.Players[^1]));
            }
        }

        private bool IsFirstTurn()
        {
            foreach (Card card in Cards)
            {
                if (card.RankValue == 0 && card.SuitValue == 0) return true;
            }
            return false;
        }

        private async void WaitForPlays()
        {
            while (true)
            {
                var incomingMessage = await Client.ReceiveMessage().ConfigureAwait(false);
                var previousPlayerNumber = incomingMessage.Player.PlayerNumber;
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (incomingMessage.MessageType == NetworkHelper.MessageType.Pass)
                    {
                        if (incomingMessage.LastPlayBy == (previousPlayerNumber + 1) % 4) Pile.Clear();
                    }
                    else
                    {
                        Pile.Clear();
                        foreach (Card card in incomingMessage.Hand) Pile.Add(card);
                        var player = Players.ElementAt(previousPlayerNumber);
                        player.CardCount -= Pile.Count;
                        Players.RemoveAt(previousPlayerNumber);
                        Players.Insert(previousPlayerNumber, player);
                        if (previousPlayerNumber == Player.PlayerNumber) RemovePlayedCards();
                    }
                    CurrentPlayer = Players[(previousPlayerNumber + 1) % 4];
                    UpdateTurn(previousPlayerNumber);
                });
                CurrentCategory = incomingMessage.CurrentCategory.ToString();
            }
        }

        private void UpdateTurn(int previousPlayerNumber)
        {
            var changeTo = (previousPlayerNumber + 1) % 4 == Player.PlayerNumber;
            if (changeTo && Cards.Count == 0) OnPass();
            else
            {
                CanSubmit = changeTo;
                CanPass = changeTo;
            }
        }

        private void RemovePlayedCards()
        {
            var cardsToDelete = new List<Card>();
            foreach (Card cardToRemove in Pile)
            {
                foreach (Card card in Cards)
                {
                    if (cardToRemove.RankValue == card.RankValue && cardToRemove.SuitValue == card.SuitValue) cardsToDelete.Add(card);
                }
            }
            foreach (Card card in cardsToDelete) Cards.Remove(card);
        }

        private void OnSend(object hand)
        {
            var cardsToPlay = new List<Card>();
            var items = (System.Collections.IList)hand;
            var collection = items.Cast<Card>();
            foreach (Card card in collection) cardsToPlay.Add(card);
            cardsToPlay.Sort((a, b) =>
                (a.RankValue < b.RankValue || (a.RankValue == b.RankValue && a.SuitValue < b.SuitValue)) ? -1 : 1);
            var message = new NetworkMessage
            {
                MessageType = NetworkHelper.MessageType.ValidPlay,
                Player = Player,
                Hand = cardsToPlay
            };
            Client.SendMessage(message);
        }

        private void OnPass()
        {
            var message = new NetworkMessage
            {
                MessageType = NetworkHelper.MessageType.Pass,
                Player = Player
            };
            Client.SendMessage(message);
        }
    }
}
