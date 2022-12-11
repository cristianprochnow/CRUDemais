using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUDemais
{
    
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : Window
    {
        public DataView()
        {
            InitializeComponent();
        }

        private void openRegister(bool isEdition = false)
        {
            int selectedIndex = -1;
            if (isEdition)
            {
                selectedIndex = DTGdados.SelectedIndex;
            }
            Register registerWindow = new Register(DTGdados, selectedIndex);
            
            registerWindow.Show();
        }

        private void BTNadd_Click(object sender, RoutedEventArgs e)
        {
            openRegister();
        }

        private void BTNedit_Click(object sender, RoutedEventArgs e)
        {
            if (DTGdados.SelectedIndex == -1)
            {
                Msg.alert("Aviso", "Selecione um registro da tabela abaixo para editar!");

                return;
            }

            openRegister(true);
        }

        private void BTNdelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DTGdados.SelectedIndex;


            if (selectedIndex == -1)
            {
                Msg.alert("Aviso", "Selecione um registro da tabela abaixo para excluir!");

                return;
            }

            MessageBoxResult answer = Msg.confirm("Exclusão de Fato Curioso", "Confirma a exclusão do registro selecionado?");

            if (answer != MessageBoxResult.Yes)
            {
                return;
            }

            LocalDatabase localDatabase = LocalDatabase.getInstance();
            
            int codigo = localDatabase.list()[selectedIndex].Codigo;
            
            localDatabase.delete(codigo);
            DTGdados.Items.Refresh();
            DTGdados.Focus();
        }

        private void DTGdados_Loaded(object sender, RoutedEventArgs e)
        {
            DTGdados.CanUserAddRows = false;
            DTGdados.CanUserDeleteRows = false;
            DTGdados.Focus();
            DTGdados.SelectedIndex = 0;
        }

        private void WINdados_Loaded(object sender, RoutedEventArgs e)
        {
            LocalDatabase localDatabase = LocalDatabase.getInstance();

            if (localDatabase.list().Count < 1)
            {
                localDatabase.insert(new FatoCurioso()
                {
                    Descricao = "Descrição de Exemplo",
                    Informacoes = "Mais informações do exemplo que foi cadastrado.",
                    Tags = "fatos,curiosidades,fatos curiosos",
                    Avaliacao = 4
                });
            }

            DTGdados.ItemsSource = LocalDatabase.fatosCuriosos;

            DTGdados.Items.Refresh();
        }
    }
}
