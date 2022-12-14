using AppBuscaSites.Models;
using AppBuscaSites.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuscaSites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private GameScore score;
        GameScoreApi api;
        public Page1()
        {
            InitializeComponent();
            api = new GameScoreApi();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //localiza um registro
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(entId.Text));
                if (score.id > 0)
                {
                    entHiScore.Text = score.highscore.ToString();
                    entGame.Text = score.game;
                    entName.Text = score.name;
                    entPhrase.Text = score.phrase;
                    entEmail.Text = score.email;
                    btSalvar.Text = "Atualizar";
                }
                else btSalvar.Text = "Cadastrar";
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            //cadastra ou altera um registro
            try
            {
                score = new GameScore();
                score.game = entGame.Text;
                score.name = entName.Text;
                score.highscore = Convert.ToInt32(entHiScore.Text);
                score.email = entEmail.Text;
                score.phrase = entPhrase.Text;
                if (btSalvar.Text == "Atualizar")
                {
                    score.id = Convert.ToInt32(entId.Text);
                    await api.UpDateHighScore(score);
                }
                else
                {
                    await api.CreateHighScore(score);
                }
                this.LimparCampos();
                await DisplayAlert("Alerta", "Operação realizada com sucesso", "OK");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }

        }

        private void LimparCampos()
        {
            entId.Text = "";
            entHiScore.Text = "";
            entGame.Text = "";
            entName.Text = "";
            entPhrase.Text = "";
            entEmail.Text = "";
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(entId.Text));
                if (score.id > 0)
                {
                    await api.DeleteHighScore(score.id);
                }
                await DisplayAlert("Alerta", "Operação realizada com sucesso", "OK");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }
    }
}