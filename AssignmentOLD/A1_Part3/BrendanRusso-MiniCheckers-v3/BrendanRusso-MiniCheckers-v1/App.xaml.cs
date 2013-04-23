using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BrendanRusso_MiniCheckers_v1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window;
        private MainWindow player2Window;
        private string networkIPAddress;
        private string player1Name;
        private string player2Name;
        private State state;

        public App()
            : base()
        {
            state = State.WAIT_VALID_IP;
            //TODO incorperate this with the view so when I click on a space it highlights red
            player1Window = new MainWindow();
            player2Window = new MainWindow();
            player2Window.IPAddressMenuItem.IsEnabled = false;
            player1Window.Show();
            player2Window.Show();

            //wire the handlers
            wireHandlers(player1Window);
            wireHandlers(player2Window);

            updateGameState();
        }
        private void updateGameState()
        {
            player1Window.changeGameState(state, 1);
            player2Window.changeGameState(state, 2);
        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
            playerWindow.NameMenuItemHandler(HandleNameMenuItem);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            state = State.WAIT_PLAYER1_NAME;
            player1Window.IPAddressMenuItem.IsEnabled = false;
            updateNameMenu();
            updateGameState();
        }

        private void HandleNameMenuItem(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            setNameInformationDialogOwner(dialog);
            dialog.ShowDialog();
            getPlayerNames(dialog);
            changeState();
            updateNameMenu(); 
            updateGameState();                       
        }

        private void updateNameMenu()
        {
            if (state == State.WAIT_PLAYER1_NAME)
            {
                player1Window.setName.IsEnabled = true;
                player2Window.setName.IsEnabled = false;
            }
            else if (state == State.WAIT_PLAYER2_NAME)
            {
                player2Window.setName.IsEnabled = true;
                player1Window.setName.IsEnabled = false;
            }
            else
            {
                player1Window.setName.IsEnabled = false;
                player2Window.setName.IsEnabled = false;
            }
        }

        private void changeState()
        {
            if (state == State.WAIT_PLAYER1_NAME)
            {
                state = State.WAIT_PLAYER2_NAME;
            }
            else if(state == State.WAIT_PLAYER2_NAME)
            {
                state = State.PLAYER1_MOVING;
            }
        }

        private void setNameInformationDialogOwner(NameInformationWindow dialog)
        {
            if (state == State.WAIT_PLAYER1_NAME)
            {
                dialog.Owner = player1Window;
            }
            else if(state == State.WAIT_PLAYER2_NAME)
            {
                dialog.Owner = player2Window;
            }
        }

        private void getPlayerNames(NameInformationWindow dialog)
        {
            if (state == State.WAIT_PLAYER1_NAME)
            {
                player1Name = dialog.GetName();
            }
            else if (state == State.WAIT_PLAYER2_NAME)
            {
                player2Name = dialog.GetName();
            }
        }
    }
}
