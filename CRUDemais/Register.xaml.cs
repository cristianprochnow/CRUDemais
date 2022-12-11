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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private DataGrid dataGridForView;
        private int selectedIndexFromGridView = -1;

        public Register(DataGrid dataGridForView, int selectedIndexFromGridView)
        {
            InitializeComponent();

            this.dataGridForView = dataGridForView;
            this.selectedIndexFromGridView = selectedIndexFromGridView;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            salvarDadosFormulario();
        }

        private void salvarDadosFormulario()
        {
            FatoCurioso? fatoCuriosoExistente = getEdition();
            LocalDatabase localDatabase = LocalDatabase.getInstance();

            string descricao = TXTdescricao.Text;
            string informacoes = TXTinformacoes.Text;
            string tags = TXTtags.Text;
            decimal avaliacao = (decimal) SLDavaliacao.Value;

            FatoCurioso fatoCurioso = new FatoCurioso()
            {
                Descricao = descricao,
                Informacoes = informacoes,
                Tags = tags,
                Avaliacao = avaliacao
            };
            // Caso for edição, substitue o código automático pelo código existente.
            if (fatoCuriosoExistente != null)
            {
                fatoCurioso.Codigo = fatoCuriosoExistente.Codigo;
                localDatabase.update(fatoCurioso);
            }
            else
            {
                localDatabase.insert(fatoCurioso);
            }

            this.dataGridForView.Items.Refresh();
            this.dataGridForView.Focus();
            this.Close();
        }

        private void SLDavaliacao_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clearForm();

            if (this.selectedIndexFromGridView != -1)
            {
                activateEdition();
            }

            TXTdescricao.Focus();
        }

        private void activateEdition()
        {
            FatoCurioso? fatoCurioso = getEdition();

            if (fatoCurioso != null)
            {
                TXTdescricao.Text = fatoCurioso.Descricao;
                TXTinformacoes.Text = fatoCurioso.Informacoes;
                TXTtags.Text = fatoCurioso.Tags;
                SLDavaliacao.Value = (double) fatoCurioso.Avaliacao;
            }
        }

        private FatoCurioso? getEdition()
        {
            FatoCurioso? fatoCurioso = null;

            if (this.selectedIndexFromGridView != -1)
            {
                LocalDatabase localDatabase = LocalDatabase.getInstance();
                int codigo = localDatabase.list()[this.selectedIndexFromGridView].Codigo;
                fatoCurioso = localDatabase.get(codigo);
            }

            return fatoCurioso;
        }

        private void clearForm()
        {
            TXTdescricao.Text = "";
            TXTinformacoes.Text = "";
            TXTtags.Text = "";
            SLDavaliacao.Value = (double) 0;
        }
    }
}
